using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobTask.Enums
{
    public enum Income
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "0")]
        Zero = 1,
        [Display(Name = "1-12000")]
        UpTo12000 = 2,
        [Display(Name = "12001-40000")]
        UpTo40000 = 3,
        [Display(Name = "40001+")]
        MoreThen40000 = 4,
    }
}
