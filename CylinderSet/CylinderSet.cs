using System;
using Contracts;
using System.Linq;

namespace CylinderSet
{
    public class CylinderSet
    {
        public class Sets : IChi, ITheta
        {
            private double r;
            private double T;
      
            public Sets(double finalR,double finalT)
            {
                r = finalR;
                T = finalT;
            }
      
            public double theta(double t, PhasePoint x)
            {
                return 0;
            }

            public double chi(double t, PhasePoint x)
            {
                double norm = Math.Sqrt(x
                    .Select(a => a * a)
                    .Sum());
                if (norm > r)
                {
                    return (norm / r) - 1 - t + T;
                }
                else return T - t;
            }
        }    
    }
}