using System;
using System.Linq;

using Contracts;

namespace CylinderSet_NoConstraints
{
  public class CylinderSet_NoConstraints
  {
    public class Sets : IChi, ITheta
    {
      private double r;
      
      public Sets(double finalR)
      {
        r = finalR;
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
          return (norm / r) - 1;
        }
        else return 0;
      }
    }    
  }
}