using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllSkills
{
	public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
	{
		private readonly DevFreelaDbContext _dbContext;
		private readonly string _connectionString;
		public GetAllSkillsQueryHandler(DevFreelaDbContext dbContext, IConfiguration connectionString) 
		{
			_dbContext = dbContext;
			_connectionString = connectionString.GetConnectionString("DevFreelaCs");
		}
		public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
		{
			//using (var sqlConnection = new SqlConnection(_connectionString))
			//{
			//	sqlConnection.Open();

			//	var script = "select Id, Description From Skills";

			//	return sqlConnection.Query<SkillViewModel>(script).ToList();
			//}

			var projects = _dbContext.Projects;

			var projectsModel = await projects
				.Select(p => new SkillViewModel(p.Id, p.Description))
				.ToListAsync();

			return projectsModel;

		}
	}
}
