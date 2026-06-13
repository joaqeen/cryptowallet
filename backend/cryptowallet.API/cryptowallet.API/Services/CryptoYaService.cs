using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;
using System;
using System.Text.Json;

namespace cryptowallet.API.Services
{
        public class CryptoYaService : ICryptoYaService
        {
            private readonly HttpClient _httpClient;

            public CryptoYaService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<decimal> GetPrice (string cryptoCode, string action)
            {
                string url = $"https://criptoya.com/api/binance/{cryptoCode}/ars";
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<CryptoYaDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (action == "purchase")
                {
                    return data.TotalAsk;
                } if (action == "sale")
                {
                    return data.TotalBid;
                }else
                {
                    throw new ArgumentException("Acción inválida");
                }
            }
        }
    }

