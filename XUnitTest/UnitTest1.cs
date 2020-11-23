using JobTask.Controllers;
using JobTask.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTest
{
    public class BundleControllerUnitTest
    {

        BundleController _controller;
        private readonly ITestOutputHelper output;

        public BundleControllerUnitTest(ITestOutputHelper output)
        {
            _controller = new BundleController();
            this.output = output;
        }

        [Fact]
        public void TestChooseBundleOk1()
        {
            ObjectResult result = _controller.ChooseBundle(new Answer { Age = "0-17", Student = "Yes", Income = "0" }) as ObjectResult;
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            var actualResult = result.Value;

            var expected = HelperFunctions.GetReadableBundle(HelperFunctions.BundlesList[0]);
            Assert.Equal(expected.Name, ((ReadableBundle)actualResult).Name);
            Assert.Equal(expected.Products, ((ReadableBundle)actualResult).Products);
            Assert.Equal(expected.Value, ((ReadableBundle)actualResult).Value);
        }
        [Fact]
        public void TestChooseBundleOk2()
        {
            ObjectResult result = _controller.ChooseBundle(new Answer { Age = "18-64", Student = "Yes", Income = "1-12000" }) as ObjectResult;
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            var actualResult = result.Value;

            var expected = HelperFunctions.GetReadableBundle(HelperFunctions.BundlesList[2]);
            Assert.Equal(expected.Name, ((ReadableBundle)actualResult).Name);
            Assert.Equal(expected.Products, ((ReadableBundle)actualResult).Products);
            Assert.Equal(expected.Value, ((ReadableBundle)actualResult).Value);
        }
        [Fact]
        public void TestChooseBundleOk3()
        {
            ObjectResult result = _controller.ChooseBundle(new Answer { Age = "65+", Student = "Yes", Income = "12001-40000" }) as ObjectResult;
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            var actualResult = result.Value;

            var expected = HelperFunctions.GetReadableBundle(HelperFunctions.BundlesList[3]);
            Assert.Equal(expected.Name, ((ReadableBundle)actualResult).Name);
            Assert.Equal(expected.Products, ((ReadableBundle)actualResult).Products);
            Assert.Equal(expected.Value, ((ReadableBundle)actualResult).Value);
        }
        [Fact]
        public void TestChooseBundleOk4()
        {
            ObjectResult result = _controller.ChooseBundle(new Answer { Age = "18-64", Student = "No", Income = "40001+" }) as ObjectResult;
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            var actualResult = result.Value;

            var expected = HelperFunctions.GetReadableBundle(HelperFunctions.BundlesList[4]);
            Assert.Equal(expected.Name, ((ReadableBundle)actualResult).Name);
            Assert.Equal(expected.Products, ((ReadableBundle)actualResult).Products);
            Assert.Equal(expected.Value, ((ReadableBundle)actualResult).Value);
        }
        [Fact]
        public void TestChooseBundleOk5()
        {
            ObjectResult result = _controller.ChooseBundle(new Answer { Age = "0-17", Student = "No", Income = "40001+" }) as ObjectResult;
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            var actualResult = result.Value;

            var expected = HelperFunctions.GetReadableBundle(HelperFunctions.BundlesList[0]);
            Assert.Equal(expected.Name, ((ReadableBundle)actualResult).Name);
            Assert.Equal(expected.Products, ((ReadableBundle)actualResult).Products);
            Assert.Equal(expected.Value, ((ReadableBundle)actualResult).Value);
        }
        [Fact]
        public void TestChooseBundleBad()
        {
            ObjectResult result = _controller.ChooseBundle(new Answer { Age = "20-30", Student = "Yes", Income = "1-12000" }) as ObjectResult;
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            var actualResult = result.Value;

            var expected = "20-30 is not a valid answer for age";

            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public void TestValidateOk1()
        {
            Validation validate = new Validation
            {
                Answer = new Answer { Age = "0-17", Student = "Yes", Income = "0" },
                Readablebundle = new ReadableBundle { Name = "Junior Saver", Products = new List<string> { "Junior Saver Account" }, Value = 0 }
            };

            var result = _controller.Validate(validate);
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public void TestValidateOk2()
        {
            Validation validate = new Validation
            {
                Answer = new Answer { Age = "18-64", Student = "Yes", Income = "1-12000" },
                Readablebundle = new ReadableBundle { Name = "Classic", Products = new List<string> { "Current Account", "Debit Card" }, Value = 1 }
            };

            var result = _controller.Validate(validate);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TestValidateOk3()
        {
            Validation validate = new Validation
            {
                Answer = new Answer { Age = "65+", Student = "Yes", Income = "12001-40000" },
                Readablebundle = new ReadableBundle { Name = "Classic Plus", Products = new List<string> { "Current Account", "Debit Card", "Credit Card" }, Value = 2 }
            };

            var result = _controller.Validate(validate);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TestValidateBad1()
        {
            Validation validate = new Validation
            {
                Answer = new Answer { Age = "18-64", Student = "No", Income = "0" },
                Readablebundle = new ReadableBundle { Name = "Classic Plus", Products = new List<string> { "Current Account", "Debit Card", "Student Account" }, Value = 2 }
            };

            ObjectResult result = _controller.Validate(validate) as ObjectResult;
            var actualResult = result.Value;

            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            var expected = "Chosen bundle is not valid";
            Assert.Equal(expected, actualResult);
        }

        [Fact]
        public void TestValidateBad2()
        {
            Validation validate = new Validation
            {
                Answer = new Answer { Age = "18-64", Student = "No", Income = "40001+" },
                Readablebundle = new ReadableBundle { Name = "Classic Plus", Products = new List<string> { "Current Account", "Debit Card", "Credit B Card" }, Value = 2 }
            };

            ObjectResult result = _controller.Validate(validate) as ObjectResult;
            var actualResult = result.Value;

            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            var expected = "Credit B Card is not a product";
            Assert.Equal(expected, actualResult);
        }
    }
}
