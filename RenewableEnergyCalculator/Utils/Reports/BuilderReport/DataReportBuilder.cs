using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace builderReport
{
    public class DataReportBuilder:IDataReportBuilder
    {
        private DataReport _dataReport;
        private IEnumerable<Data> _datas;
        public DataReportBuilder(IEnumerable<Data> datas)
        {
            _datas = datas;
            _dataReport = new DataReport();
        }
        public void BuildHeader()
        {
            _dataReport.HeaderPart = $"DATA REPORT \n";
        }
        public void BuildBody()
        {
           // _dataReport.BodyPart = string.Join(Environment.NewLine, _datas.Select(p => $"Product name: {p.Name}, product price: {p.Price}"));
        }
        public void BuildFooter()
        {
            _dataReport.FooterPart = "\nThis is the report footer.";
        }
        public DataReport GetReport()
        {
            var productStockReport = _dataReport;
            Clear();
            return productStockReport;
        }
        private void Clear() => _dataReport = new DataReport();
    }
}
