using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Core.Exceptions
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException(int id) : base($"Company with id {id} was not found") { }
    }
}
