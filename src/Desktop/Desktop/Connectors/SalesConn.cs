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
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

namespace Desktop.Connectors
{
    /// <summary>
    /// Clase para gestionar las ventas  almacenados en el
    /// servidor de API RESTful escrito en java.
    /// </summary>
    /// <remarks>@Neirth Sergio Martinez</remarks>
    class SalesConn
    {
        private readonly string mauzoUrl = Settings.Default.MauzoServer + "/api/sales";
        private string token = LoginConn.Token;

        /// <summary>
        /// Método para añadir una venta a la base de datos, a traves de la API RESTful.
        /// </summary>
        /// <param name="sale">El objeto de venta.</param>
        public void Add(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.POST);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Convertimos la venta a json y lo incorporamos a la petición.
            string jsonRequest = JsonConvert.SerializeObject(new { 
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
                LoginConn.CalculateException(response, "No se ha encontrado la venta");
        }

        /// <summary>
        /// Método para obtener del servidor, a partir del id, un objeto de venta.
        /// </summary>
        /// <param name="Id">El ID de la venta.</param>
        /// <returns>Objeto de la venta.</returns>
        public Sale Get(int Id) 
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + Id);
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
                sale = JsonConvert.DeserializeObject<Sale>(response.Content);
            else
                LoginConn.CalculateException(response, "No se ha encontrado la venta");

            // Devolvemos el objeto
            return sale;
        }

        /// <summary>
        /// Método para obtener todas las ventas del servidor.
        /// </summary>
        public List<Sale> List
        {
            get 
            {
                // Iniciamos la conexión.
                Uri baseUrl = new Uri(mauzoUrl + "/");
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
                    LoginConn.CalculateException(response, "No se ha encontrado la venta");

                // Devolvemos el objeto
                return sales;
            }
        }

        /// <summary>
        /// Método que modifica una vebta en el servidor.
        /// </summary>
        /// <param name="sale">El objeto </param>
        public void Modify(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + sale.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.PUT);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Convertimos la venta a json y lo incorporamos a la petición.
            string jsonRequest = JsonConvert.SerializeObject(new
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
                LoginConn.CalculateException(response, "No se ha encontrado la venta");
        }

        /// <summary>
        /// Método que elimina a una venta en el servidor.
        /// </summary>
        /// <param name="sale">El objeto de venta.</param>
        public void Delete(Sale sale)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + sale.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.DELETE);

            // Agregamos la autorización de token en el header.
            request.AddHeader("Authorization", token);

            // Ejecutamos la petición.
            IRestResponse response = client.Execute(request);

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado la venta");
        }
    }
}
