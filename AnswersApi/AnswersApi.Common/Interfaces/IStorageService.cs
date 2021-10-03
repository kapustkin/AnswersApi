using AnswersApi.Common.Models.Base;
using System.IO;
using System.Threading.Tasks;

namespace AnswersApi.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с хранилищем
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Метод отправляет файл в хранилище
        /// </summary>
        /// <param name="fileStream">Поток файла</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="contentType">Тип содержимого</param>
        /// <returns></returns>
        Task<DataResult<string>> UploadAsync(Stream fileStream, string fileName, string contentType);
    }
}