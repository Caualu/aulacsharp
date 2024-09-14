class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Digite o numero do Ataque: ");
        string pokemon = Console.ReadLine()?.ToLower();

        try
        {
            string respostaPokemon = await ObterDadosPokemon(pokemon);
            Console.WriteLine(respostaPokemon);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter dados do Pokémon: {ex.Message}");
        }
    }

    static async Task<string> ObterDadosPokemon(string pokemon)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://pokeapi.co/api/v2/move/{pokemon}";
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                string Ataque = json["name"].ToString();
                string Precisão = json["accuracy"].ToString();


                return $"ataque: {Ataque}\nprecisão: {Precisão} ";
            }
            else
            {
                throw new Exception("Não foi possível obter os dados do Pokémon.");
            }
        }
    }
}
