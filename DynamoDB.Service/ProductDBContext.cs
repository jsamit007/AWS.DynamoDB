using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDB.Service;

public class ProductDBContext : IDisposable
{
    private readonly IDynamoDBContext _context;

    public ProductDBContext(IAmazonDynamoDB client)
    {
        _context = new DynamoDBContext(client);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = await _context.ScanAsync<Product>(new List<ScanCondition>()).GetRemainingAsync();
        return products;
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await _context.LoadAsync<Product>(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.SaveAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _context.DeleteAsync<Product>(id);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _context.SaveAsync(product);
    }
}
