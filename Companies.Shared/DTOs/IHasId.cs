using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Shared.DTOs;

public interface IHasId
{
    Guid Id { get; init; }
}
