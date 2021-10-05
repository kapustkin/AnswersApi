using AnswersApi.Models;
using AnswersApi.Models.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AnswersApi.Mappers
{
    // ReSharper disable once UnusedType.Global
    public class AnswersMapper : Profile
    {
        public AnswersMapper()
        {
            CreateMap<AnswerEvent, Common.Models.AnswerEvent>();
            CreateMap<AnswerEventType, Common.Models.Enums.AnswerEventType>();

            CreateMap<IFormFile, Common.Models.AttachmentFile>().ConvertUsing(new FormFileToAttachmentFileConverter());
            CreateMap<Common.Models.AttachmentResult, AttachmentResult>();
        }


        private class FormFileToAttachmentFileConverter : ITypeConverter<IFormFile, Common.Models.AttachmentFile>
        {
            public Common.Models.AttachmentFile Convert(IFormFile source, Common.Models.AttachmentFile destination, ResolutionContext context)
            {
                return new Common.Models.AttachmentFile
                {
                    Created = System.DateTime.Now,
                    FileName = source.FileName,
                    MimeType = source.ContentType,
                    Size = source.Length,
                    Stream = source.OpenReadStream()
                };
            }
        }
    }
}