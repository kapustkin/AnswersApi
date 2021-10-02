using AnswersApi.Common.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace AnswersApi.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Метод возращает не успешный ответ
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static ActionResult Error(object message)
        {
            // добавить запись таких ошибок в лог или прометеус
            return new BadRequestObjectResult(message);
        }

        /// <summary>
        /// Метод возваращет результат или ошибку
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected ActionResult Result<T>(DataResult<T> result)
        {
            return result.IsNonSuccess 
                ? Error(new
                {
                   errorMessage = result.ErrorMessage
                }) 
                : Ok(result.Result);
        }
    }
}