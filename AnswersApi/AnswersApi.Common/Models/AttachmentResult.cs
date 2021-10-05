namespace AnswersApi.Common.Models
{
    /// <summary>
    /// Модель ответа метода сохранения файлов
    /// </summary>
    public class AttachmentResult
    {
        /// <summary>
        /// Признак успешного сохранения
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }
    }
}