using NumberJumbler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NumberJumbler.Services
{
    public interface INumberJumblerWebService
    {        
        Task<NumberJumblerResult> FindLeader(HttpRequestBase request, bool checkForErrors);
    }
}
