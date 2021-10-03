using AnswersApi.Common.Interfaces;
using Moq;

namespace AnswersApi.Tests.Services.AnswersService
{
    public abstract class BaseAnswersServiceTests
    {
        protected readonly Mock<IDataBaseService> DataBaseService = new();
        protected readonly Mock<IStorageService> StorageService = new();
        protected readonly AnswersApi.Services.AnswersService Service;

        protected BaseAnswersServiceTests()
        {
            Service = new AnswersApi.Services.AnswersService(DataBaseService.Object, StorageService.Object);
        }
    }
}
