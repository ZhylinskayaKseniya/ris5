using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisLab5
{
    class Person : Mammal
    {
        private static string phrase = "Man is a Mammal!";
        private readonly string hello = "Ama Man!";

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Serie { get; set; }
        public string Number { get; set; }
        public string Phone { get; set; }

        protected override string getType()
        {
            return GetType().ToString();
        }

        protected override string sayHello()
        {
            return hello;
        }

        public string saySmth()
        {
            return base.saySmth();
        }
    }
}
