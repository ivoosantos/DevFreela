using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
	public class CreateCommentCommandHandlerTests
	{
		[Fact]
		public async Task InputDataIsOk_Executed_ReturnCommentId()
		{
			// Arrange
			var commentRepository = new Mock<IProjectRepository>();

			var createCommentCommand = new CreateCommentCommand
			{
				Content = "Testando a criação de um comentátio",
				IdProject = 1,
				IdUser = 1
			};

			var createCommentCommandHandler = new CreateCommentCommandHandler(commentRepository.Object);//Cria um CommandHandler retornando um objeto dele...

			// Act
			var id = await createCommentCommandHandler.Handle(createCommentCommand, new CancellationToken());

			// Assert
			Assert.NotNull(id);

			commentRepository.Verify(c => c.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once());
		}
	}
}
