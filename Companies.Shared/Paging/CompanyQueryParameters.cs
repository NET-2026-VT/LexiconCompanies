using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Companies.Shared.Paging;

public class CompanyQueryParameters
{
    private const int _maxPageSize = 100;

    public bool IncludeEmployees { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "{0} must be at least {1}.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, _maxPageSize, ErrorMessage = "{0} must be between {1} and {2}.")]
    public int PageSize { get; set; } = 10;

    public int GetOffset => checked((PageNumber - 1) * PageSize);

}
