namespace Routine.Api.DtoParameters
{
    public class CompanyDtoParameters
    {
        private int MaxPageSize = 20;
        public string CompanyName { get; set; }
        public string Introduction { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
