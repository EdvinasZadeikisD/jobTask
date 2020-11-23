using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobTask.Enums
{
    public enum BundleName
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "Junior Saver")]
        JuniorSaver = 1,
        [Display(Name = "Student")]
        Student = 2,
        [Display(Name = "Classic")]
        Classic = 3,
        [Display(Name = "Classic Plus")]
        ClassicPlus = 4,
        [Display(Name = "Gold")]
        Gold = 5,
    }
}
