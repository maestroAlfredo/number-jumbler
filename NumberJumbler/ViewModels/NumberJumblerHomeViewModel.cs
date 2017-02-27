using NumberJumbler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NumberJumbler.ViewModels
{
    public class NumberJumblerHomeViewModel
    {
        [Required]
        [CorrectExtension]
        public HttpPostedFileBase File { get; set; }
        public bool CheckError { get; set; }
        public NumberJumblerResult Result { get; set; }
    }

    public class CorrectExtensionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }
            else if (!file.FileName.Substring(file.FileName.LastIndexOf('.')).Equals(".csv"))
            {
                ErrorMessage = "Please upload a csv file";
                return false;
            }
            return true;
             
        }
    }

}