using System;
using System.Collections.Generic;
using System.Linq;

namespace CouchyProblem
{
    //    class ControlConstraintsStorage : SortedDictionary<PhasePoint, Dictionary<double, double>>
    //    {
    //    }

    public class ControlConstraints
    {
        public readonly Grid constr;

        public PhasePoint FindMin()  // Передали функцию
        {
            Grid grid = new Grid;
            var num = grid.Keys
               .Select(_ => Function.function(_))
               .ToList()
               .Min();
            List<PhasePoint> keys = grid.Keys
                .ToList();
            int i = keys.FindIndex(c => Function.function(c) == num);
            PhasePoint p = grid.Keys.ElementAt(i);
            return p;
        }

        public PhasePoint FindMax() // Передали функцию
        {
            Grid grid = new Grid;
            var num = grid.Keys
               .Select(_ => Function.function(_))
               .ToList()
               .Max();
            List<PhasePoint> keys = grid.Keys
                .ToList();
            int i = keys.FindIndex(c => Function.function(c) == num);
            PhasePoint p = grid.Keys.ElementAt(i);
            return p;
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
            ControlConstraints constr = new Grid;
            return constr;
        }
    }

    public class Function
    {
        public static Func<PhasePoint, double> function = DefineFunction;

        public static double DefineFunction(PhasePoint p)
        {
            return 1.0;
        }
    }
}