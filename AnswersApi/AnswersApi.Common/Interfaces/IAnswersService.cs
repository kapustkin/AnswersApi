using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using Microsoft.AspNetCore.Http;

namespace AnswersApi.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с сервисом ответов
    /// </summary>
    public interface IAnswersService
    {
        /// <summary>
        /// Метод сохраняет файлы
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="files">Список файлов</param>
        /// <returns></returns>
        Task<DataResult<bool?>> AttachmentFiles(Guid answerId, IEnumerable<IFormFile> files);
        
        /// <summary>
        /// Метод сохраняет события
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="events">Список событий</param>
        /// <returns></returns>
        Task<DataResult<bool?>> SaveEvents(Guid answerId, IEnumerable<AnswerEvent> events);
    }
}