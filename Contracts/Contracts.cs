using System;

namespace Contracts
{
  public interface IDynamics
  {
    PhasePoint f(double t, PhasePoint x, PhasePoint u, PhasePoint v);
  }
}