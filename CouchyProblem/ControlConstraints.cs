using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace CouchyProblem
{
  public class ControlConstraints : SortedDictionary<PhasePoint, Dictionary<double, double>>
  {
    private readonly Grid constr;

    public ICollection<PhasePoint> Keys
    {
      get { return constr.Keys; }
    }

    public PhasePoint FindMin(Func<PhasePoint, Double> f) // Передали функцию
    {
      Tuple<Double, PhasePoint> res = constr.Keys
        .Select(point => Tuple.Create(f(point), point))
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
      ControlConstraints constr = new ControlConstraints();
      int[]
        upperBounds = new int[ld.Dim],
        cnt = new int[ld.Dim];
      for (int i = 0; i < ld.Dim; i++)
      {
        upperBounds[i] = (int) (sizes[i] / steps[i]);
        cnt[i] = 0;
      }

      int curind;
      do
      {
        PhasePoint curpoint = new PhasePoint(ld, steps, cnt);
        constr.Add(curpoint, null);

        curind = 0;
        cnt[curind]++;
        while (curind < ld.Dim && cnt[curind] > upperBounds[curind])
        {
          cnt[curind] = 0;
          curind++;
          if (curind < ld.Dim) cnt[curind]++;
        }
      } while (curind < ld.Dim);

      return constr;
    }

    public static ControlConstraints BallConstraints(PhasePoint center, double radius, PhasePoint steps)
    {
      ControlConstraints constr = new ControlConstraints();
      int[]
        lowerBounds = new int[center.Dim],
        upperBounds = new int[center.Dim],
        cnt = new int[center.Dim];

      lowerBounds[0] = -(int) (radius / steps[0]);
      upperBounds[0] = -lowerBounds[0];
      cnt[0] = lowerBounds[0] - 1;

      int curind = 0;

      while (curind >= 0)
      {
        cnt[curind]++;
        if (cnt[curind] > upperBounds[curind]) curind--;
        else
        {
          curind++;
          if (curind < center.Dim)
          {
            lowerBounds[curind] =
              (int) Math.Ceiling(-Math.Sqrt(radius * radius -
                                            cnt.Take(curind).Select(x => x * x).Sum()) / steps[curind]);
            upperBounds[curind] = -lowerBounds[curind];
            cnt[curind] = lowerBounds[curind] - 1;
          }
          else
          {
            PhasePoint curpoint = new PhasePoint(center, steps, cnt);
            constr.Add(curpoint, null);
            curind--;
          }
        }
      }

      return constr;
    }

    public static ControlConstraints SegmentConstraints(PhasePoint beginning, PhasePoint end, PhasePoint steps)
    {
      ControlConstraints constr = new ControlConstraints();
      PhasePoint currentpoint = beginning;
      while (currentpoint.CompareTo(end) != 0)
      {
        constr.Add(currentpoint, null);
        currentpoint += steps;
      }

      return constr;
    }
  }
}