using Amazon.DynamoDBv2.DataModel;

namespace DynamoDB.Service;

[DynamoDBTable("SNSMessage")]
public class SNSMessage : BaseEntity
{
    [DynamoDBHashKey]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DynamoDBProperty]
    public string Message { get; set; }
    [DynamoDBProperty]
    public string Subject { get; set; }
    [DynamoDBProperty]
    public string TopicArn { get; set; }
    [DynamoDBProperty]
    public List<string> Subscribers { get; set; } = new List<string>();
}

