using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Core.Exceptions
{
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException(int id): base($"Country with id {id} was not found") { }
    }
}
