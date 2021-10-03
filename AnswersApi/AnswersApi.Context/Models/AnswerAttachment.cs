using Microsoft.EntityFrameworkCore;
using System;

namespace AnswersApi.Context.Models
{
    /// <summary>
    /// Модель данных вложения
    /// </summary>
    [Index(nameof(AnswerId))]
    public class AnswerAttachment
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id {get; set;}
        
        /// <summary>
        /// Ид ответа
        /// </summary>
        public Guid AnswerId { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }
        
        /// <summary>
        /// Название файла
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// Тип файла
        /// </summary>
        public string MimeType { get; set; }
        
        /// <summary>
        /// Размер файла
        /// </summary>
        public long Size { get; set; }
    }
}