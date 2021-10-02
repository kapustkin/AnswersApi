using AnswersApi.Models;
using AnswersApi.Models.Enums;
using AutoMapper;

namespace AnswersApi.Mappers
{
    // ReSharper disable once UnusedType.Global
    public class AnswersMapper : Profile
    {
        public AnswersMapper()
        {
            CreateMap<AnswerEvent, Common.Models.AnswerEvent>();
            CreateMap<AnswerEventType, Common.Models.Enums.AnswerEventType>();
        }
    }
}