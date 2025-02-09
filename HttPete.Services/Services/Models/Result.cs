using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Services.Services.Models
{
    public enum ResultStatus
    {
        Success,
        NotFound,
        ValidationError
    }

    public record Result<T>(bool IsSuccess, T? Value, string? Error, ResultStatus Status);
}
