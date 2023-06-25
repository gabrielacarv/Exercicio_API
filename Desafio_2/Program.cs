using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Desafio_2.DadosMoeda;

namespace Desafio_2
{
    internal class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        static async Task Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================== MENU ===================");
                Console.WriteLine("|                                          |");
                Console.WriteLine("|    1 - Consultar países por moeda        |");
                Console.WriteLine("|    2 - Consultar países por continente   |");
                Console.WriteLine("|    0 - Sair                              |");
                Console.WriteLine("|                                          |");
                Console.WriteLine("============================================");
                Console.Write("Digite o número da opção desejada:");   
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        await ConsultarPaisesPorMoeda();
                        break;

                    case "2":
                        await ConsultarPaisesPorContinente();
                        break;

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Opção inválida, tente novamente!");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Aperte Enter para continuar....");
                        Console.ReadLine();
                        break;
                }
                Console.WriteLine();
                Console.WriteLine();
            }         
        }

        static async Task ConsultarPaisesPorMoeda()
        {
            Console.Clear();
            Console.WriteLine("============= CONSULTAR PAISES POR MOEDA =============");
            Console.Write("Digite a moeda para consulta dos países: ");
            string moeda = Console.ReadLine();

            if (string.IsNullOrEmpty(moeda))
            {
                Console.WriteLine("Moeda inválida.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Aperte Enter para continuar....");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();
            Console.WriteLine();

            string apiUrl = $"https://restcountries.com/v3.1/currency/{moeda}?fields=name";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                Root[] countries = JsonConvert.DeserializeObject<Root[]>(responseBody);

                foreach (var country in countries)
                {
                    Console.WriteLine($"Nome Comum: {country.name.common}");
                    Console.WriteLine($"Nome Oficial: {country.name.official}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Aperte Enter para continuar....");
                Console.ReadLine();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ocorreu um erro ao fazer a solicitação: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Aperte Enter para continuar....");
                Console.ReadLine();
            }         
        }

        static async Task ConsultarPaisesPorContinente()
        {
            Console.Clear();
            Console.WriteLine("============= CONSULTAR PAISES POR CONTINENTE =============");
            Console.Write("Digite o continente para consulta dos países: ");
            string continente = Console.ReadLine();

            if (string.IsNullOrEmpty(continente))
            {
                Console.WriteLine("Continente inválido.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Aperte Enter para continuar....");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();
            Console.WriteLine();

            string apiUrl2 = $"https://restcountries.com/v3.1/region/{continente}?fields=name,capital,currencies";

            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl2);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                Root[] countries = JsonConvert.DeserializeObject<Root[]>(responseBody);

                foreach (var country in countries)
                {
                    Console.WriteLine($"Nome Comum: {country.name.common}");
                    Console.WriteLine($"Nome Oficial: {country.name.official}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Aperte Enter para continuar....");
                Console.ReadLine();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ocorreu um erro ao fazer a solicitação: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Aperte Enter para continuar....");
                Console.ReadLine();
            }
        }
    }
}