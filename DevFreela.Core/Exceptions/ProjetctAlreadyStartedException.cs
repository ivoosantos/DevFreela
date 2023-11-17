using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Exceptions
{
	public class ProjetctAlreadyStartedException : Exception
	{
        public ProjetctAlreadyStartedException() : base("Project is already in Started status")
        {
        }
    }
}
