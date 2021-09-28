using System;
using System.Collections.Generic;
using AnswersApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnswersApi.Controllers
{
    [ApiController]
    [Route("[controller]/{answerId:guid}")]
    public class AnswersController : ControllerBase
    {
        /// <summary>
        /// Метод сохраняет файлы в виде блобов в хранилище Azure Storage
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="files">Список файлов</param>
        /// <returns></returns>
        [HttpPost("attachments")]
        public ActionResult Attachments(Guid answerId, IEnumerable<IFormFile> files)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Метод сохраняет события в SQL базе в таблице AnswerEvents
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="events">Список событий</param>
        /// <returns></returns>
        [HttpPost("events")]
        public ActionResult Events(Guid answerId, [FromBody] AnswerEvent[] events)
        {
            throw new NotImplementedException();
        }
    }
}