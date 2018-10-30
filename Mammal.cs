using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisLab5
{
    class Mammal
    {
        protected string saySmth()
        {
            return "smth!";
        }

        protected virtual string getType()
        {
            return "Mammal!";
        }

        protected virtual string sayHello()
        {
            return "hello";
        }
    }
}
