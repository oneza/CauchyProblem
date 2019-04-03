using System;

using Contracts;

namespace SimpleMotion
{
  public class SimpleMotion : IDynamics
  {
    public PhasePoint f(double t, PhasePoint x, PhasePoint u, PhasePoint v)
    {
#if _DEBUG
      if (u.Dim != v.Dim)
        throw new Exception("Simple motion dynamics; f: controls of different dimensions!"`);
#endif
      return u + v;
    }
    
    public double fi(int i, double t, PhasePoint x, PhasePoint u, PhasePoint v)
    {
#if _DEBUG
      if (u.Dim != v.Dim)
        throw new Exception("Simple motion dynamics; fi: controls of different dimensions!"`);
      if (i < 0 || i >= u.Dim)
        throw new Exception("Simple motion dynamics; fi: index out of range!"`);
#endif
      return u[i] + v[i];
    }    
  }
}