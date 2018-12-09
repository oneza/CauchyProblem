using System;
using System.Collections.Generic;
using System.Text;

namespace CouchyProblem
{
    class ControlConstraintsStorage : SortedDictionary<PhasePoint, Dictionary<double, double>>
    {
    }

    public class ControlConstraints
    {
        readonly GridStorage constr = new GridStorage();

        public static Grid BoxConstraints(PhasePoint ld, PhasePoint sizes, PhasePoint steps)
        {
            return null;
        }

        public static Grid BallConstraints(PhasePoint center, double radius, PhasePoint steps)
        {
            return null;
        }

        public static Grid SegmentConstraints(PhasePoint beginning, PhasePoint end, PhasePoint steps)
        {
            return null;
        }
    }
}