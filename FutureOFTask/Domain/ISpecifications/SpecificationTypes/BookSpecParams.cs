namespace FutureOFTask.Domain.ISpecifications.SpecificationTypes
{
    public class BookSpecParams
    {
        private string search;
        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }
        public string? Sort { get; set; }
        public string? title { get; set; }
        public string? author { get; set; }
        public string? GenreName { get; set; }
    }
}
