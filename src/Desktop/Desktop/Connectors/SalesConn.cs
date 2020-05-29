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
using Desktop.Properties;
using Desktop.Templates;

using System;
using System.Net;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

namespace Desktop.Connectors
{
    class SalesConn
    {
        private string MauzoUrl = Settings.Default.MauzoServer + "/api/sales";
        private string token;

        public void AddSale(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.POST);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Convertimos la venta a json y lo incorporamos a la petición.
            String jsonRequest = JsonConvert.SerializeObject(new { 
                stampRef = sale.StampRef,
                userId = sale.UserId,
                prodId = sale.ProdId,
                discId = sale.DiscId
            });

            // Se añade el json al cuerpo de la petición.
            request.AddJsonBody(jsonRequest);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                // En caso de que no se haya encontrado el usuario, lanzamos un mensaje personalizado.
                    throw new Exception("No se ha encontrado el usuario.");
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                    // En caso de que la petición no sea valida, lanzamos un mensaje personalizado.
                    throw new Exception("La petición realizada no es valida.");
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                    // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                    throw new Exception("No tienes autorización a realizar esta operación.");
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
        }

        public Sale GetSale(int Id) 
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/" + Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.GET);

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
                if (response.StatusCode == HttpStatusCode.NotFound)
                    // En caso de que no se haya encontrado el usuario, lanzamos un mensaje personalizado.
                    throw new Exception("No se ha encontrado el usuario.");
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                    // En caso de que la petición no sea valida, lanzamos un mensaje personalizado.
                    throw new Exception("La petición realizada no es valida.");
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                    // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                    throw new Exception("No tienes autorización a realizar esta operación.");
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

            // Devolvemos el objeto
            return sale;
        }

        public List<Sale> GetSalesList()
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.GET);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Inicializamos la lista de ventas
            List<Sale> sales = null;

            // Procesamos el objeto de venta
            if (response.IsSuccessful)
                sales = JsonConvert.DeserializeObject<List<Sale>>(response.Content);
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    // En caso de que no se haya encontrado el usuario, lanzamos un mensaje personalizado.
                    throw new Exception("No se ha encontrado el usuario.");
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                    // En caso de que la petición no sea valida, lanzamos un mensaje personalizado.
                    throw new Exception("La petición realizada no es valida.");
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                    // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                    throw new Exception("No tienes autorización a realizar esta operación.");
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

            // Devolvemos el objeto
            return sales;
        }

        public void ModifySale(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(MauzoUrl + "/" + sale.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.PUT);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Convertimos el usuario a json y lo incorporamos a la petición.
            String jsonRequest = JsonConvert.SerializeObject(new
            {
                stampRef = sale.StampRef,
                userId = sale.UserId,
                prodId = sale.ProdId,
                discId = sale.DiscId
            });

            // Se añade el json al cuerpo de la petición.
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
            IRestRequest request = new RestRequest(Method.DELETE);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    // En caso de que no se haya encontrado el usuario, lanzamos un mensaje personalizado.
                    throw new Exception("No se ha encontrado el usuario.");
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                    // En caso de que la petición no sea valida, lanzamos un mensaje personalizado.
                    throw new Exception("La petición realizada no es valida.");
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                    // En caso de que el usuario no tenga autorización, lanzamos un mensaje personalizado.
                    throw new Exception("No tienes autorización a realizar esta operación.");
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
        }
    }
}
