using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Queries
{
	public class GetAllSkillsQueryHandlerTests
	{
		[Fact]
		public async Task TwoSkillsExist_Executed_ReturnTwoSkillViewModels()
		{
			// Arrange
			var skills = new List<SkillDTO>
			{
				new SkillDTO { Id = 1, Description = "Teste de Skill 1"},
				new SkillDTO { Id = 2, Description = "Teste de Skill 2"}
			};

			var skillRepositoryMock = new Mock<ISkillRepository>();

			skillRepositoryMock.Setup(sk => sk.GetAllAsync().Result).Returns(skills);

			var getAllSkillsQuery = new GetAllSkillsQuery();
			var getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillRepositoryMock.Object);

			// Act
			var skillViewModelList = await getAllSkillsQueryHandler.Handle(getAllSkillsQuery, new CancellationToken());

			// Assert
			Assert.NotNull(skillViewModelList);
			Assert.NotEmpty(skillViewModelList);
			Assert.Equal(skills.Count, skillViewModelList.Count);

			skillRepositoryMock.Verify(sk => sk.GetAllAsync().Result, Times.Once());
		}
	}
}
