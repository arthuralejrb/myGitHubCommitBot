using GitHubCommitBot._bot;
using GitHubCommitBot._services;
using GitHubCommitBot._messageHandler;

namespace GitHubCommitBot
{
    class Program
    {
        static async Task Main (string[] args)
        {

            var botToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrEmpty(botToken))
            {
                Console.WriteLine("ERROR: TELEGRAM_TOKEN NOT CONFIGURED!");
                Console.WriteLine("Configure with: export TELEGRAM_TOKEN='your token'");
                return;

            }

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("ERROR: CONNECTION_STRING NOT CONFIGURED!");
                Console.WriteLine("Configure with: export CONNECTION_STRING='your connection string'");
                return;
                
            }


            GitHubServiceFactory gitFactory =  new GitHubServiceFactory(); // responsible for working with the OctoKit API
            MessageHandler message = new MessageHandler(gitFactory, connectionString); // will work with the bot to get the user's message, and decide what to do with it
            TelegramBot bot = new TelegramBot(botToken, message); // represents the telegram bot, will first receive the user's message 

            bot.RunBot();

            Console.WriteLine("Bot running! press enter to stop");
            Console.ReadLine();
            bot.StopBot();
        
        }
    }
}