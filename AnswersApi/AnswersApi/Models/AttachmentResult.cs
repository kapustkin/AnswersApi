using System.Text.Json.Serialization;

namespace AnswersApi.Models
{
    /// <summary>
    /// Модель ответа метода сохранения файлов
    /// </summary>
    public class AttachmentResult
    {
        /// <summary>
        /// Признак успешного сохранения
        /// </summary>
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
    }
}