using Amazon.DynamoDBv2.DataModel;

namespace DynamoDB.Service;

[DynamoDBTable("Product")]
public class Product
{
    [DynamoDBHashKey]
    [DynamoDBProperty]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DynamoDBProperty]
    public string Category { get; set; } = default!;
    [DynamoDBProperty]
    public string Name { get; set; } = default!;
}
