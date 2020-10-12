using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moniturl.Hosting
{
    public class JqueryDatatableQueryModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public JqueryDatatableSearchModel Search { get; set; }
        public IEnumerable<JqueryDatatableOrderModel> Order { get; set; }
        public IEnumerable<JqueryDatatableColumnModel> Columns { get; set; }
    }

    public sealed class JqueryDatatableSearchModel
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public sealed class JqueryDatatableOrderModel
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
    public sealed class JqueryDatatableColumnModel
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Orderable { get; set; }
        public bool Searchable { get; set; }
        public JqueryDatatableSearchModel Search { get; set; }
    }

}
