﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIClient
{
    public class GithubClient : BaseClient
    {
        public GithubClient(string uri, string username, string password) :base(uri, username, password)
        {   }
        public override async Task Authenticate()
        {
            Console.WriteLine("Authenticating user...\n");
            var uri = new Uri(_uri);
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", _username),
                    new KeyValuePair<string, string>("password", _password)
                });

                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                _authenticated = true;

                Console.WriteLine(
                    $"---------------------\n" +
                    $"|User authenticated!|\n" +
                    $"---------------------\n");

                _cookieContainer.GetCookies(uri).Cast<Cookie>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nSomething went wrong!");
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        public override async Task<string> GetAsync(string endpoint)
        {
            var token = "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";
            Uri uri = new Uri(_uri + endpoint);

            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "mirth-tool-github-client");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                Console.WriteLine("Retriving data - status:");
                HttpResponseMessage response = await _httpClient.GetAsync(_uri + endpoint);
                
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nSomething went wrong!");
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public async Task<string> PostAsync(string endpoint)
        {
            var token = "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";

            try
            {
               
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "mirth-tool-github-client");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                Console.WriteLine("Retriving data - status:");
                HttpResponseMessage response = await _httpClient.GetAsync(_uri + endpoint);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nSomething went wrong!");
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public override Task<string> OptionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}