using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Digite o país (ex: BR para Brasil): ");
        string pais = Console.ReadLine();

        Console.Write("Digite a cidade (ex: São Paulo): ");
        string cidade = Console.ReadLine();

        // Substitua com sua chave de API do OpenWeatherMap
        string apiKey = "SUA_CHAVE_DE_API";

        try
        {
            string respostaClima = await ObterDadosClima(cidade, pais, apiKey);
            Console.WriteLine(respostaClima);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter dados do clima: {ex.Message}");
        }
    }

    static async Task<string> ObterDadosClima(string cidade, string pais, string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            // URL da API do OpenWeatherMap para obter clima atual
            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m";

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                // Verificar se o JSON contém a estrutura esperada
                JToken main = json["main"];
                JToken weather = json["weather"]?[0];
                JToken temp = main?["temp"];
                JToken description = weather?["description"];

                if (main == null || temp == null || description == null)
                {
                    throw new Exception("Dados climáticos não encontrados.");
                }

                // Construindo a resposta
                string temperatura = temp.ToString();
                string descricao = description.ToString();

                return $"Temperatura atual em {cidade}: {temperatura}°C\nDescrição: {descricao}";
            }
            else
            {
                throw new Exception($"Não foi possível obter os dados do clima. Status Code: {response.StatusCode}");
            }
        }
    }
}
