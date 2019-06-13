using System;
using System.Linq;
using Contracts;

namespace Functions
{
    public class Sets : IChi, ITheta
    {
        public double theta(PhasePoint x)
        {
            return 0;
        }

        public double chi(PhasePoint x, double r, double t)
        {
            double norm = Math.Sqrt(x
                .Select(a => a * a)
                .Sum());
            if (norm > r)
            {
                return (norm / r) - 1;
            }
            else return 0;
        }
    }
    
    
}