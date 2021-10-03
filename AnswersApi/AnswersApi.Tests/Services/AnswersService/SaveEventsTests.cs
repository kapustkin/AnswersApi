using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AnswersApi.Tests.Services.AnswersService
{
    public class SaveEventsTests : BaseAnswersServiceTests
    {
        public class TestData
        {
            public Guid AnswerId { get; init; }
            public IEnumerable<AnswerEvent> Events { get; init; }
            public DataResult<bool> SaveEvents { get; init; }
            public DataResult<bool?> Expected { get; init; }
        }

        public static List<object[]> TestDataList = new()
        {
            new object[] {
                new TestData
                {
                    AnswerId = new Guid(),
                    Events = null,
                    SaveEvents = new DataResult<bool>
                    {
                        ErrorMessage = "Ошибка"
                    },
                    Expected = new DataResult<bool?>
                    {
                        Result = false,
                        ErrorMessage = "Ошибка при сохранении информации о событиях в базу данных"
                    }
                }
            },
            new object[] {
                new TestData
                {
                    AnswerId = new Guid(),
                    Events = new List<AnswerEvent>{ new() { Type = Common.Models.Enums.AnswerEventType.Drag } },
                    SaveEvents = new DataResult<bool>
                    {
                        Result = true
                    },
                    Expected = new DataResult<bool?>
                    {
                        Result = true
                    }
                }
            }
        };

        [Theory]
        [MemberData(nameof(TestDataList))]
        public async Task SaveEventsTest(TestData data)
        {
            DataBaseService.Setup(s => s.SaveEvents(data.AnswerId, data.Events)).ReturnsAsync(data.SaveEvents);

            var result = await Service.SaveEvents(data.AnswerId, data.Events);

            result.ShouldBeEquivalentTo(data.Expected);
        }
    }
}
