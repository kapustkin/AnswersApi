using AnswersApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace AnswersApi.Controllers
{
    /// <summary>
    /// Контроллер со служебными методами
    /// </summary>
    public class InfoController : BaseController
    {
        /// <summary>
        /// Метод проверки доступности сервиса
        /// </summary>
        /// <returns></returns>
        public ActionResult Health()
        {
            return new OkResult();
        }
    }
}