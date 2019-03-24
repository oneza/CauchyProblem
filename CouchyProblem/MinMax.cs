using Contracts;

namespace CouchyProblem
{
    public class MinMax
    {
        //Подумать, как связать с i
        public double Lsderivative(PhasePoint point, double p, double a)
        {
            int i;
            double deltax = p * a;
            PhasePoint point1 = point;
            point1[i] = point1[i] - deltax;

            return (v(point) - v(point1)) / deltax;

        }

        public double Rsderivative(PhasePoint point, double p, double a)
        {
            int i;
            double deltax = p * a;
            PhasePoint point1 = point;
            point1[i] = point1[i] + deltax;

            return (v(point1) - v(point)) / deltax;
        }

        public double Minmax(ControlConstraints u, ControlConstraints v, Grid grid)
        {
            double res;

            res = ControlConstraints
        }
    }
}