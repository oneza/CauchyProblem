using System;
using System.Linq;
using Contracts;

namespace Sigma
{
    public class Sigma : Contracts.Sigma
    {
        public double sigma(PhasePoint x)
        {
            return Math.Sqrt(x
                .Select(a => a * a)
                .Sum());
        }
    }
}