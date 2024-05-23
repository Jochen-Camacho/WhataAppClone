using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Utility;

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

                byte[] userImageBytes = Convert.FromBase64String(responseData["userImage"]);

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = responseData["message"].ToString(),
                    UserId = responseData["userId"],
                    UserName = responseData["userName"],
                    UserImage = ImageUtil.BytesToImageSource(userImageBytes)
                };
            }
            else
            {
                return new ApiResponse { IsSuccess = false, Message = responseMessage };
            }
        }

        public async Task<ApiResponse> RegisterUserAsync(RegisterDto registerDto)
        {
            var json = JsonConvert.SerializeObject(registerDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/User/register", content);
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseMessage);

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = responseData["message"].ToString()
                };
            }
            else
            {
                return new ApiResponse { IsSuccess = false, Message = responseMessage };
            }
        }

        public async Task<List<List<Contact>>> GetUserContacts()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Contact/get_contacts");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"API call failed with status code: {response.StatusCode}");
                    return null;
                }

                var responseMessage = await response.Content.ReadAsStringAsync();

                var contactsList = JsonConvert.DeserializeObject<List<List<Contact>>>(responseMessage);

                if (contactsList == null || !contactsList.Any())
                {
                    MessageBox.Show("Deserialization returned null or empty list");
                    return null;
                }

                return contactsList;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Message>> GetUserMessages(ChatDto chatDto)
        {

            var json = JsonConvert.SerializeObject(chatDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Message/get_messages_by_user", content);

            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();

            var messages = JsonConvert.DeserializeObject<List<Message>>(responseMessage);

            // MessageBox.Show($"Received {messages.Count} messages.");

            return messages;
        }

        public async Task<ApiResponse> SendMessage(MessageDto messageDto)
        {
            var json = JsonConvert.SerializeObject(messageDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Message/send_message", content);
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseMessage);

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = responseData["message"].ToString()
                };
            }
            else
            {
                return new ApiResponse { IsSuccess = false, Message = responseMessage };
            }
        }

        public async Task<ApiResponse> AddContact(ContactDto contactDto)
        {
            var json = JsonConvert.SerializeObject(contactDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Contact/add_contact", content);
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = responseMessage
                };
            }
            else
            {
                return new ApiResponse { IsSuccess = false, Message = responseMessage };
            }
        }

        public async Task<ApiResponse> UploadImage(string imagePath)
        {
            byte[] imageBytes = ImageUtil.ImageConverter(imagePath);

            var json = JsonConvert.SerializeObject(imageBytes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/User/upload_image", content);
            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = responseMessage
                };
            }
            else
            {
                return new ApiResponse { IsSuccess = false, Message = responseMessage };
            }
        }
    }
}
