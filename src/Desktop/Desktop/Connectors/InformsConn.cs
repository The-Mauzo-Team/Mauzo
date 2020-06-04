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
    /// Clase para gestionar los informes almacenados en el
    /// servidor de API RESTful escrito en java.
    /// </summary>
    /// <remarks>@ant04x Antonio Izquierdo</remarks>
    class InformsConn
    {
        private readonly string mauzoUrl = Settings.Default.MauzoServer + "/api/informs";
        private string token = LoginConn.Token;

        /// <summary>
        /// Propiedad para obtener todos los informes del servidor.
        /// </summary>
        public List<Inform> List
        {
            get
            {
                // Iniciar la conexión.
                Uri baseUrl = new Uri(mauzoUrl + "/");
                IRestClient client = new RestClient(baseUrl);
                IRestRequest request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

                IRestResponse response = client.Execute(request); // Ejecutamos la petición.

                List<Inform> informs = null; // Inicializamos la lista de informes.

                // Procesamos el objeto de informe
                if (response.IsSuccessful)
                    informs = JsonConvert.DeserializeObject<List<Inform>>(response.Content);
                else
                    LoginConn.CalculateException(response, "No se ha encontrado el informe.");

                return informs; // Devolvemos el objeto
            }
        }

        /// <summary>
        /// Método para obtener del servidor, a partir del id, un objeto de informe.
        /// </summary>
        /// <param name="id">El ID del informe.</param>
        /// <returns>Objeto del informe.</returns>
        public Inform Get(int id)
        {
            // Iniciamos la conexión.
            Uri baseUrl = new Uri(mauzoUrl + "/" + id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token); // Agregamos la autorización de token en el header.

            IRestResponse response = client.Execute(request); // Ejecutamos la petición.

            Inform inform = null; // Inicializamos la venta

            // Procesamos el objeto del informe
            if (response.IsSuccessful)
                inform = JsonConvert.DeserializeObject<Inform>(response.Content);
            else
                LoginConn.CalculateException(response, "No se ha encontrado el informes.");

            return inform; // Devolvemos el objeto
        }
    }
}
