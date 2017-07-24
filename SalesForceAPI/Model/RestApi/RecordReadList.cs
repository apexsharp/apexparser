using System.Collections.Generic;

namespace SalesForceAPI.Model.RestApi
{
    public class RecordReadList<T>
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public string nextRecordsUrl { get; set; }
        public List<T> records { get; set; }
    }
}