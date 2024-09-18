using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Microsoft.VisualBasic;

class Program
{
    private static readonly string BotToken = "7921980738:AAEmY1sfNNbQRuF7yE7ZRBz9FeHpRxoBm-c";
    private static TelegramBotClient botClient;

    // Lista de produtos
    private static Dictionary<string, string> produtos = new Dictionary<string, string>
    {
        { "produto1", "Pink Lemonad: Seção de Bebidas, Prateleira 3A, Código 1001" },
        { "produto2", "Royal Plant Barbecue: Seção de Alimentos Veganos, Prateleira 5B, Código 1002" },
        { "produto3", "Tilapia Filet: Seção de Peixes e Frutos do Mar, Prateleira 2C, Código 1003" }
    };

    // Cardápio do restaurante
    private static List<string> cardapio = new List<string>
    {
        "Menu Principal: All Ribs",
        "Menu Principal: Chop Brahma e Aperivos",
        "Menu Principal: Double Ribs"
    };
    private static List<string> localização = new List<string>
    {
          "Shopping XYZ: Outback Steakhouse: Endereço: Rua A, Número 123" ,
          "Avenida Central: Outback Bar & Grill: Endereço: Avenida B, Número 456" ,
          "Parque da Cidade: Outback Express: Endereço: Rua C, Número 789" 
    };

    static void Main(string[] args)
    {
        botClient = new TelegramBotClient(BotToken);

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions);

        Console.WriteLine("Bot iniciado. Pressione qualquer tecla para parar o bot...");
        Console.ReadKey();

        botClient.CloseAsync().Wait();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message && update.Message!.Text != null)
        {
            var message = update.Message;
            Console.WriteLine($"Recebi uma mensagem de {message.Chat.FirstName}: {message.Text}");

            string resposta = ProcessarMensagem(message.Text.ToLower());

            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: resposta
            );
        }
    }

    private static string ProcessarMensagem(string mensagem)
    {
        if (mensagem.Contains("produtos"))
        {
            return "Aqui estão nossos produtos:\n" + string.Join("\n", produtos.Values);
        }
        else if (mensagem.Contains("cardápio"))
        {
            return "Confira nosso cardápio:\n" + string.Join("\n", cardapio);
        }
        else if (mensagem.Contains("qualidade"))
        {
            return "Nós garantimos a melhor qualidade em nossos produtos.";
        }
        else if (mensagem.Contains("localização"))
        {
            return "Aqui estão nossas localizações:\n" + string.Join("\n", localização);
        }
        else
        {
            return "Desculpe, não consegui entender sua pergunta. Tente perguntar sobre produtos ou o cardápio.";
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Erro na API do Telegram:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
