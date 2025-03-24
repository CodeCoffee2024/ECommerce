namespace ECommerce.Domain.Commons
{
    public class UnpagedResult<T>
    {
        #region Public Constructors

        public UnpagedResult(List<T> data, int totalRecords, int totalEntries)
        {
            Result = data;
            TotalRecords = totalRecords;
            TotalEntries = totalEntries;
        }

        #endregion Public Constructors

        #region Properties

        public int TotalRecords { get; init; }
        public int TotalEntries { get; init; }
        public List<T> Result { get; init; }

        #endregion Properties

        #region Public Methods

        public UnpagedResult<Y> SetResultResponse<Y>(Func<T, Y> map)
        {
            var mappedResult = Result.Select(map).ToList();

            return new UnpagedResult<Y>(mappedResult, TotalRecords, TotalEntries);
        }

        #endregion Public Methods
    }
}