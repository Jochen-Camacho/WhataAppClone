using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Models;

namespace WhatsAppCloneMainProject.Services
{
    public class DataService
    {
        private readonly HttpClientHandler _httpClientHandler;
        private readonly HttpClient _httpClient;

        public DataService()
        {
            _httpClientHandler = new HttpClientHandler { CookieContainer = new CookieContainer() };
            _httpClient = new HttpClient(_httpClientHandler) { BaseAddress = new Uri("http://localhost:5292") };
        }


        public async Task<ApiResponse> LoginUserAsync(LoginDto loginDto)
        {
            var json = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/User/login", content);
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {

                var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseMessage);

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = responseData["message"].ToString(),
                    UserId = responseData["userId"],
                    UserName = responseData["userName"]
                };
            }
            else
            {
                return new ApiResponse { IsSuccess = false, Message = responseMessage };
            }
        }

        public async Task<List<Contact>> GetUserContacts()
        {
            var response = await _httpClient.GetAsync("/Contact/get_contacts");

            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();

            var contacts = JsonConvert.DeserializeObject<List<Contact>>(responseMessage);

            MessageBox.Show($"Received {contacts.Count} contacts.");

            return contacts;
        }



        public async Task<List<Message>> GetUserMessages(string recipId)
        {
            var chatDto = new ChatDto { RecipientId = recipId };

            var json = JsonConvert.SerializeObject(chatDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Message/get_messages_by_user", content);

            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();

            var messages = JsonConvert.DeserializeObject<List<Message>>(responseMessage);

            MessageBox.Show($"Received {messages.Count} messages.");

            return messages;
        }
    }
}
