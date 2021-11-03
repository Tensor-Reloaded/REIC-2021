using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace builderReport
{
    public class DataReportDirector
    {
        private readonly IDataReportBuilder _dataReportBuilder;
        public DataReportDirector(IDataReportBuilder dataReportBuilder)
        {
            _dataReportBuilder = dataReportBuilder;
        }
        public void BuildReport()
        {
            _dataReportBuilder.BuildHeader();
            _dataReportBuilder.BuildBody();
            _dataReportBuilder.BuildFooter();
        }
    }
}
