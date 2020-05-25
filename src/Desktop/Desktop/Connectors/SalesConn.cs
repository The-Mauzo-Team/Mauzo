using Desktop.Properties;
using Desktop.Templates;

using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

namespace Desktop.Connectors
{
    class SalesConn
    {
        private string MauzoUrl = Settings.Default.MauzoServer + "/api/users";
        private string token;

        public void AddSale(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("post", Method.POST);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Convertimos la venta a json y lo incorporamos a la petición.
            String jsonRequest = JsonConvert.SerializeObject(sale);
            request.AddJsonBody(jsonRequest);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
            {
                // Procesamos la respuesta y obtenemos el mensaje.
                dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string message = j["message"].ToString();

                // Lanzamos una excepción con el mensaje que ha dado el servidor.
                throw new Exception("Se ha producido un error: " + message);
            }
        }

        public Sale GetSale(int Id) 
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/" + Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("get", Method.GET);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Inicializamos la venta
            Sale sale = null;

            // Procesamos el objeto de la venta
            if (response.IsSuccessful)
            {
                sale = JsonConvert.DeserializeObject<Sale>(response.Content);
            }
            else
            {
                // Procesamos la respuesta y obtenemos el mensaje.
                dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string message = j["message"].ToString();

                // Lanzamos una excepción con el mensaje que ha dado el servidor.
                throw new Exception("Se ha producido un error: " + message);
            }

            // Devolvemos el objeto
            return sale;
        }

        public List<Sale> GetSalesList()
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("get", Method.GET);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Inicializamos la lista de ventas
            List<Sale> sales = null;

            // Procesamos el objeto de venta
            if (response.IsSuccessful)
            {
                sales = JsonConvert.DeserializeObject<List<Sale>>(response.Content);
            }
            else
            {
                // Procesamos la respuesta y obtenemos el mensaje.
                dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string message = j["message"].ToString();

                // Lanzamos una excepción con el mensaje que ha dado el servidor.
                throw new Exception("Se ha producido un error: " + message);
            }

            // Devolvemos el objeto
            return sales;
        }

        public void ModifySale(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/" + sale.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("put", Method.PUT);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Convertimos el usuario a json y lo incorporamos a la petición.
            String jsonRequest = JsonConvert.SerializeObject(sale);
            request.AddJsonBody(jsonRequest);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
            {
                // Procesamos la respuesta y obtenemos el mensaje.
                dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string message = j["message"].ToString();

                // Lanzamos una excepción con el mensaje que ha dado el servidor.
                throw new Exception("Se ha producido un error: " + message);
            }
        }

        public void DeleteUser(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/" + sale.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("delete", Method.DELETE);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
            {
                // Procesamos la respuesta y obtenemos el mensaje.
                dynamic j = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string message = j["message"].ToString();

                // Lanzamos una excepción con el mensaje que ha dado el servidor.
                throw new Exception("Se ha producido un error: " + message);
            }
        }
    }
}
