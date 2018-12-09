using Ajj.Core.Configuration;
using Ajj.Core.Entities;
using Ajj.Core.Entities.GBUserSyncAPI;
using Ajj.Core.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Ajj.Infrastructure.Services
{
    public class APICallingService : IAPICallingService
    {
        private readonly GBAPISettings _apiSettings;

        public APICallingService(GBAPISettings apiSettings,
            IJobSeekerRepository jobSeekerRepository)
        {
            _apiSettings = apiSettings;
        }

        //public IResponse CreateUserByGB_Bak(JobSeeker jobseeker)
        //{
        //    try
        //    {
        //        var client = new JsonServiceClient(_apiSettings.BaseUrl);
        //        GBUser gbuser = new GBUser(jobseeker);
        //        var updateResult = client.Post<string>("/user_create_sync.php", gbuser);
        //        var dtoupdate = updateResult.FromJson<Response>();
        //        return dtoupdate;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response { Result = "ERROR", Error = ex.Message };
        //    }
        //}

        public IResponse GetUserFromGB(string email)
        {
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(_apiSettings.BaseUrl);
                var request = new RestRequest($"get_userinfo.php?user_email={email}", Method.GET);

                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("cache-control", "no-cache");
                IRestResponse<Response> response = client.Execute<Response>(request);
                return JsonConvert.DeserializeObject<Response>(response.Content);

            }
            catch (Exception ex)
            {
                return new Response { Result = "ERROR", Error = ex.Message };
            }
        }

        public IResponse CreateUserInGB(JobSeeker jobseeker)
        {
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(_apiSettings.BaseUrl);
                var request = new RestRequest("user_create_sync.php", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("cache-control", "no-cache");
                GBUser gbuser = new GBUser(jobseeker);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(gbuser);
                //Alternate way of sending request object
                //request.AddParameter("",JsonConvert.SerializeObject(gbuser),ParameterType.RequestBody);
                IRestResponse<Response> response = client.Execute<Response>(request);
                return JsonConvert.DeserializeObject<Response>(response.Content);
                
            }
            catch (Exception ex)
            {
                return new Response { Result = "ERROR", Error = ex.Message };
            }
        }

        public IResponse ResetPasswordInGB(string email, string password)
        {
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(_apiSettings.BaseUrl);
                var request = new RestRequest("user_reset_password.php", Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("user_email", email);
                request.AddParameter("password", password);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<Response>(response.Content);
            }
            catch (Exception ex)
            {
                return new Response { Result = "ERROR", Error = ex.Message };
            }
        }

        public IResponse UpdateUserInGB(JobSeeker jobseeker)
        {
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(_apiSettings.BaseUrl);
                var request = new RestRequest("user_update_sync.php", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("cache-control", "no-cache");
                GBUser gbuser = new GBUser(jobseeker);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(gbuser);
                //Alternate way of sending request object
                //request.AddParameter("",JsonConvert.SerializeObject(gbuser),ParameterType.RequestBody);
                IRestResponse<Response> response = client.Execute<Response>(request);
                return JsonConvert.DeserializeObject<Response>(response.Content);

            }
            catch (Exception ex)
            {
                return new Response { Result = "ERROR", Error = ex.Message };
            }
        }

        public IResponse UpdatePasswordInGB(string userEmail, string passwordOld, string passwordNew)
        {
            try
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(_apiSettings.BaseUrl);
                var request = new RestRequest("user_update_password.php", Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("user_email", userEmail);
                request.AddParameter("pass_n", passwordNew);
                request.AddParameter("pass_o", passwordOld);
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<Response>(response.Content);
            }
            catch (Exception ex)
            {
                return new Response { Result = "ERROR", Error = ex.Message };
            }
        }

      
    }
}