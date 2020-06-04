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
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Desktop.Connectors
{
    /// <summary>
    /// Clase para gestionar los descuentos almacenados en el
    /// servidor de API RESTful escrito en java.
    /// </summary>
    /// <remarks>@ant04x Antonio Izquierdo</remarks>
    class DiscountsConn
    {
        private readonly string mauzoUrl = Settings.Default.MauzoServer + "/api/discounts"; // TODO => Minuscula
        private readonly string token = LoginConn.Token;

        /// <summary>
        /// Propiedad para obtener todos los descuentos del servidor.
        /// </summary>
        public List<Discount> List
        {
            get
            {
                // Iniciamos la conexión.
                Uri baseUrl = new Uri(mauzoUrl + "/");
                IRestClient client = new RestClient(baseUrl);
                IRestRequest request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

                IRestResponse response = client.Execute(request); // Ejecutamos la petición.

                List<Discount> discounts = null; // Inicializamos la lista de descuentos

                // Procesamos el objeto de venta
                if (response.IsSuccessful)
                    discounts = JsonConvert.DeserializeObject<List<Discount>>(response.Content);
                else
                    LoginConn.CalculateException(response, "No se ha encontrado el descuento");

                return discounts; // Devolvemos el objeto
            }
        }

        /// <summary>
        /// Método para añadir un descuento a la base de datos, a traves de la API RESTful.
        /// </summary>
        /// <param name="discount">El objeto de descuento.</param>
        public void Add(Discount discount)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

            // Convertimos el descuento a json y lo incorporamos a la petición.
            string jsonRequest = JsonConvert.SerializeObject(new { 
                id = discount.Id,
                codeDisc = discount.Code,
                descDisc = discount.Desc,
                pricePerc = discount.PricePerc
            });

            request.AddJsonBody(jsonRequest); // Se añade el json al cuerpo de la petición.

            IRestResponse response = client.Execute(request); // Ejecutamos la petición.

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado el descuento");
        }

        /// <summary>
        /// Método para obtener del servidor, a partir del id, un objeto de descuento.
        /// </summary>
        /// <param name="id">El ID del descuento.</param>
        /// <returns>Objeto del descuento.</returns>
        public Discount Get(int id)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

            IRestResponse response = client.Execute(request); // Ejecutamos la petición.

            Discount discount = null; // Inicializamos el descuento

            // Procesamos el objeto del descuento
            if (response.IsSuccessful)
                discount = JsonConvert.DeserializeObject<Discount>(response.Content);
            else
                LoginConn.CalculateException(response, "No se ha encontrado el descuento.");

            return discount; // Devolvemos el objeto
        }

        /// <summary>
        /// Método que modifica un descuento en el servidor.
        /// </summary>
        /// <param name="discount">El objeto de descuento.</param>
        public void Modify(Discount discount)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + discount);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.PUT);

            request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

            // Convertimos el descuento a json y lo incorporamos a la petición.
            String jsonRequest = JsonConvert.SerializeObject(new
            {
                id = discount.Id,
                codeDisc = discount.Code,
                descDisc = discount.Desc,
                pricePerc = discount.PricePerc
            });

            request.AddJsonBody(jsonRequest); // Se añade el json al cuerpo de la petición.

            IRestResponse response = client.Execute(request); // Ejecutamos la petición.

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado el descuento");
        }

        /// <summary>
        /// Método que elimina a un descuento en el servidor.
        /// </summary>
        /// <param name="discount">El objeto de descuento.</param>
        public void Delete(Discount discount)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + discount.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.DELETE);

            request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

            IRestResponse response = client.Execute(request); // Ejecutamos la petición.

            // Procesamos la respuesta de la petición.
            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado el descuento");
        }
    }
}
