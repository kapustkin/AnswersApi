using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnswersApi.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа к базе данных
    /// </summary>
    public interface IDataBaseService
    {
        /// <summary>
        /// Метод сохранения информации о вложениях
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="attachment">Информация о вложениях</param>
        /// <returns></returns>
        Task<DataResult<bool>> SaveAttachments(Guid answerId, IEnumerable<AttachmentInfo> attachment);

        /// <summary>
        /// Метод сохранения информации о вложениях
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="attachment">Информация о вложении</param>
        /// <returns></returns>
        Task<DataResult<bool>> SaveAttachment(Guid answerId, AttachmentInfo attachment);

        /// <summary>
        /// Метод сохранения информации о событиях
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        Task<DataResult<bool>> SaveEvents(Guid answerId, IEnumerable<AnswerEvent> events);
    }
}