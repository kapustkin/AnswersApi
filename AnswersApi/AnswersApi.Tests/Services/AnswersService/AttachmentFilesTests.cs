using AnswersApi.Common.Models;
using AnswersApi.Common.Models.Base;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AnswersApi.Tests.Services.AnswersService
{
    public class AttachmentFilesTests : BaseAnswersServiceTests
    {
        public class TestData
        {
            public Guid AnswerId => new();

            public IEnumerable<AttachmentFile> Attachments = new List<AttachmentFile>{ new()
            {
                Created = new DateTime(2021, 10, 5),
                FileName = "FileName",
                MimeType = "MimeType",
                Size = 123
            }};
            public DataResult<string> UploadAsync { get; init; }
            public DataResult<bool> SaveAttachment { get; init; }
            public DataResult<IList<AttachmentResult>> Expected { get; init; }
        }

        public static List<object[]> TestDataList = new()
        {
            new object[] {
                new TestData
                {
                    Attachments = null,
                    Expected = new DataResult<IList<AttachmentResult>>
                    {
                        ErrorMessage = "Пустой список файлов не допускается"
                    }
                }
            },
            new object[] {
                new TestData
                {
                    UploadAsync = new DataResult<string>
                    {
                        ErrorMessage = "Ошибка"
                    },
                    SaveAttachment = new DataResult<bool>
                    {
                        ErrorMessage = "Ошибка"
                    },
                    Expected = new DataResult<IList<AttachmentResult>>
                    {
                        Result = new List<AttachmentResult>{
                            new()
                            {
                                FileName = "FileName",
                                IsSuccess = false
                            }
                        },
                    }
                }
            },
            new object[] {
                new TestData
                {
                    UploadAsync = new DataResult<string>
                    {
                        Result = "success upload link"
                    },
                    SaveAttachment = new DataResult<bool>
                    {
                        ErrorMessage = "Ошибка"
                    },
                    Expected = new DataResult<IList<AttachmentResult>>
                    {
                        Result = new List<AttachmentResult>{
                            new()
                            {
                                FileName = "FileName",
                                IsSuccess = false
                            }
                        },
                    }
                }
            },
            new object[] {
                new TestData
                {
                    UploadAsync = new DataResult<string>
                    {
                        Result = "success upload link"
                    },
                    SaveAttachment = new DataResult<bool>
                    {
                        Result = true
                    },
                    Expected = new DataResult<IList<AttachmentResult>>
                    {
                        Result = new List<AttachmentResult>{
                            new()
                            {
                                FileName = "FileName",
                                IsSuccess = true
                            }
                        },
                    }
                }
            }
        };

        [Theory]
        [MemberData(nameof(TestDataList))]
        public async Task AttachmentFilesTest(TestData data)
        {
            StorageService.Setup(s => s.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(data.UploadAsync);
            DataBaseService.Setup(s => s.SaveAttachment(data.AnswerId, It.IsAny<AttachmentInfo>()))
                .ReturnsAsync(data.SaveAttachment);

            var result = await Service.AttachmentFiles(data.AnswerId, data.Attachments);

            result.ShouldBeEquivalentTo(data.Expected);
        }
    }
}
