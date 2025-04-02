using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Service;

public class PartiQLService
{
    private readonly IAmazonDynamoDB _client;

    public PartiQLService(IAmazonDynamoDB client)
    {
        _client = client;
    }

    public async Task<ExecuteStatementResponse> Insert(string tableName)
    {
        var request = new ExecuteStatementRequest
        {
            Statement = $"INSERT INTO {tableName} VALUE {{'Id': ?, 'Category': ?, 'Name': ?}} ",
            Parameters = new List<AttributeValue>
            {
                new AttributeValue {S = "8"},
                new AttributeValue {S = "Mobile"},
                new AttributeValue {S = "I-Phone 16 Pro Max"}
            }
        };

        return await _client.ExecuteStatementAsync(request);
    }

    public async Task<ExecuteStatementResponse> Read(string tableName)
    {
        var request = new ExecuteStatementRequest
        {
            Statement = $"SELECT * FROM {tableName} WHERE Id = ? ",
            Parameters = new List<AttributeValue>
            {
                new AttributeValue {S = "8"}
            }
        };

        return await _client.ExecuteStatementAsync(request);
    }

    public async Task<ExecuteStatementResponse> Update(string tableName)
    {
        var request = new ExecuteStatementRequest
        {
            Statement = $"UPDATE {tableName} SET Category = ? WHERE Id = ?",
            Parameters = new List<AttributeValue>
            {
                new AttributeValue { S = "Tablet" }, // new attribute value
                new AttributeValue { S = "8" } // id
            }
        };
        return await _client.ExecuteStatementAsync(request);
    }

    public async Task<ExecuteStatementResponse> Delete(string tableName)
    {
        var request = new ExecuteStatementRequest
        {
            Statement = $"DELETE FROM {tableName} WHERE Id = ?",
            Parameters = new List<AttributeValue>
            {
                new AttributeValue { S = "8" } // id
            }
        };
        return await _client.ExecuteStatementAsync(request);
    }

    public async Task<ExecuteStatementResponse> GetAll(string tableName)
    {
        var request = new ExecuteStatementRequest
        {
            Statement = $"SELECT * FROM {tableName}"
        };
        return await _client.ExecuteStatementAsync(request);
    }
}
