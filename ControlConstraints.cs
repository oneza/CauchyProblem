using System;
using System.Collections.Generic;
using System.Text;

namespace CouchyProblem
{
//    class ControlConstraintsStorage : SortedDictionary<PhasePoint, Dictionary<double, double>>
//    {
//    }

    public class ControlConstraints
    {
        public readonly Grid constr;

        public PhasePoint FindMin(); // Передали функцию
        public PhasePoint FindMax(); // Передали функцию


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
            return null;
        }
    }
}