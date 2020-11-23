using JobTask.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JobTask.Controllers
{
    public static class HelperFunctions
    {

        public static List<Bundle> BundlesList = new List<Bundle> {
            new Bundle{Name = BundleName.JuniorSaver, Products = new List<ProductName> {ProductName.JuniorSaverAccount }, Value= 0 },
            new Bundle{Name = BundleName.Student, Products = new List<ProductName> {ProductName.StudentAccount, ProductName.DebitCard, ProductName.CreditCard }, Value= 0 },
            new Bundle{Name = BundleName.Classic, Products = new List<ProductName> {ProductName.CurrentAccount, ProductName.DebitCard }, Value= 1 },
            new Bundle{Name = BundleName.ClassicPlus, Products = new List<ProductName> {ProductName.CurrentAccount, ProductName.DebitCard, ProductName.CreditCard }, Value= 2 },
            new Bundle{Name = BundleName.Gold, Products = new List<ProductName> {ProductName.CurrentAccountPlus, ProductName.DebitCard, ProductName.GoldCreditCard }, Value= 3 }
        };
        public static IActionResult IsNotValid(string name)
        {
            return new BadRequestObjectResult(name + " is not valid option");
        }

        public static List<Bundle> PossibleBundles(Age age, Student student, Income income)
        {
            List<Bundle> possibleBuntles = new List<Bundle> { };

            if (age < Age.Adult)
            {
                possibleBuntles.Add(BundlesList[0]);
            }

            if (age > Age.Underage && student == Student.Yes)
            {
                possibleBuntles.Add(BundlesList[1]);
            }

            if (age > Age.Underage && income > Income.Zero)
            {
                possibleBuntles.Add(BundlesList[2]);
            }

            if (income > Income.UpTo12000 && age > Age.Underage)
            {
                possibleBuntles.Add(BundlesList[3]);
            }

            if (income > Income.UpTo40000 && age > Age.Underage)
            {
                possibleBuntles.Add(BundlesList[4]);
            }

            return possibleBuntles;
        }



        public static Enum GetEnumFromString(string gotString, Type gotType)
        {
            foreach (Enum enumerate in Enum.GetValues(gotType))
            {
                if (GetDisplayValue(enumerate) == gotString)
                    return enumerate;
            }
            return null;
        }

        public static string GetDisplayValue(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                DisplayAttribute attr =
                       Attribute.GetCustomAttribute(field,
                         typeof(DisplayAttribute)) as DisplayAttribute;
                if (attr != null)
                {
                    return attr.Name;
                }
            }

            return null;
        }

        public static ReadableBundle GetReadableBundle(Bundle bundle)
        {
            var readableBundle = new ReadableBundle
            {
                Name = GetDisplayValue(bundle.Name),
                Products = new List<string>(),
                Value = bundle.Value
            };
            foreach (ProductName productName in bundle.Products)
            {
                readableBundle.Products.Add(GetDisplayValue(productName));
            }
            return readableBundle;
        }
    }
}
