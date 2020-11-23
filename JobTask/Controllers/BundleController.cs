using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using JobTask.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JobTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BundleController : ControllerBase
    {
        [HttpPost]
        public IActionResult ChooseBundle([FromBody]Answer answer)
        {
            Age age;
            try
            {
                age = (Age)HelperFunctions.GetEnumFromString(answer.Age, typeof(Age));
            }
            catch (Exception e)
            {
                return BadRequest(answer.Age + " is not a valid answer for age");
            }

            Student student;
            try
            {
                student = (Student)HelperFunctions.GetEnumFromString(answer.Student, typeof(Student));
            }
            catch (Exception e)
            {
                return BadRequest(answer.Student + " is not a valid answer for student");
            }

            Income income;
            try
            {
                income = (Income)HelperFunctions.GetEnumFromString(answer.Income, typeof(Income));
            }
            catch (Exception e)
            {
                return BadRequest(answer.Income + " is not a valid answer for income");
            }

            var possibleBuntles = HelperFunctions.PossibleBundles(age, student, income);
            possibleBuntles.Sort((i, c) => c.Value.CompareTo(i.Value));
            var bundle = possibleBuntles[0];

            var readableBundle = HelperFunctions.GetReadableBundle(bundle);

            return StatusCode(201, readableBundle);
        }

        [HttpPost("validate")]
        public IActionResult Validate([FromBody]Validation validation)
        {
            Age age;
            try
            {
                age = (Age)HelperFunctions.GetEnumFromString(validation.Answer.Age, typeof(Age));
            }
            catch (Exception e)
            {
                return BadRequest(validation.Answer.Age + " is not a valid answer for age");
            }

            Student student;
            try
            {
                student = (Student)HelperFunctions.GetEnumFromString(validation.Answer.Student, typeof(Student));
            }
            catch (Exception e)
            {
                return BadRequest(validation.Answer.Student + " is not a valid answer for student");
            }

            Income income;
            try
            {
                income = (Income)HelperFunctions.GetEnumFromString(validation.Answer.Income, typeof(Income));
            }
            catch (Exception e)
            {
                return BadRequest(validation.Answer.Income + " is not a valid answer for income");
            }
           
            var possibleBuntles = HelperFunctions.PossibleBundles(age, student, income);

            var bundleName = (BundleName)HelperFunctions.GetEnumFromString(validation.Readablebundle.Name, typeof(BundleName));
            var bundle = HelperFunctions.BundlesList.Find(b => b.Name == bundleName);

            if (!possibleBuntles.Contains(bundle))
            {
                return BadRequest("Chosen bundle is not valid");
            }

            foreach(string productName in validation.Readablebundle.Products)
            {
                ProductName product;
                try
                {
                    product = (ProductName)HelperFunctions.GetEnumFromString(productName, typeof(ProductName));
                }
                catch (Exception e)
                {
                    return BadRequest(productName + " is not a product");
                }
                if (product == ProductName.CurrentAccount)
                    {
                        if (income > Income.Zero & age > Age.Underage)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }

                    if (product == ProductName.CurrentAccountPlus)
                    {
                        if (income > Income.UpTo40000 & age > Age.Underage)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }

                    if (product == ProductName.JuniorSaverAccount)
                    {
                        if (age < Age.Adult)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }

                    if (product == ProductName.StudentAccount)
                    {
                        if (student == Student.Yes & age > Age.Underage)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }

                    if (product == ProductName.DebitCard)
                    {
                        var isValid = false;
                        foreach (string productName2 in validation.Readablebundle.Products)
                        {
                            var product2 = (ProductName)HelperFunctions.GetEnumFromString(productName2, typeof(ProductName));

                            if (product2 == ProductName.CurrentAccount ||
                                product2 == ProductName.CurrentAccountPlus ||
                                product2 == ProductName.JuniorSaverAccount ||
                                product2 == ProductName.StudentAccount)
                            {
                                isValid = true;
                                break;
                            }
                        }

                        if (isValid)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }

                    if (product == ProductName.CreditCard)
                    {
                        if (income > Income.UpTo12000 & age > Age.Underage)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }

                    if (product == ProductName.GoldCreditCard)
                    {
                        if (income > Income.UpTo40000 & age > Age.Underage)
                        {
                            continue;
                        }
                        else
                        {
                            return HelperFunctions.IsNotValid(productName);
                        }
                    }
                }
            return Ok();
        }
    }
}
