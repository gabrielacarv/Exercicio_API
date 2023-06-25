using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Desafio_3.Dados;

namespace Desafio_3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================== MENU ===================");
                Console.WriteLine("|                                          |");
                Console.WriteLine("|     1 - Converter de Real para Dolár     |");
                Console.WriteLine("|     2 - Converter de Dolár para Real     |");
                Console.WriteLine("|     0 - Sair                             |");
                Console.WriteLine("|                                          |");
                Console.WriteLine("============================================");
                Console.Write("Digite o número da opção desejada:");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=================== REAL para DOLÁR ===================");
                        decimal Dolar = await Cotacao();
                        decimal Real = Valor();
                        Console.WriteLine($"US${(Real / Dolar) * 10000:F2}");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Aperte Enter para continuar....");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=================== DOLÁR para REAL ===================");
                        Dolar = await Cotacao();
                        Real = Valor();
                        Console.WriteLine($"R$ {(Dolar / Real):F2}");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Aperte Enter para continuar....");
                        Console.ReadLine();
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

        static decimal Valor()
        {
            Console.Write("Digite o valor que deseja converter: ");
            string valor = Console.ReadLine();
            decimal ValorUsuario = decimal.Parse(valor);
            return ValorUsuario;
        }


        static async Task<decimal> Cotacao()
        {
            string apiUrl = "http://economia.awesomeapi.com.br/json/last/USD-BRL";
         
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if ((int)response.StatusCode == 308)
                {
                    string redirectedUrl = response.Headers.Location.ToString();
                    response = await client.GetAsync(redirectedUrl);
                }

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Dados.Root data = JsonConvert.DeserializeObject<Dados.Root>(responseBody);

                Console.WriteLine();
                Console.WriteLine("-------------- Cotação do Dólar em Reais --------------");
                Console.WriteLine("Valor de Compra: R$ " + data.USDBRL.bid);
                Console.WriteLine("Valor de Venda: R$ " + data.USDBRL.ask);
                Console.WriteLine("Data e Hora da Cotação: " + data.USDBRL.create_date);
                Console.WriteLine();
                Console.WriteLine();

                decimal ValorSistema = decimal.Parse(data.USDBRL.bid);
                return ValorSistema;
            }
           
        }
    }
}
