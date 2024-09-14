
class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string apiUrl = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd";

        try
        {
            var response = await client.GetStringAsync(apiUrl);
            Console.WriteLine("Preço atual de Bitcoin e Ethereum (em USD):");
            Console.WriteLine(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar a API: {ex.Message}");
        }
    }
}