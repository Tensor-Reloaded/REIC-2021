using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace builderReport
{
    public interface IDataReportBuilder
    {
        void BuildHeader();
        void BuildBody();
        void BuildFooter();
        DataReport GetReport();
    }
}
