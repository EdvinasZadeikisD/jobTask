using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobTask.Enums
{
    public enum Age
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "0-17")]
        Underage = 1,
        [Display(Name = "18-64")]
        Adult = 2,
        [Display(Name = "65+")]
        Senior = 3,
    }
}
