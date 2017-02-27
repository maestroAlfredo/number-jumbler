using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberJumbler.Models
{
    public class NumberJumblerResult
    {
        public int Result { get; set; }
        public bool HasErrors { get; set; }
        public string ErrorMessage { get; set; }
    }
}