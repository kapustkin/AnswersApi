using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using Microsoft.AspNetCore.Http;

namespace AnswersApi.Services
{
    /// <summary>
    /// Реализация сервиса по сохранению ответов
    /// </summary>
    public class AnswersService : IAnswersService
    {
        public Task<DataResult<bool?>> AttachmentFiles(Guid answerId, IEnumerable<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<bool?>> SaveEvents(Guid answerId, IEnumerable<AnswerEvent> events)
        {
            return Task.FromResult(new DataResult<bool?>()
            {
                ErrorMessage = "Метод в разработке"
            });
        }
    }
}