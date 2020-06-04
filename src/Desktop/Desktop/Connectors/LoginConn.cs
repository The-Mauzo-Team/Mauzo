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
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Desktop.Connectors
{
    /// <summary>
    /// Clase que gestiona el inicio de sesión de los usuarios.
    /// </summary>
    /// <remarks>@Neirth - Sergio Martinez</remarks>
    class LoginConn
    {
        private readonly string mauzoUrl = Settings.Default.MauzoServer + "/api/login";
        private static string token = null;

        /// <summary>
        /// Propiedad usada para obtener la información del usuario actual en forma de objecto.
        /// </summary>
        public static User User
        {
            get; private set;
        }

        /// <summary>
        /// Propiedad para obtener el tiempo en el cual el token expira.
        /// </summary>
        public static DateTime ExpirerToken
        {
            get; private set;
        }

        /// <summary>
        /// Propiedad que obtiene el token usado en esta instacia o lo agrega, parseando
        /// los elementos que tiene dentro respecto a la información del usuario y la 
        /// caducidad del mismo.
        /// </summary>
        public static string Token
        {
            get { return token; }
            set
            {
                // Inicializamos el parser del jwt
                JwtSecurityToken handler = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(value.Remove(0, 7));

                // Creamos los objetos de usuario y tiempo de expiración.
                User = new User();
                ExpirerToken = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

                // Añadimos los atributos del usuario recuperados del token.
                List<Claim> claims = handler.Claims.ToList();

                User.Id = int.Parse(claims[1].Value);
                User.Username = claims[2].Value;
                User.IsAdmin = bool.Parse(claims[3].Value);

                // Añadimos el tiempo en el que expira el token.
                ExpirerToken = ExpirerToken.AddSeconds(double.Parse(claims[4].Value)).ToLocalTime();

                // Setteamos el valor del token que nos han pasado.
                token = value;
            }
        }

        /// <summary>
        /// Método que conecta con el servidor y autentifica al usuario, la respuesta esperada
        /// será un 200 OK y se recibirá el token en la cabecera de Autenticación.
        /// </summary>
        /// <param name="user">El objeto de usuario.</param>
        public void LoginUser(User user) {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.POST);

            // Creamos un objeto solo con el nombre de usuario y la contraseña.
            string jsonRequest = JsonConvert.SerializeObject(new { 
                username = user.Username,
                password = user.Password
            });

            // Se añade el json al cuerpo de la petición.
            request.AddJsonBody(jsonRequest);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (response.IsSuccessful)
            {
                // En caso de que el servidor nos haya devuelto un token, nos lo guardaremos.
                Token = response.Headers.ToList().Find(x => x.Name == "Authorization").Value.ToString();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                    // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                    throw new ExpiredLoginException("El usuario y contraseña no son correctos.");
                else
                {
                    // En caso de que se haya producido un error en el servidor, mostramos el mensaje en el cliente.
                    // Procesamos la respuesta y obtenemos el mensaje.
                    dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    string message = j["message"].ToString();

                    // Lanzamos una excepción con el mensaje que ha dado el servidor.
                    throw new ServerException("Se ha producido un error: " + message);
                }
            }
        }

        /// <summary>
        /// Método para, a partir de la respuesta de una petición, se pueda determinar la causa
        /// del error que luego será capturada por el controlador.
        /// </summary>
        /// <param name="response">Respuesta de la petición.</param>
        /// <param name="defaultMessage">Mensaje por defecto.</param>
        public static void CalculateException(IRestResponse response, string defaultMessage)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
                // En caso de que no se haya encontrado el usuario, lanzamos un mensaje personalizado.
                throw new NotFoundException(defaultMessage);
            else if (response.StatusCode == HttpStatusCode.BadRequest)
                // En caso de que la petición no sea valida, lanzamos un mensaje personalizado.
                throw new BadRequestException("La petición realizada no es valida.");
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                if (token != null)
                {
                    // Hacer un diff del tiempo actual con el de expiración y comprobar si es admin.
                    if (User.IsAdmin == false && (DateTime.Now.Ticks - ExpirerToken.Ticks) < 0)
                        // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                        throw new AdminForbiddenException("No tienes autorización a realizar esta operación.");
                    else if (User.IsAdmin == true && (DateTime.Now.Ticks - ExpirerToken.Ticks) < 0)
                        // Es sospechoso esto, lo más probable es que se hayan deshabilitado remotamente.
                        throw new AdminForbiddenException("Se ha producido un error, tal vez, en el servidor se hayan deshabilitado los privilegios de administrador para su usuario.");
                    else if (Token != null && (DateTime.Now.Ticks - ExpirerToken.Ticks) > 0)
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
                throw new ServerException("Se ha producido un error: " + message);
            }
        }
    }
}
