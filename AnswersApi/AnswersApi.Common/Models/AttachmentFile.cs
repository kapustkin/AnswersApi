using System.IO;

namespace AnswersApi.Common.Models
{
    public class AttachmentFile : AttachmentInfo
    {
        /// <summary>
        /// Содержимое файла
        /// </summary>
        public Stream Stream { get; set; }
    }
}
