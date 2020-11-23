using JobTask.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTask.Controllers
{
    public class Bundle
    {
        public BundleName Name { get; set; }
        public List<ProductName> Products { get; set; }
        public int Value { get; set; }
    }

    public class ReadableBundle
    {
        public string Name { get; set; }
        public List<string> Products { get; set; }
        public int Value { get; set; }
    }
}
