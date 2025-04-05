using Amazon.DynamoDBv2.DataModel;

namespace DynamoDB.Service;

[DynamoDBTable("ObjetMetaData")]
public class ObjectMetaData : BaseEntity
{
    [DynamoDBHashKey]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DynamoDBProperty]
    public string ETag { get; set; } = default!;
    [DynamoDBProperty]
    public DateTime LastModified { get; set; }
    [DynamoDBProperty]
    public string BucketName { get; set; } = default!;
    [DynamoDBProperty]
    public string Key { get; set; } = default!;
}
