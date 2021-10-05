using AnswersApi.Common.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace AnswersApi.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Метод возвращает не успешный ответ
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static ActionResult Error(object message)
        {
            // добавить запись ошибок в лог или прометеус
            return new BadRequestObjectResult(message);
        }

        /// <summary>
        /// Метод возвращает результат или ошибку
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected ActionResult Result<T>(DataResult<T> result)
        {
            return result.IsNonSuccess
                ? Error(new { errorMessage = result.ErrorMessage })
                : Ok(new { result = result.Result });
        }
    }
}