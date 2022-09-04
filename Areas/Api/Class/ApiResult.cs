using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Class
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public StatusCodeEnum StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Message { get; set; }

        public ApiResult(bool isSuccess, StatusCodeEnum statusCode, List<string> message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, StatusCodeEnum.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, StatusCodeEnum.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            List<string> message = new List<string>();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message.AddRange(errorMessages);
            }

            else if (result.Value is IEnumerable<IdentityError> identityErrors)
            {
                var errorMessages = identityErrors.Select(p => p.Description).Distinct();
                message.AddRange(errorMessages);
            }

            else
            {
                message.Add(result.Value.ToString());
            }

            return new ApiResult(false, StatusCodeEnum.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            List<string> Message = new List<string>() { result.Content };
            return new ApiResult(true, StatusCodeEnum.Success, Message);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, StatusCodeEnum.NotFound);
        }
        #endregion

    }
    public class ApiResult<TData> : ApiResult
       where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, StatusCodeEnum statusCode, TData data, List<string> message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, StatusCodeEnum.Success, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, StatusCodeEnum.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, StatusCodeEnum.Success, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, StatusCodeEnum.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {

            List<string> message = new List<string>();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message.AddRange(errorMessages);
            }

            else if (result.Value is IEnumerable<IdentityError> identityErrors)
            {
                var errorMessages = identityErrors.Select(p => p.Description).Distinct();
                message.AddRange(errorMessages);
            }

            else
            {
                message.Add(result.Value.ToString());
            }

            return new ApiResult<TData>(false, StatusCodeEnum.BadRequest, null, message);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            List<string> Message = new List<string>() { result.Content };
            return new ApiResult<TData>(true, StatusCodeEnum.Success, null, Message);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, StatusCodeEnum.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, StatusCodeEnum.NotFound, (TData)result.Value);
        }
        #endregion

    }
}
