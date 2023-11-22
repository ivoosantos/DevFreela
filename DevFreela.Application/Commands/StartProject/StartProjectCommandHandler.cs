using Dapper;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
	public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
	{
		private readonly DevFreelaDbContext _dbContext;
		private readonly string _connectionString;
		public StartProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration connectionString)
        {
            _dbContext = dbContext;
			_connectionString = connectionString.GetConnectionString("DevFreelaCs");
		}
        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
		{
			var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

			project.Start();
			//_dbContext.SaveChanges();

			using (var sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();

				var script = "update Projects set Status = @status, StardedAt = @startedat where Id = @id";

				sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, request.Id });
			}

			return Unit.Value;
		}
	}
}
