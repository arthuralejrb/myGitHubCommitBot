namespace GitHubCommitBot._services
{
    class GitHubServiceFactory
    {
        public GitHubService Create (string userName, string repoName, string repoBranch, string token)
        {
            return new GitHubService(userName,repoName, repoBranch, token);

        }
    }
}