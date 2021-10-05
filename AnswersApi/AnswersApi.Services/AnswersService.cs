using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnswersApi.Services
{
    /// <summary>
    /// Реализация сервиса по сохранению ответов
    /// </summary>
    public class AnswersService : IAnswersService
    {
        private const string ErrorMessageSaveEvents = "Ошибка при сохранении информации о событиях в базу данных";
        private const string ErrorMessageSaveAttachments = "Пустой список файлов не допускается";

        private readonly IDataBaseService _dataBaseService;
        private readonly IStorageService _storageService;

        public AnswersService(
            IDataBaseService dataBaseService,
            IStorageService storageService)
        {
            _dataBaseService = dataBaseService;
            _storageService = storageService;
        }

        public async Task<DataResult<IList<AttachmentResult>>> AttachmentFiles(Guid answerId, IEnumerable<AttachmentFile> files)
        {
            if (files == null) return new DataResult<IList<AttachmentResult>>
            {
                ErrorMessage = ErrorMessageSaveAttachments
            };

            var result = await Task.WhenAll(files.Select(s => GetAttachmentResultAsync(answerId, s)));

            return new DataResult<IList<AttachmentResult>>
            {
                Result = result.ToList()
            };
        }

        public async Task<DataResult<bool?>> SaveEvents(Guid answerId, IEnumerable<AnswerEvent> events)
        {
            var saveEvents = await _dataBaseService.SaveEvents(answerId, events);
            if (saveEvents.IsNonSuccess)
            {
                return new DataResult<bool?>
                {
                    Result = saveEvents.Result,
                    ErrorMessage = ErrorMessageSaveEvents
                };
            }

            return new DataResult<bool?> { Result = true };
        }

        /// <summary>
        /// Метод загружает файл в хранилище и делает запись в базу данных
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="file">Файл для загрузки</param>
        /// <returns></returns>
        private async Task<AttachmentResult> GetAttachmentResultAsync(Guid answerId, AttachmentFile file)
        {
            var attachmentResult = new AttachmentResult
            {
                FileName = file.FileName
            };

            var upload = await _storageService.UploadAsync(file.Stream, file.FileName, file.MimeType);
            if (upload.IsNonSuccess)
            {
                return attachmentResult;
            }

            var saveAttachment = await _dataBaseService.SaveAttachment(answerId, file);
            if (saveAttachment.IsNonSuccess)
            {
                return attachmentResult;
            }

            attachmentResult.IsSuccess = true;

            return attachmentResult;
        }
    }
}