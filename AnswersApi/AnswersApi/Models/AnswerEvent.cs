using System;
using System.Text.Json.Serialization;
using AnswersApi.Models.Enums;

namespace AnswersApi.Models
{
    /// <summary>
    /// Событие
    /// </summary>
    public class AnswerEvent
    {
        /// <summary>
        /// Значение
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// Тип действия
        /// </summary>
        [JsonPropertyName("type")]
        public AnswerEventType Type { get; set; }

        /// <summary>
        /// Время у клиента
        /// </summary>
        [JsonPropertyName("clientTime")]
        public DateTime ClientTime { get; set; }
    }
}