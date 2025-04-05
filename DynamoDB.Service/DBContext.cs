using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDB.Service;

public class DBContext<T> : IDisposable where T:BaseEntity
{
    private readonly IDynamoDBContext _context;

    public DBContext(IAmazonDynamoDB client)
    {
        _context = new DynamoDBContext(client);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<List<T>> GetAllProductsAsync()
    {
        var results = await _context.ScanAsync<T>(new List<ScanCondition>()).GetRemainingAsync();
        return results;
    }

    public async Task<T> GetProductByIdAsync(Guid id)
    {
        return await _context.LoadAsync<T>(id);
    }

    public async Task AddProductAsync(T product)
    {
        await _context.SaveAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _context.DeleteAsync<T>(id);
    }

    public async Task UpdateProductAsync(T product)
    {
        await _context.SaveAsync(product);
    }
}
