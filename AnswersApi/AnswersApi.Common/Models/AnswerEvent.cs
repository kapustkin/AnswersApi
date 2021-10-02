using System;
using System.Text.Json.Serialization;
using AnswersApi.Common.Models.Enums;

namespace AnswersApi.Common.Models
{
    /// <summary>
    /// Событие
    /// </summary>
    public class AnswerEvent
    {
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