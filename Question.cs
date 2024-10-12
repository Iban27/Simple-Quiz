using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Quiz_
{
    public class Question
    {
        public readonly string question;
        public readonly string[] correctAnswers;
        public readonly string[] options;
        public readonly int points;

        public Question(string question, string[] correctAnswers, string[] options, int points)
        {
            this.question = question;
            this.correctAnswers = correctAnswers;
            this.options = options;
            this.points = points;
        }
    }
}
