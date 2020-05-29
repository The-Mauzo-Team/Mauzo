using Desktop.Properties;
using Desktop.Templates;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace Desktop.Connectors
{
    class LoginConn
    {
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
    }
}
