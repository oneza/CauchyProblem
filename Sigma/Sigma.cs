using System;
using System.Linq;
using Contracts;

namespace Sigma
{
    public class Distance : ISigma
    {
        public double sigma(PhasePoint x)
        {
            return Math.Sqrt(x
                .Select(a => a * a)
                .Sum());
        }
    }
}