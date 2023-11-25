using DevFreela.Application.Commands.CreateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
	public class CreateProjectCommandvalidator : AbstractValidator<CreateProjectCommand>
	{
		public CreateProjectCommandvalidator() 
		{
			RuleFor(p => p.Description)
				.MaximumLength(255)
				.WithMessage("Tamanho máximo de Descrição é de 255 caracteres.");

			RuleFor(p => p.Title)
				.MaximumLength(30)
				.WithMessage("Tamanho máximo do Título é de 30 caracteres.");
		}
	}
}
