using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Quiz_
{
    public class Test
    {
        public readonly string name;
        public readonly string discription;
        public readonly Question[] questions;

        public Test(string name, string discription, Question[] questions)
        {
            this.name = name;
            this.discription = discription;
            this.questions = questions;
        }
    }
}
