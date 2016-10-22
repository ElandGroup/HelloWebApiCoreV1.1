using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloWebApiCoreV2.Common.ApiPack
{
    public static class ControllerExtension
    {

        #region webapi v2

        public static OkObjectResult OkEx<T>(this Controller context, T result)
        {
            return context.Ok(new ApiResult<T>
            {
                Success = true,
                Result = result,
                Error = null
            });
        }
        public static ObjectResult CreatedEx(this Controller context)
        {
            return context.CreatedEx<string>();
        }
        public static ObjectResult NoContentEx(this Controller context)
        {
            return context.NoContentEx<string>();
        }
        public static ObjectResult NotFoundEx(this Controller context)
        {
            return context.NotFoundEx<string>();
        }
        public static ObjectResult ErrorEx(this Controller context, string errorMessage)
        {
            return context.ErrorEx<string>(errorMessage);
        }
        public static ObjectResult BadRequestEx(this Controller context, string message)
        {
            return context.BadRequestEx<string>(message);
        }

        #endregion

        #region Simple<T>

        public static ObjectResult CreatedEx<T>(this Controller context)
        {
            return context.StatusCode((int)HttpStatusCode.Created
                , new ApiResult<T>
                {
                    Success = true,
                    Result = default(T),
                    Error = null
                });
        }
        public static ObjectResult NoContentEx<T>(this Controller context)
        {
            return context.StatusCode((int)HttpStatusCode.NoContent
                , new ApiResult<T>
                {
                    Success = true,
                    Result = default(T),
                    Error = null
                });
        }
        public static ObjectResult NotFoundEx<T>(this Controller context)
        {
            return context.StatusCode((int)HttpStatusCode.NotFound
                , new ApiResult<T>
                {
                    Success = true,
                    Result = default(T),
                    Error = null
                });
        }
        public static BadRequestObjectResult BadRequestEx<T>(this Controller context, string message)
        {
            return context.BadRequest(new ApiResult<T>
            {
                Success = false,
                Result = default(T),
                Error = new Dictionary<string, object>
                {
                    { "code" , 1 },
                    { "message" , "A required parameter is missing or doesn't have the right format:" + message}
                    , { "details",null}
                    , {"validationErrors",null }
                }
            });
        }
        public static ObjectResult ErrorEx<T>(this Controller context, string errorMessage)
        {
            return context.StatusCode((int)HttpStatusCode.InternalServerError
                , new ApiResult<T>
                {
                    Success = false,
                    Result = default(T),
                    Error = new Dictionary<string, object>
                    {
                        { "code" , 2 },
                        { "message" , errorMessage}
                        , { "details",null}
                        , {"validationErrors",null }
                    }

                });
        }

        #endregion

        #region Stay

        public static BadRequestObjectResult BadRequestEx<T>(this Controller context, Dictionary<string, object> dicError)
        {
            return context.BadRequest(new ApiResult<T>
            {
                Success = false,
                Result = default(T),
                Error = dicError
            });
        }
        public static ObjectResult ErrorEx<T>(this Controller context, Dictionary<string, object> dicError)
        {
            return context.StatusCode((int)HttpStatusCode.InternalServerError
                , new ApiResult<T>
                {
                    Success = false,
                    Result = default(T),
                    Error = dicError
                });
        }

        #endregion

    }
}
