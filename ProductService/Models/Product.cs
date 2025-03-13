using Common.DTOs;

namespace ProductService.Models;

public class Product
{
	public int Id {get; set;}
	public string Name {get; set;} = string.Empty;
	public string Description {get; set;} = string.Empty;
	public decimal Price {get; set;}
	public int Stock {get; set;}
	public int CategoryId {get; set;}
	public Category? Category {get; set;}


	public static Product FromDto(ProductDto product)
	{
		return new Product {
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			Stock = product.Stock,
			CategoryId = product.CategoryId
		};
	}

	public ProductDto ToDto()
	{
		return new ProductDto
		{
			Id = Id,
			Name = Name,
			Description = Description,
			Price = Price,
			Stock = Stock
		};
	}
}