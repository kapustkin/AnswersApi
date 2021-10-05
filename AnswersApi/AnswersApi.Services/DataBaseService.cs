using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using AnswersApi.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnswersApi.Services
{
    /// <summary>
    /// Реализация сервиса по работе с базой данных
    /// </summary>
    public class DataBaseService : IDataBaseService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<DataBaseService> _logger;

        public DataBaseService(ApplicationContext context, ILogger<DataBaseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<DataResult<bool>> SaveAttachment(Guid answerId, AttachmentInfo attachment)
        {
            return SaveAttachments(answerId, new[] { attachment });
        }

        public async Task<DataResult<bool>> SaveAttachments(Guid answerId, IEnumerable<AttachmentInfo> attachment)
        {
            try
            {
                await _context.AnswerAttachments.AddRangeAsync(attachment.Select(s => new Context.Models.AnswerAttachment
                {
                    Id = new Guid(),
                    AnswerId = answerId,
                    Created = s.Created,
                    Size = s.Size,
                    FileName = s.FileName,
                    MimeType = s.MimeType
                }));

                var result = await _context.SaveChangesAsync();

                return new DataResult<bool>
                {
                    Result = result > 0
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении информации о файлах в базу данных");
                return new DataResult<bool>
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<DataResult<bool>> SaveEvents(Guid answerId, IEnumerable<AnswerEvent> events)
        {
            try
            {
                await _context.AnswerEvents.AddRangeAsync(events.Select(s => new Context.Models.AnswerEvent
                {
                    Id = new Guid(),
                    AnswerId = answerId,
                    Type = s.Type,
                    Value = s.Value,
                    ClientTime = s.ClientTime
                }));

                var result = await _context.SaveChangesAsync();

                return new DataResult<bool>
                {
                    Result = result > 0
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении событий в базу данных");
                return new DataResult<bool>
                {
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}