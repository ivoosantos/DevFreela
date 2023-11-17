using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
	[Route("api/projects")]
	public class ProjectsController : ControllerBase
	{
		private readonly OpeningTimeOption _option;
		public ProjectsController(IOptions<OpeningTimeOption> option)
		{
			_option = option.Value;
		}

		// api/projects?query=net core
		[HttpGet]
		public IActionResult Get(string query)
		{
			return Ok();
		}

		[HttpGet("{id}")]
		public IActionResult GetByid(int id)
		{

			return Ok();
		}

		[HttpPost]
		public IActionResult Post([FromBody] CreateProjectModel createProject)
		{
			if (createProject.Title.Length > 50)
			{
				return BadRequest();
			}

			return CreatedAtAction(nameof(GetByid), new { id = createProject.Id }, createProject);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody]UpdateProjectsModel updateProject)
		{
			if (updateProject.Description.Length > 200)
			{
				return BadRequest();
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			return NoContent();
		}

		[HttpPost("{id}/comments")]
		public IActionResult PostComment(int id, [FromBody]CreateCommentModel createCommentModel)
		{
			return NoContent();
		}

		[HttpPut("{id}/start")]
		public IActionResult Start(int id)
		{
			return NoContent();
		}

		[HttpPut("{id}/finish")]
		public IActionResult Finish(int id)
		{
			return NoContent();
		}
	}
}
