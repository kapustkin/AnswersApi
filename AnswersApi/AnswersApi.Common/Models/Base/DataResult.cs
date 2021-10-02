namespace AnswersApi.Common.Models.Base
{
    /// <summary>
    /// Общая модель ответа для всех методов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataResult<T>
    {
        /// <summary>
        /// Данные ответа
        /// </summary>
        public T Result { get; set; }
        
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Флаг успешного выполнения запроса
        /// </summary>
        public bool IsNonSuccess => Result == null || ErrorMessage != null;
    }
}