using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobTask.Enums
{
    public enum Student
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "Yes")]
        Yes = 1,
        [Display(Name = "No")]
        No = 2,

    }
}
