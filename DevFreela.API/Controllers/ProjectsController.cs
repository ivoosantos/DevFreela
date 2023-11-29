using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
	[Route("api/projects")]
	public class ProjectsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ProjectsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// api/projects?query=net core
		[HttpGet]
		[Authorize(Roles = "client, freelancer")]
		public async Task<IActionResult> Get(string query)
		{
			//var projects = _projectService.GetAll(query);

			var command = new GetAllProjectsQuery(query);

			var projects = await _mediator.Send(command);
			
			return Ok(projects);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "client, freelancer")]
		public async Task<IActionResult> GetByid(int id)
		{
			//var project = _projectService.GetById(id);

			var query = new GetProjectByIdQuery(id);

			var resp = await _mediator.Send(query);

			if(resp == null)
			{
				return NotFound();
			}


			return Ok(resp);
		}

		[HttpPost]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
		{
			var id = await _mediator.Send(command);

			return CreatedAtAction(nameof(GetByid), new { id = id }, command);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
		{
			if (command.Description.Length > 200)
			{
				return BadRequest();
			}

			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Delete(int id)
		{
			//_projectService.Delete(id);
			var command = new DeleteProjectCommand(id);
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpPost("{id}/comments")]
		[Authorize(Roles = "client, freelancer")]
		public async Task<IActionResult> PostComment(int id, [FromBody]CreateCommentCommand command)
		{
			//_projectService.CreateComment(inputModel);

			await _mediator.Send(command);

			return NoContent();
		}

		[HttpPut("{id}/start")]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Start(int id)
		{
			var command = new StartProjectCommand(id);

			await _mediator.Send(command);

			return NoContent();
		}

		[HttpPut("{id}/finish")]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Finish(int id)
		{
			//_projectService.Finish(id);

			var command = new FinishProjectCommand(id);

			await _mediator.Send(command);

			return NoContent();
		}
	}
}
