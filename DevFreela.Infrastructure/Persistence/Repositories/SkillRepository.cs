using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
	public class SkillRepository : ISkillRepository
	{
		private readonly string _connectionString;
		public SkillRepository(IConfiguration connectionString)
        {
			_connectionString = connectionString.GetConnectionString("DevFreelaCs");
		}
        public async Task<List<SkillDTO>> GetAllAsync()
		{
			using (var sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();

				var script = "select Id, Description From Skills";

				var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

				return skills.ToList();
			}

			//var projects = _dbContext.Projects;

			//var projectsModel = await projects
			//	.Select(p => new SkillDTO(p.Id, p.Description))
			//	.ToListAsync();

			//return projectsModel;
		}
	}
}
