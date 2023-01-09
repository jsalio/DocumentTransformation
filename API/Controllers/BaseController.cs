using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Linq;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal void ValidateResult<TResult, TException>(Option<TResult, TException> results)
        {
            if (!results.HasValue)
            {
                TypeConverter c = TypeDescriptor.GetConverter(results);
                if (c.CanConvertTo(typeof(TException)))
                {
                    var result = c.ConvertTo(results, typeof(TException));
                    results.MatchNone(err => throw (Exception)result);
                }
            }
        }

        internal void ValidateRequestResult(IEnumerable<ValidationResult> validations)
        {
            if (!validations.Any())
            {
                return;
            }

            var messages = validations.Select(x => x.ErrorMessage).ToList();
            throw new Core.Models.Exceptions.CoreException(string.Join(",", messages));
        }
    }
}
