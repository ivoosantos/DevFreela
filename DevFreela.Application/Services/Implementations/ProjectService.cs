﻿using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DevFreela.Application.Services.Implementations
{
	public class ProjectService : IProjectService
	{
		private readonly DevFreelaDbContext _dbContext;
		private readonly string _connectionString;
		public ProjectService(DevFreelaDbContext dbContext, IConfiguration connectionString)
        {
            _dbContext = dbContext;
			_connectionString = connectionString.GetConnectionString("DevFreelaCs");
        }
        public int Create(NewProjectInputModel inputModel) // Refatorado
		{
			var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
			_dbContext.Projects.Add(project);
			_dbContext.SaveChanges();

			return project.Id;
		}

		public void CreateComment(CreateCommentInputModel inputModel) // Refatorado
		{
			var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
			_dbContext.ProjectComments.Add(comment);
			_dbContext.SaveChanges();
		}

		public void Delete(int id)
		{
			var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
			project.Cancel();
			_dbContext.SaveChanges();
		}

		public void Finish(int id)
		{
			var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

			project.Finish();
			_dbContext.SaveChanges();
		}

		public List<ProjectViewModel> GetAll(string query)
		{
			var projects = _dbContext.Projects;

			var projectsViewModel = projects
				.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
				.ToList();

			return projectsViewModel;
		}

		public ProjectDetailsViewModel GetById(int id)
		{
			var project = _dbContext.Projects
				.Include(p => p.Client)
				.Include(p => p.Freelancer)
				.SingleOrDefault(p => p.Id == id);

			if (project == null) return null;

			var projectDetailsViewModel = new ProjectDetailsViewModel(
				project.Id,
				project.Title,
				project.Description,
				project.TotalCost,
				project.StartedAt,
				project.FinishedAt,
				project.Client.FullName,
				project.Freelancer.FullName
				);

			return projectDetailsViewModel;
		}

		public void Start(int id)
		{
			var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

			project.Start();
			//_dbContext.SaveChanges();

			using (var sqlConnection = new SqlConnection(_connectionString))
			{
				sqlConnection.Open();

				var script = "update Projects set Status = @status, StardedAt = @startedat where Id = @id";

				sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, id });
			}

		}

		public void Update(UpdateProjectInputModel inputModel)
		{
			var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

			project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
			_dbContext.SaveChanges();
		}
	}
}
