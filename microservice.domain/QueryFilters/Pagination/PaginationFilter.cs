using microservice.domain.Settings;

namespace microservice.domain.QueryFilters.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter(int pageNumber, int pageSize, PaginationOptionsSetting options)
        {
            PageNumber = pageNumber == 0 ? options.DefaultPageNumber : pageNumber;
            PageSize = pageSize == 0 ? options.DefaultPageSize : pageSize;
        }
    }
}
