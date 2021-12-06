using System;
using System.Collections.Generic;
using MathNet.Numerics.Interpolation;
using System.Linq;

namespace REIC
{
    /// The wind turbine power curves are given as pairs of samples.
    public class PowerCurve
    {
        LinearSpline values;
        //public List<Tuple<double, double>> Values { get; }

        public PowerCurve(List<Tuple<double, double>> values) 
            : this(values.Select(x => x.Item1), values.Select(x => x.Item2))
        {
        }
        public PowerCurve(IEnumerable<double> xs, IEnumerable<double> ys)//List<Tuple<double, double>> values)
        {
            this.values = LinearSpline.Interpolate(xs, ys);
        }

        public double At(double x)
        {
            return values.Interpolate(x);
        }
    }
}
