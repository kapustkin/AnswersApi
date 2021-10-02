using AnswersApi.Mappers;
using AutoMapper;
using Xunit;

namespace AnswersApi.Tests
{
    public class MapperConfigurationTests
    {
        [Fact]
        public void ShouldBeValidMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AnswersMapper>());
            config.AssertConfigurationIsValid();
        }
    }
}