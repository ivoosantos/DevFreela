using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
	public class DevFreelaDbContext
	{
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu pojeto ASPNET Core 1", "Minha Descrição de Projeto 1", 1, 1, 10000),
				new Project("Meu pojeto ASPNET Core 2", "Minha Descrição de Projeto 2", 1, 1, 20000),
				new Project("Meu pojeto ASPNET Core 3", "Minha Descrição de Projeto 3", 1, 1, 30000)
			};

            Users = new List<User>
            {
                new User("Luis Felipe", "luisdev@luisdev..br", new DateTime(1992, 1, 1)),
                new User("Roberto Martinez", "rm@luisdev..br", new DateTime(1980, 1, 1)),
                new User("Anderson", "soso@luisdev..br", new DateTime(1999, 1, 1)),
            };

            Skills = new List<Skill>
            {
                new Skill(".Net Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
