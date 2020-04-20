using MovieScoring.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScoring.Domain.ScoreCalculator
{
    interface ScoreCalculator
    {
        int calculate(int movieId);
    }
}
