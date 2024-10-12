using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Quiz_
{
    public class ScoreManager
    {
        private int _score;
        public int Score => _score;

        public void AddScore(int addScore)
        {
            _score += addScore;
        }
    }
}
