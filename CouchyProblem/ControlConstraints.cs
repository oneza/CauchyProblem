using System;
using System.Collections.Generic;
using System.Linq;

using Contracts;

namespace CouchyProblem
{
    public class ControlConstraints
    {
        public readonly Grid constr;

        public PhasePoint FindMin(Func<PhasePoint,Double> f)  // Передали функцию
        {
            Tuple<Double,PhasePoint> res = constr.Keys
               .Select(point => Tuple.Create(f(point),point))
               .Min();
            return res.Item2;
        }

        public PhasePoint FindMax(Func<PhasePoint, Double> f) // Передали функцию
        {
            Tuple<Double, PhasePoint> res = constr.Keys
               .Select(point => Tuple.Create(f(point), point))
               .Max();
            return res.Item2;
        }

        private ControlConstraints()
        {
            constr = null;
        }

        public static ControlConstraints BoxConstraints(PhasePoint ld, PhasePoint sizes, PhasePoint steps)
        {
            return null;
        }

        public static ControlConstraints BallConstraints(PhasePoint center, double radius, PhasePoint steps)
        {
            return null;
        }

        public static ControlConstraints SegmentConstraints(PhasePoint beginning, PhasePoint end, PhasePoint steps)
        {
            ControlConstraints res = new ControlConstraints();
            return res;
        }
    }

}