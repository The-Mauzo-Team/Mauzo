/*
 * MIT License
 *
 * Copyright (c) 2020 The Mauzo Team
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using Desktop.Exceptions;
using Desktop.Properties;
using Desktop.Templates;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;

namespace Desktop.Connectors
{
    class LoginConn
    {
        private static User user;
        private static string token = null;
        private static DateTime expirerToken;

        private string MauzoUrl = Settings.Default.MauzoServer + "/api/login";

        public string LoginUser(User user) {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.POST);

            // Creamos un objeto solo con el nombre de usuario y la contraseña.
            String jsonRequest = JsonConvert.SerializeObject(new { 
                username = user.Username,
                password = user.Password
            });

            // Se añade el json al cuerpo de la petición.
            request.AddJsonBody(jsonRequest);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);
            string result = null;

            // Procesamos la respuesta de la petición.
            if (response.IsSuccessful)
            {
                // En caso de que el servidor nos haya devuelto un token, nos lo guardaremos.
                result = response.Headers.ToList().Find(x => x.Name == "Authorization").Value.ToString();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                    // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                    throw new Exception("El usuario y contraseña no son correctos.");
                else
                {
                    // En caso de que se haya producido un error en el servidor, mostramos el mensaje en el cliente.
                    // Procesamos la respuesta y obtenemos el mensaje.
                    dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    string message = j["message"].ToString();

                    // Lanzamos una excepción con el mensaje que ha dado el servidor.
                    throw new Exception("Se ha producido un error: " + message);
                }
            }

            return result;
        }

        public static void CalculateException(IRestResponse response, string defaultMessage)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
                // En caso de que no se haya encontrado el usuario, lanzamos un mensaje personalizado.
                throw new Exception(defaultMessage);
            else if (response.StatusCode == HttpStatusCode.BadRequest)
                // En caso de que la petición no sea valida, lanzamos un mensaje personalizado.
                throw new Exception("La petición realizada no es valida.");
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                if (token != null)
                {
                    // Hacer un diff del tiempo actual con el de expiración y comprobar si es admin.
                    if (User.IsAdmin == false && (DateTime.Now.Ticks - ExpirerTime.Ticks) < 0)
                        // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                        throw new AdminForbiddenException("No tienes autorización a realizar esta operación.");
                    else if (User.IsAdmin == true && (DateTime.Now.Ticks - ExpirerTime.Ticks) < 0)
                        // Es sospechoso esto, lo más probable es que se hayan deshabilitado remotamente.
                        throw new AdminForbiddenException("Se ha producido un error, tal vez, en el servidor se hayan deshabilitado los privilegios de administrador para su usuario.");
                    else if (Token != null && (DateTime.Now.Ticks - ExpirerTime.Ticks) > 0)
                        // El token ha expirado, notificamos de ello para tomar acciones en el controlador.
                        throw new ExpiredLoginException("El token que ha proporcionado el servidor ha caducado.");
                }
                else 
                {
                    // En caso de que no haya establecido un token, devolveremos esta excepción.
                    throw new ExpiredLoginException("No se ha iniciado sesión en el servidor.");
                }
            }
            else
            {
                // En caso de que se haya producido un error en el servidor, mostramos el mensaje en el cliente.
                // Procesamos la respuesta y obtenemos el mensaje.
                dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string message = j["message"].ToString();

                // Lanzamos una excepción con el mensaje que ha dado el servidor.
                throw new Exception("Se ha producido un error: " + message);
            }
        }

        public static User User {
            get;
        }
        
        public static DateTime ExpirerTime
        {
            get;
        }

        public static string Token
        {
            get { return token; }
            set {
                // Inicializamos el parser del jwt
                JwtSecurityToken handler = (JwtSecurityToken) new JwtSecurityTokenHandler().ReadToken(token);

                // Creamos los objetos de usuario y tiempo de expiración.
                user = new User();
                expirerToken = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

                // Añadimos los atributos del usuario recuperados del token.
                user.Id = int.Parse(handler.Claims.First(claim => claim.Type == "jti").Value);
                user.Username = handler.Claims.First(claim => claim.Type == "sub").Value;
                user.IsAdmin = bool.Parse(handler.Claims.First(claim => claim.Type == "adm").Value);

                // Añadimos el tiempo en el que expira el token.
                expirerToken = expirerToken.AddSeconds(double.Parse(handler.Claims.First(claim => claim.Type == "exp").Value)).ToLocalTime();

                // Setteamos el valor del token que nos han pasado.
                token = value;
            }
        }
    }
}
