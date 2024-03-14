using Newtonsoft.Json;
using NLog;
using RestSharp;
using ServicesLayer.IWebServices;

namespace ServicesLayer.WebServices
{
    public class WebService : IWebService
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string GetHTTPService(string url)
        {
            var response = string.Empty;
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("Content-Type", "application/json");
                RestResponse result = client.Execute(request);

                if (result.IsSuccessStatusCode)
                {
                    return result.Content;
                    
                }
            }
            catch (Exception e)
            {
                logger.Info($"Error while trying send Request with Server API ::Exception {JsonConvert.SerializeObject(e)} ::URL {JsonConvert.SerializeObject(url)}");
                throw;
            }
            return response;
        }
    }
}
