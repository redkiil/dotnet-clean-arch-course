using Xunit;
using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using static FluentAssertions.FluentActions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Invoking(() => new Category(1,"Category Name")).Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact]
        public void CreateCategory_NegativeIdValue_ResultThrowError()
        {
            Invoking(() => new Category(-1,"Category Name")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid Id value!");
        }
        [Fact]
        public void CreateCategory_ShortName_ResultThrowError()
        {
            Invoking(() => new Category(1,"Ca")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name, Name is too short!");
        }
        [Fact]
        public void CreateCategory_NoName_ResultThrowError()
        {
            Invoking(() => new Category(1,"")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name, Name is required!");
        }
        [Fact]
        public void CreateCategory_BigName_ResultThrowError()
        {
            Invoking(() => new Category(1,"Category Name xxx Category Name xxx Category Name xxx Category Name xxx Category Name xxx Category Name xxx Category Name xxx "))
                .Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name, Name is too big!");
        }
    }
}