using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using Microsoft.AspNetCore.Http;
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
        private const string ErrorMessageSaveAttachments = "Ошибка при сохранении информации о файлах в базу данных";

        private readonly IDataBaseService _dataBaseService;
        private readonly IStorageService _storageService;

        public AnswersService(
            IDataBaseService dataBaseService,
            IStorageService storageService)
        {
            _dataBaseService = dataBaseService;
            _storageService = storageService;
        }

        public async Task<DataResult<bool?>> AttachmentFiles(Guid answerId, IEnumerable<IFormFile> files)
        {
            //TODO можно оптимизировать
            //Что делать если загрузилась только часть файлов?
            //Какой ответ в этом случае ожидается? 
            //Требуется ли при ошибке делать откат изменений?
            foreach (var file in files)
            {
                var upload = await _storageService.UploadAsync(file.OpenReadStream(), file.FileName, file.ContentType);
                if (upload.IsNonSuccess)
                {
                    return new DataResult<bool?>
                    {
                        Result = false,
                        ErrorMessage = "При загрузке файлов произошла ошибка"
                    };
                }

                var saveAttachment = await _dataBaseService.SaveAttachment(answerId, new AttachmentInfo()
                {
                    Created = DateTime.Now,
                    Size = file.Length,
                    FileName = file.FileName,
                    MimeType = file.ContentType
                });
                if (saveAttachment.IsNonSuccess)
                {
                    return new DataResult<bool?>
                    {
                        Result = saveAttachment.Result,
                        ErrorMessage = ErrorMessageSaveAttachments
                    };
                }
            }

            return new DataResult<bool?> { Result = true };
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