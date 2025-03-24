namespace ECommerce.Domain.Commons
{
    public class PagedResult<T>
    {
        #region Public Constructors

        public PagedResult(List<T> data, int pageIndex, int pageSize, int totalRecords, int totalEntries)
        {
            Result = data;
            Page = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalEntries = totalEntries;
            TotalPages = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
        }

        #endregion Public Constructors

        #region Properties

        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalEntries { get; init; }
        public int TotalPages { get; init; }
        public List<T> Result { get; init; }

        #endregion Properties

        #region Public Methods

        public PagedResult<Y> SetPagedResultResponse<Y>(Func<T, Y> map)
        {
            var mappedResult = Result.Select(map).ToList();

            return new PagedResult<Y>(
                mappedResult,
                Page,
                PageSize,
                TotalRecords,
                TotalEntries
            );
        }

        #endregion Public Methods
    }
}