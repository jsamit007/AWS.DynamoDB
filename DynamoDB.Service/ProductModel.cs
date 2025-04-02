using Amazon.DynamoDBv2.DataModel;

namespace DynamoDB.Service;

[DynamoDBTable("Product")]
public class ProductModel
{
    [DynamoDBHashKey]
    [DynamoDBProperty]
    public string Id { get; set; }
    [DynamoDBProperty]
    public string Category { get; set; }
    [DynamoDBProperty]
    public string Name { get; set; }
}
