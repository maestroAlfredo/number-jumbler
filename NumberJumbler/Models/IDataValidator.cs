using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberJumbler.Models
{
    public interface IDataValidator
    {
        bool isValid(Stream input, out string errorMessage); //checks whether or not the input string is valid
    }
}
