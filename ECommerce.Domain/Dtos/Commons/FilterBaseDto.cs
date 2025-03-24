﻿namespace ECommerce.Domain.Dtos.Commons
{
    public abstract class FilterBaseDto
    {
        #region Properties

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string ReportName { get; set; } = string.Empty;
        public string? SortDirection { get; set; } = "desc";
        public string? SortBy { get; set; } = string.Empty;
        public string? Search { get; set; } = string.Empty;

        #endregion Properties
    }
}