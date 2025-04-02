using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Service;

public class DynamoDbDDLService : IDisposable
{
    private readonly IAmazonDynamoDB _client;

    public DynamoDbDDLService(IAmazonDynamoDB client)
    {
        _client = client;        
    }

    public async Task<CreateTableResponse> Create(string tableName)
    {
        var response = await _client.CreateTableAsync(new CreateTableRequest
        {
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "Id",
                    AttributeType = ScalarAttributeType.S
                }
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Id",
                    KeyType = KeyType.HASH
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 1,
                WriteCapacityUnits = 1
            },
            TableName = tableName,
            StreamSpecification = new StreamSpecification
            {
                StreamEnabled = false
            }
        });

        return response;
    }

    public async Task<DeleteTableResponse> Delete(string tableName)
    {
        return await _client.DeleteTableAsync(new DeleteTableRequest
        {
            TableName = tableName
        });
    }

    public async Task<DescribeTableResponse> Describe(string tableName)
    {
        return await _client.DescribeTableAsync(new DescribeTableRequest
        {
            TableName = tableName
        });
    }

    public async Task<ListTablesResponse> List()
    {
        return await _client.ListTablesAsync();
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
