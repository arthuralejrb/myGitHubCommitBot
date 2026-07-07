namespace GitHubCommitBot._models
{
    class UserConfig {
        public long TelegramChatID {get; set;}
        public string GitHubUser {get; set;}
        public string UserRepository {get;set;}
        public string RepositoryBranch {get;set;}
        public string UserToken {get; set;} 

        public UserConfig()
        {
            TelegramChatID = 0;
            GitHubUser = "";
            UserRepository = "";
            RepositoryBranch = "" ;
            UserToken = "";
        
        }

        public UserConfig(long telegramChatID, string githubUser, 
                          string userRepository, string repositoryBranch, string userToken)
        {
            TelegramChatID = telegramChatID;
            GitHubUser = githubUser;
            UserRepository = userRepository;
            RepositoryBranch = repositoryBranch;
            UserToken = userToken;

        }

    }
}