using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Quiz_
{
    public class TestBase
    {
        //private Test[] _tests = new Test[3];
        private List<Test> _tests = new List<Test>();

        public Test GetTestById(int id)
        {
            return _tests[id - 1];
        }

        public void AddTest( Test test)
        {
            _tests.Add(test);
        }

        public List<Test> GetTestList()
        {
            return _tests;
        }
    }
}
