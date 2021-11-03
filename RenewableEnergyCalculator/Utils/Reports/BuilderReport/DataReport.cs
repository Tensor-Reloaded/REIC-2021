using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace builderReport
{
    public class DataReport
    {
        public string HeaderPart { get; set; }
        public string BodyPart { get; set; }
        public string FooterPart { get; set; }
        public override string ToString() =>
            new StringBuilder().AppendLine(HeaderPart).AppendLine(BodyPart).AppendLine(FooterPart).ToString();
    }
}
