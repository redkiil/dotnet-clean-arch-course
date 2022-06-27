using System;
using Xunit;
using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using static FluentAssertions.FluentActions;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Invoking(() => new Product(1,"Product Name", "Valid Description", 100, 10, "https://image.com/product.jpg")).Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact]
        public void CreateProduct_WithNoName_ResultException()
        {
            Invoking(() => new Product(1,"", "Valid Description", 100, 10, "https://image.com/product.jpg")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. name is required!");
        }
        [Fact]
        public void CreateProduct_WithShortName_ResultException()
        {
            Invoking(() => new Product(1,"Pr", "Valid Description", 100, 10, "https://image.com/product.jpg")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is to short!");
        }
        [Fact]
        public void CreateProduct_WithBigName_ResultException()
        {
            Invoking(() => new Product(1,"Product Name xxx Product Name xxx Product Name xxx Product Name xxx Product Name xxx Product Name xxxx"
                , "Valid Description", 100, 10, "https://image.com/product.jpg"))
                    .Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                        .WithMessage("Invalid name. name is to big!");

        }
        [Fact]
        public void CreateProduct_WithNoDesc_ResultException()
        {
            Invoking(() => new Product(1,"Product Name", "", 100, 10, "https://image.com/product.jpg")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description. description is required!");
        }
        [Fact]
        public void CreateProduct_WithShortDesc_ResultException()
        {
            Invoking(() => new Product(1,"Product Name", "De", 100, 10, "https://image.com/product.jpg")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description. Description is to short!");
        }
        [Fact]
        public void CreateProduct_WithBigDesc_ResultException()
        {
            Invoking(() => new Product(1,"Product Name", "Description Product xxx Description Product xxx Description Product xxx Description Product xxx Description Product xxx"
                , 100, 10, "https://image.com/product.jpg"))
                    .Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                        .WithMessage("Invalid description. Description is to big!");
        }
        [Theory]
        [InlineData(-10)]
        public void CreateProduct_WithInvalidPrice_ResultException(int price)
        {
            Invoking(() => new Product(1,"Product Name", "Description Product", price, 10, "https://image.com/product.jpg")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid price. price must be greater than 0!");
        }
        [Theory]
        [InlineData(-10)]
        public void CreateProduct_WithInvalidStock_ResultException(int stock)
        {
            Invoking(() => new Product(1,"Product Name", "Description Product", 100, stock, "https://image.com/product.jpg")).Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock. stock must be greater than 0!");
        }
        [Fact]
        public void CreateProduct_WithBigImageUrl_ResultException()
        {
            Invoking(() => new Product(1,"Product Name", "Description Product", 100, 10
                , "https://image.com/productAIOSDJjdo819123901391239901iasjdioasjdaisodjiaos83971293uasidjisaojd102893123239901iasjdioasj023jd13911283971293uasidjisaojd102893123239901iasjdioasj0238181318381jsdjoad12323j189jda98sj189212093jkSAJHD#!SD)123jasdijasiod18IASDJj12i3hjhas.jpg"))
                    .Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                        .WithMessage("Invalid image url. url is to big!");
        }
        [Fact]
        public void CreateProduct_WithNoImage_ResultValidObject()
        {
            Invoking(() => new Product(1,"Product Name", "Description Product", 100, 10, null))
                .Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact]
        public void CreateProduct_WithNoImage_ResultNoNullException()
        {
            Invoking(() => new Product(1,"Product Name", "Description Product", 100, 10, null))
                .Should().NotThrow<NullReferenceException>();
        }
    }
}