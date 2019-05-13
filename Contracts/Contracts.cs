using System;

namespace Contracts
{
  public interface IDynamics
  {
    PhasePoint f(double t, PhasePoint x, PhasePoint u, PhasePoint v);
    double fi(int i, double t, PhasePoint x, PhasePoint u, PhasePoint v);
  }

  public interface ISigma
  {
    double sigma(PhasePoint x);
  }
}