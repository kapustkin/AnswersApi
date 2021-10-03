using System;
using AnswersApi.Common.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AnswersApi.Context.Models
{
    /// <summary>
    /// Модель данных для событий
    /// </summary>
    [Index(nameof(AnswerId))]
    public class AnswerEvent
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ид ответа
        /// </summary>
        public Guid AnswerId { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Тип действия
        /// </summary>
        public AnswerEventType Type { get; set; }

        /// <summary>
        /// Время у клиента
        /// </summary>
        public DateTime ClientTime { get; set; }
    }
}