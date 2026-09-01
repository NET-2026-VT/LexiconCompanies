using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Shared.Paging;

public class CompanyQueryParameters : QueryParameters
{
    public bool IncludeEmployees { get; set; }

}
