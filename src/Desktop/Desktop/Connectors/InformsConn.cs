using Desktop.Properties;
using Desktop.Templates;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Connectors
{
    class InformsConn
    {
        private readonly string mauzoUrl = Settings.Default.MauzoServer + "/api/informs";
        private string token = LoginConn.Token;

        public List<Inform> List
        {
            get
            {
                Uri baseUrl = new Uri(mauzoUrl + "/");
                IRestClient client = new RestClient(baseUrl);
                IRestRequest request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", token);

                IRestResponse response = client.Execute(request);

                List<Inform> informs = null;

                if (response.IsSuccessful)
                    informs = JsonConvert.DeserializeObject<List<Inform>>(response.Content);
                else
                    LoginConn.CalculateException(response, "No se ha encontrado el informe.");

                return informs;
            }
        }

        public Inform Get(int id)
        {
            Uri baseUrl = new Uri(mauzoUrl + "/" + id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token);

            IRestResponse response = client.Execute(request);

            Inform inform = null;

            if (response.IsSuccessful)
                inform = JsonConvert.DeserializeObject<Inform>(response.Content);
            else
                LoginConn.CalculateException(response, "No se ha encontrado el informes.");

            return inform;
        }
    }
}
