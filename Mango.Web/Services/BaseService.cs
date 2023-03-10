using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            responseModel = new ResponseDto();
        }

        public async Task<T> SendAsync<T>(ApiRequest request)
        {
            try
            {
                var client = HttpClientFactory.CreateClient("MangoAPI");
                var message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                client.DefaultRequestHeaders.Clear();
                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                }

                switch (request.ApiType)
                {
                    case SD.ApiType.POST:
                        {
                            message.Method = HttpMethod.Post;
                            break;
                        }
                    case SD.ApiType.PUT:
                        {
                            message.Method = HttpMethod.Put;
                            break;
                        }
                    case SD.ApiType.DELETE:
                        {
                            message.Method = HttpMethod.Delete;
                            break;
                        }
                    default:
                        {
                            message.Method = HttpMethod.Get;
                            break;
                        }
                }

                var apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message.ToString() },
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
