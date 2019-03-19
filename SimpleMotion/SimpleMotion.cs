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
        throw new Exception("Simple motion dynamics: controls of different dimensions!"`);
#endif
      return u + v;
    }
  }
}