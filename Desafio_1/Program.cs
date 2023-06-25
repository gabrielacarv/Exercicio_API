using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("ipapi.co/#c-sharp-v1.03");

                try
                {
                    string url = "https://ipapi.co/json/";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Dados data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dados>(responseBody);

                    Console.WriteLine($"IP: {data.ip}");
                    Console.WriteLine($"Rede: {data.network}");
                    Console.WriteLine($"Versão: {data.version}");
                    Console.WriteLine($"Cidade: {data.city}");
                    Console.WriteLine($"Região: {data.region}");
                    Console.WriteLine($"Código da Região: {data.region_code}");
                    Console.WriteLine($"País: {data.country}");
                    Console.WriteLine($"Nome do País: {data.country_name}");
                    Console.WriteLine($"Código do País: {data.country_code}");
                    Console.WriteLine($"Código ISO3 do País: {data.country_code_iso3}");
                    Console.WriteLine($"Capital do País: {data.country_capital}");
                    Console.WriteLine($"TLD do País: {data.country_tld}");
                    Console.WriteLine($"Código do Continente: {data.continent_code}");
                    Console.WriteLine($"Na União Europeia: {data.in_eu}");
                    Console.WriteLine($"CEP: {data.postal}");
                    Console.WriteLine($"Latitude: {data.latitude}");
                    Console.WriteLine($"Longitude: {data.longitude}");
                    Console.WriteLine($"Fuso Horário: {data.timezone}");
                    Console.WriteLine($"Deslocamento UTC: {data.utc_offset}");
                    Console.WriteLine($"Código de Chamada do País: {data.country_calling_code}");
                    Console.WriteLine($"Moeda: {data.currency}");
                    Console.WriteLine($"Nome da Moeda: {data.currency_name}");
                    Console.WriteLine($"Idiomas: {data.languages}");
                    Console.WriteLine($"Área do País: {data.country_area}");
                    Console.WriteLine($"População do País: {data.country_population}");
                    Console.WriteLine($"ASN: {data.asn}");
                    Console.WriteLine($"ORG: {data.org}");

                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao fazer a requisição HTTP: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }
            }

            Console.ReadLine();
        }
    }
}