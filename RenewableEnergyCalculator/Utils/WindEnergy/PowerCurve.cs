﻿using System;
using System.Collections.Generic;
namespace REIC
{
    /// The wind turbine power curves are given as pairs of samples.
    class PowerCurve
    {
        public Tuple<double, double> Values { get; }
    }
}