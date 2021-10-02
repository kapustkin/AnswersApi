using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnswersApi.Common.Interfaces;
using AnswersApi.Controllers.Base;
using AnswersApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("attachments")]
        public async Task<ActionResult> Attachments(Guid answerId, IEnumerable<IFormFile> files)
        {
            var result = await _answersService.AttachmentFiles(answerId, files);
            return Result(result);
        }
        
        /// <summary>
        /// Метод сохраняет события
        /// </summary>
        /// <param name="answerId">Идентификатор ответа</param>
        /// <param name="events">Список событий</param>
        /// <returns></returns>
        [HttpPost("events")]
        public async Task<ActionResult> Events(Guid answerId, [FromBody] AnswerEvent[] events)
        {
            var result = await _answersService.SaveEvents(answerId, _mapper.Map<Common.Models.AnswerEvent[]>(events));
            return Result(result);
        }
    }
}