using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberJumbler.Models
{
    public interface INumberJumblerService : IDataValidator
    {
        NumberJumblerResult FindLeader(Stream input);
        NumberJumblerResult FindLeaderNoValidation(Stream input);
    }
}
