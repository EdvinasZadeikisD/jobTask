using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobTask.Enums
{
    public enum ProductName
    {
        [Display(Name = "Undefined")]
        Undefined = 0,
        [Display(Name = "Current Account")]
        CurrentAccount = 1,
        [Display(Name = "Current Account Plus")]
        CurrentAccountPlus = 2,
        [Display(Name = "Junior Saver Account")]
        JuniorSaverAccount = 3,
        [Display(Name = "Student Account")]
        StudentAccount = 4,
        [Display(Name = "Debit Card")]
        DebitCard = 5,
        [Display(Name = "Credit Card")]
        CreditCard = 6,
        [Display(Name = "Gold Credit Card")]
        GoldCreditCard = 7,
    }
}
