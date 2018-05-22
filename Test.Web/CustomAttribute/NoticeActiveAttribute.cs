using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Web.CustomAttribute
{
    public class NoticeActiveAttribute : ValidationAttribute
    {
        //public int Allowed { get; set; }

        //public override bool IsValid(object value)
        //{
        //    if (value == 0)
        //    {
        //        return True;
        //    }

        //    if (value != (object)Allowed)
        //    {
        //        return false;
        //    }

        //    return True;
        //}
    }
}