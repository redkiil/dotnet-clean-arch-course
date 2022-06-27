
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name {get;private set;}
        public string Description {get;private set;}
        public decimal Price {get;private set;}
        public int Stock {get;private set;}
        public string Image {get;private set;}

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name,description,price,stock,image);
        }
        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "invalid id value, must be greater than 0!");
            Id = id;
            ValidateDomain(name,description,price,stock,image);
        }
        public void Update(string name, string description, decimal price, int stock, string image, int categoryid)
        {
            ValidateDomain(name,description,price,stock,image);
            CategoryId = categoryid;
        }
        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. name is required!");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name. Name is to short!");
            DomainExceptionValidation.When(name.Length > 100, "Invalid name. name is to big!");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. description is required!");
            DomainExceptionValidation.When(description.Length < 3, "Invalid description. Description is to short!");
            DomainExceptionValidation.When(description.Length > 100, "Invalid description. Description is to big!");
            
            DomainExceptionValidation.When(price < 0, "Invalid price. price must be greater than 0!");

            DomainExceptionValidation.When(stock < 0, "Invalid stock. stock must be greater than 0!");
            
            DomainExceptionValidation.When(image?.Length > 250, "Invalid image url. url is to big!");
            
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }

        public int CategoryId {get;set;}
        public Category Category {get;set;}
    }
}