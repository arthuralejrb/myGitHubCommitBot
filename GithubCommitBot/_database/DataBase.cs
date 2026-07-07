using GitHubCommitBot._models;
using GitHubCommitBot._services;
using Npgsql;
using Octokit;

namespace GitHubCommitBot._database
{
    class DataBase
    {
        private string ConnectionString {get; set;}
        

        public DataBase(String connectionString)
        {
            ConnectionString = connectionString;
            
        }

        public async Task Post(UserConfig Config)
        {
            string query = $"INSERT INTO userconfig (telegram_chat_id, github_user,user_repository, repository_branch, user_token) VALUES ($1, $2, $3, $4, $5);";
            

            try
            {
                await using var dataSource = NpgsqlDataSource.Create(ConnectionString);
                await using var cmd = dataSource.CreateCommand(query);
                cmd.Parameters.AddWithValue(Config.TelegramChatID);
                cmd.Parameters.AddWithValue(Config.GitHubUser ?? string.Empty);
                cmd.Parameters.AddWithValue(Config.UserRepository ?? string.Empty);
                cmd.Parameters.AddWithValue(Config.RepositoryBranch ?? string.Empty);
                cmd.Parameters.AddWithValue(Config.UserToken ?? string.Empty);

                await cmd.ExecuteNonQueryAsync();
                
    
            }catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
                
            }

        }

        public async Task<UserConfig> GetUserConfig (long chatID)
        {

            string query = $"SELECT * FROM userconfig WHERE telegram_chat_id = $1";
            UserConfig notFound = new UserConfig();

            try
            {
                await using var dataSource = NpgsqlDataSource.Create(ConnectionString);
                await using var cmd = dataSource.CreateCommand(query);

                cmd.Parameters.AddWithValue(chatID);

                await using var reader = await cmd.ExecuteReaderAsync();
               if ( await reader.ReadAsync())
                {   
                    return new UserConfig
                    (
                        reader.GetInt64(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5)
                            
                    );
                        
                }
                
                return notFound;
                
            }catch(Exception ex) {
                Console.WriteLine($"Error while searching: {ex.Message}");
                return notFound;

            }

        }
    }
}