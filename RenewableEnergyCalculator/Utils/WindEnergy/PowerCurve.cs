using System;
using System.Collections.Generic;
using MathNet.Numerics.Interpolation;
using System.Linq;

namespace REIC
{
    /// <summary>
    /// The wind turbine power curves are given as pairs of samples.
    /// </summary>
    public class PowerCurve
    {
        LinearSpline values;
        //public List<Tuple<double, double>> Values { get; }

        /// <summary>
        /// The maximum energy that can be produced
        /// </summary>
        public double MaxValue { get; }

        public PowerCurve(List<Tuple<double, double>> values) 
            : this(values.Select(x => x.Item1), values.Select(x => x.Item2))
        {
        }
        public PowerCurve(IEnumerable<double> xs, IEnumerable<double> ys)
        {
            this.values = LinearSpline.Interpolate(xs, ys);
            MaxValue = ys.Max();
        }

        public double At(double x)
        {
            return values.Interpolate(x);
        }
    }
}
