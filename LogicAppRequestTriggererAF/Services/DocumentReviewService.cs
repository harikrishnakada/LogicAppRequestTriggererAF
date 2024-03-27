using ApiClientLib;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogicAppRequestTriggererAF.Services
{
    public class DocumentReviewService
    {
        public static string _apiEndpoint { get; private set; }

        private readonly IApiClient _apiClient;
        public DocumentReviewService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task InitiateReviewProcess(object body)
        {
            _apiEndpoint = Environment.GetEnvironmentVariable("LogicAppWorkflows_DOCUMENT-REVIEW-WORKFLOW_API");
            var rm =  new HttpRequestMessage(HttpMethod.Post, _apiEndpoint);
            rm.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            await this._apiClient.SendAsync(rm);
        }
    }
}
