using Octokit;

namespace GitHubCommitBot._services
{
    class GitHubService
    {
        private GitHubClient GitClient {get; set;}
        private string RepoOwner {get;set;}
        private string RepoName {get; set;}
        private string RepoBranch {get;set;}
        

        public GitHubService(string repoOwner, string repoName, string repoBranch, string token)
        {
            GitClient = new GitHubClient(new ProductHeaderValue("GitHubCommitBot"));
            
            var tokenAuth = new Credentials(token);
            GitClient.Credentials = tokenAuth;

            RepoOwner = repoOwner;
            RepoName = repoName;
            RepoBranch = repoBranch;
            
        }

        async public Task<string> MakeCommit(string commitContent)
        {

            Console.WriteLine("commmitando");
            try
            {
                var repoContents = await GitClient.Repository.Content.GetAllContents(RepoOwner,RepoName, "commit.txt");
                var shaCode = repoContents.First().Sha;

                UpdateFileRequest updateRequest = new UpdateFileRequest("message",commitContent,shaCode);
                
                var updateResponse =  await GitClient.Repository.Content.UpdateFile(RepoOwner, RepoName, "commit.txt", updateRequest);
                string CommitUrl = updateResponse.Commit.Url;
                
                return CommitUrl;
            }catch(NotFoundException)
            {

                var createRequest = new CreateFileRequest("creating file", commitContent, RepoBranch);
                var createResponse = await GitClient.Repository.Content.CreateFile(RepoOwner, RepoName, "commit.txt", createRequest);


            }catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                
            }

            return "";

        }
    }
}