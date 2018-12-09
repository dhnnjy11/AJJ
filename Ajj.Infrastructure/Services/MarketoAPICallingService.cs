using Ajj.Core.Configuration;
using Ajj.Core.Entities;
using Ajj.Core.Entities.MarketoAPI;
using Ajj.Core.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Ajj.Infrastructure.Services
{
    public class MarketoAPICallingService : IMaketoAPICallingService
    {
        private readonly MarketoApiSettings _apiSettings;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public MarketoAPICallingService(MarketoApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
            _clientId = apiSettings.ClientId;
            _clientSecret = apiSettings.ClientSecret;
        }

        private string GetToken()
        {
            var client = new RestClient(_apiSettings.BaseUrl);
            client.BaseUrl = new Uri(_apiSettings.BaseUrl);
            var request = new RestRequest($"identity/oauth/token?grant_type=client_credentials&client_id={_clientId}&client_secret={_clientSecret}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            var response = client.Execute(request);
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            return dict["access_token"];
        }

        public MarketoResponse CreateUpdateLead(IMarketoLead lead) //MarketoResponse
        {
            string result = GetToken();
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(_apiSettings.BaseUrl);
                string accessToken = GetToken();
                var request = new RestRequest($"rest/v1/leads.json?access_token={accessToken}", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("cache-control", "no-cache");
                
                
                request.RequestFormat = DataFormat.Json;
                request.AddBody(lead);
                //Alternate way of sending request object
                //request.AddParameter("",JsonConvert.SerializeObject(gbuser),ParameterType.RequestBody);
                IRestResponse<MarketoResponse> response = client.Execute<MarketoResponse>(request);
                return JsonConvert.DeserializeObject<MarketoResponse>(response.Content);

            }
            catch (Exception ex)
            {
                return new MarketoResponse { };
            }
        }
    }
}