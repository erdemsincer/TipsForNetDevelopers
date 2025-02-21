using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Index(nameof(Product.Name),IsUnique=true)]
public sealed class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}
