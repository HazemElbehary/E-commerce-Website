using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.LoggedInUserServices
{
	public interface ILoggedInUserService
	{
        public string? UserId { get; }
    }
}
