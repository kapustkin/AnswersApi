using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models.Base;
using AnswersApi.Controllers.Base;
using AnswersApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnswersApi.Controllers
{
    [ApiController]
    [Route("[controller]/{answerId:guid}")]
    public class AnswersController : BaseController
    {
        private readonly IAnswersService _answersService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public AnswersController(
            IAnswersService answersService,
            IMapper mapper)
        {
            _answersService = answersService;
            _mapper = mapper;
        }


        /// <summary>
        /// Метод сохраняет файлы
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="files">Список файлов</param>
        /// <returns></returns>
        [HttpPost("attachments"), DisableRequestSizeLimit]
        public async Task<ActionResult> Attachments(Guid answerId, IEnumerable<IFormFile> files)
        {
            var result = await _answersService.AttachmentFiles(answerId, _mapper.Map<IEnumerable<Common.Models.AttachmentFile>>(files));
            return Result(new DataResult<IEnumerable<AttachmentResult>>()
            {
                Result = _mapper.Map<IEnumerable<AttachmentResult>>(result.Result)
            });
        }

        /// <summary>
        /// Метод сохраняет события
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="events">Список событий</param>
        /// <returns></returns>
        [HttpPost("events")]
        public async Task<ActionResult> Events(Guid answerId, [FromBody] Models.AnswerEvent[] events)
        {
            var result = await _answersService.SaveEvents(answerId, _mapper.Map<Common.Models.AnswerEvent[]>(events));
            return Result(result);
        }
    }
}