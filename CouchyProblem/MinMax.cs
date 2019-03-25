using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace CouchyProblem
{
    public class MinMax
    {
        public double Lsderivative(PhasePoint point, double p, int i, Grid grid)
        {
            double deltax = p * ; // Ai - ?
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
            List<double> res;

            for (int i = 0; i < grid.Keys.Count; i++)
            {
                res.Add();                        
            }
        }
    }
}