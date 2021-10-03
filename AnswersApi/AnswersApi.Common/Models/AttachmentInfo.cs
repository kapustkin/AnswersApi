using System;

namespace AnswersApi.Common.Models
{
    /// <summary>
    /// Информация о вложении
    /// </summary>
    public class AttachmentInfo
    {
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