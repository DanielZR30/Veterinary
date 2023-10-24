using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Veterinary.Interfaces
{
    internal interface IDependencyResolver: IDependencyScope,IDisposable
    {
        IDependencyScope BeginScope();
    }
}
