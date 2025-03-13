using Common.DTOs;

namespace ProductService.Models;

public class Category
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;


	public CategoryDto ToDto()
	{
		return new CategoryDto
		{
			Id = Id,
			Name = Name,
			Description = Description
		};
	}

	public static Category FromDto(CategoryDto category)
	{
		return new Category
		{
			Id = category.Id,
			Name = category.Name,
			Description = category.Description
		};
	}
}