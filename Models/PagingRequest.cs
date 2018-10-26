namespace NetCore.Common.Models
{
    public class PagingRequest<T>
    {
        public int? PageIndex { get; set; }

        public int? RecordCount { get; set; }

        public T Data { get; set; }
    }
}
