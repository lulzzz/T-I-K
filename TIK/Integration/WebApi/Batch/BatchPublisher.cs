﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using TIK.Domain.Jobs;
using TIK.Domain.SearchNews;
using TIK.Integration.Batch;

namespace TIK.Integration.WebApi.Batch
{
    public class BatchPublisher : IBatchPublisher
    {
        private readonly HttpClient _client;
        private readonly string _getAllJobs;
        private readonly string _postSearchNews;

        private readonly Uri _uri;

        public BatchPublisher(Uri uri)
        {
            _uri = uri;
            _client = new HttpClient();
            _client.BaseAddress = uri;
            _getAllJobs = "GetAllJobs";
            _postSearchNews = "SearchNews";
        }

        public Task<IEnumerable<Job>> GetAllJobs()
        {
            IEnumerable<Job> jobs = null;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // HTTP GET
            HttpResponseMessage response = _client.GetAsync(_getAllJobs).Result;
            if (response.IsSuccessStatusCode)
            {
                string data =  response.Content.ReadAsStringAsync().Result;
                jobs = JsonConvert.DeserializeObject<IEnumerable<Job>>(data);
            }


            return Task.FromResult<IEnumerable<Job>>(jobs);  
        }

        public Task<Job> SearchNews(CriteriaSearchNews criteria)
        {

            var client = new RestClient(_uri);

            var request = new RestRequest(_postSearchNews, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", JsonConvert.SerializeObject(criteria), ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content; // raw content as string      

            var job = JsonConvert.DeserializeObject<Job>(content);
            return Task.FromResult<Job>(job);
        }
        /*
        public Task<Job> SearchNews(CriteriaSearchNews criteria)
        {
            Job job = null;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //StringContent content = new StringContent(JsonConvert.SerializeObject(criteria));
            var content = new StringContent(JsonConvert.SerializeObject(criteria),
            Encoding.UTF8, "application/json");
            // HTTP POST
            HttpResponseMessage response = _client.PostAsync(_postSearchNews, content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                job = JsonConvert.DeserializeObject<Job>(data);
            }


            return Task.FromResult<Job>(job);
        }
        */
    }
}
