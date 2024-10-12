using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Quiz_
{
    public class TestList
    {
        public TestData[] tests;

        public TestList(TestData[] tests)
        {
            this.tests = tests;
        }
    }

    public class TestData
    {
        public string theme;
        public string description;
        public QuestionsData[] questions;
    }

    public class QuestionsData
    {
        public string question;
        public string[] correct_answers;
        public string[] options;
        public int points;
    }
}
