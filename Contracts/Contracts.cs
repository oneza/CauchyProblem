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

  public interface IChi
  {
    double chi(PhasePoint x, double r, double t, double T);
  }

  public interface ITheta
  {
    double theta(PhasePoint x);
  }
}