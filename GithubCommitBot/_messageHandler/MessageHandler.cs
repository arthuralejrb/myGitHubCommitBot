using GitHubCommitBot._models;
using GitHubCommitBot._services;
using GitHubCommitBot._database;

namespace GitHubCommitBot._messageHandler
{
    class MessageHandler
    {
        private GitHubServiceFactory Git {get; set;}
        private DataBase Database {get; set;}

        public MessageHandler(GitHubServiceFactory git, String connectionString)
        {
            Git = git;
            Database = new DataBase(connectionString);

        }

        // Method that properly processess the user message
        async public Task<string> ProcessMessage(string message, long chatID)
        {
            
            string answer = "";

            if(string.IsNullOrWhiteSpace(message))
            {
                answer = "Empty message!";
                return answer;
            }
            
            string[] splitMessage = message.Split(' ');

            if (splitMessage[0] == "/start") //starts the bot
            {
                answer = "Bot started! to configure, type: /config 'repository owner' 'repository name' 'repository branch' 'user token'";
                
            }else if (splitMessage[0] == "/config") // configures the user
            {

                if(splitMessage.Count() - 1 != 4)
                {
                    answer = "Invalid /config arguments";

                }else
                {
                    string NewConfigUser = splitMessage[1];
                    string NewConfigRepo = splitMessage[2];
                    string NewConfigBranch = splitMessage[3];
                    string NewConfigToken = splitMessage[4];
                    
                    var NewUser = new UserConfig(
                        chatID,
                        NewConfigUser, 
                        NewConfigRepo, 
                        NewConfigBranch, 
                        NewConfigToken
                    );

                    await Database.Post(NewUser);
                    answer = $"User {NewUser.GitHubUser} configured successfully";

                }

            }
            else //makes a commit
            {
                UserConfig CommitUser = await Database.GetUserConfig(chatID);
            
                var git = Git.Create(CommitUser.GitHubUser, CommitUser.UserRepository, CommitUser.RepositoryBranch, CommitUser.UserToken);
                string commitUrl = await git.MakeCommit(message);
                answer = $"Commit done ✅ URL: {commitUrl}";
                
            }

            return answer;
        }  
    }
}