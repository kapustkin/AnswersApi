using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using System;
using System.Collections.Generic;
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

            var result = new DataResult<IList<AttachmentResult>>()
            {
                Result = new List<AttachmentResult>()
            };

            foreach (var file in files)
            {
                var attachmentResult = new AttachmentResult
                {
                    FileName = file.FileName
                };

                result.Result.Add(attachmentResult);

                var upload = await _storageService.UploadAsync(file.Stream, file.FileName, file.MimeType);
                if (upload.IsNonSuccess)
                {
                    continue;
                }

                var saveAttachment = await _dataBaseService.SaveAttachment(answerId, file);
                if (saveAttachment.IsNonSuccess)
                {
                    continue;
                }

                attachmentResult.IsSuccess = true;
            }

            return result;
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
    }
}