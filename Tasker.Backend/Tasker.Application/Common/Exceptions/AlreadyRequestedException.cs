using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Common.Exceptions
{
    public class AlreadyRequestedException : Exception
    {
        public AlreadyRequestedException(string name, object key, object worker) : base($"Entity \"{name}\" ({key}) is already requested by {worker}." ) { }
    }
}
