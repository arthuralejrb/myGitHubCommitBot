using Telegram.Bot;
using Telegram.Bot.Types;
using GitHubCommitBot._messageHandler;

namespace GitHubCommitBot._bot
{
    class TelegramBot
    {
        private TelegramBotClient BotClient {get; set;}
        private CancellationTokenSource Cts {get; set;}
        private MessageHandler Handler {get;set;}

        public TelegramBot(string Token, MessageHandler handler)
        {
            BotClient = new TelegramBotClient(Token);
            Cts = new CancellationTokenSource();
            Handler = handler;
        
        }

        public void RunBot()
        {   
            BotClient.StartReceiving(
                OnUpdateHandler,
                OnErrorHandler,
                cancellationToken: Cts.Token    
            );
        }

        public void StopBot()
        {
            Cts.Cancel();
        }

        // Method that runs after each message the bot receives
        private async Task OnUpdateHandler(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            if(update.Message == null) return;
            long chat = update.Message.Chat.Id;
            var text = update.Message.Text ?? string.Empty;
            
            Console.WriteLine("mensagem");

            await client.SendMessage(chat, $"{await Handler.ProcessMessage(text, chat)}");
        
        }

        // Method that runs when there is an error on running the bot
        private Task OnErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken ct)
        {
            return Task.CompletedTask;

        }
    }
}