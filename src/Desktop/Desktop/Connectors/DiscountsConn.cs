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
    class DiscountsConn
    {
        private readonly string mauzoUrl = Settings.Default.MauzoServer + "/api/discounts"; // TODO => Minuscula
        private readonly string token = LoginConn.Token;

        public List<Discount> List
        {
            get
            {
                Uri baseUrl = new Uri(mauzoUrl + "/");
                IRestClient client = new RestClient(baseUrl);
                IRestRequest request = new RestRequest(Method.GET);

                request.AddHeader("Authorization", token);

                IRestResponse response = client.Execute(request);

                List<Discount> discounts = null;

                if (response.IsSuccessful)
                    discounts = JsonConvert.DeserializeObject<List<Discount>>(response.Content);
                else
                    LoginConn.CalculateException(response, "No se ha encontrado el descuento");

                return discounts;
            }
        }

        public void Add(Discount discount)
        {
            Uri baseUrl = new Uri(mauzoUrl + "/");
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", token);

            string jsonRequest = JsonConvert.SerializeObject(new { 
                id = discount.Id,
                codeDisc = discount.Code,
                descDisc = discount.Desc,
                pricePerc = discount.PricePerc
            });

            request.AddJsonBody(jsonRequest);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado el descuento");
        }

        public Discount Get(int id) // TODO => Sergio
        {
            Uri baseUrl = new Uri(mauzoUrl + "/" + id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token);

            IRestResponse response = client.Execute(request);

            Discount discount = null;

            if (response.IsSuccessful)
                discount = JsonConvert.DeserializeObject<Discount>(response.Content);
            else
                LoginConn.CalculateException(response, "No se ha encontrado el descuento.");

            return discount;
        }

        public void Modify(Discount discount)
        {
            Uri baseUrl = new Uri(mauzoUrl + "/" + discount);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.PUT);

            request.AddHeader("Authorization", token);

            String jsonRequest = JsonConvert.SerializeObject(new
            {
                id = discount.Id,
                codeDisc = discount.Code,
                descDisc = discount.Desc,
                pricePerc = discount.PricePerc
            });

            request.AddJsonBody(jsonRequest);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado el descuento");
        }

        public void Delete(Discount discount)
        {
            Uri baseUrl = new Uri(mauzoUrl + "/" + discount.Id);
            IRestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest(Method.DELETE);

            request.AddHeader("Authorization", token);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                LoginConn.CalculateException(response, "No se ha encontrado el descuento");
        }
    }
}
