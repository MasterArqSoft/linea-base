using microservice.domain.QueryFilters.Pagination;

namespace microservice.domain.Interfaces;

public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}
