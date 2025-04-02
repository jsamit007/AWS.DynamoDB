using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Service;

public class DynamoDMLService : IDisposable
{
    private readonly IAmazonDynamoDB _client;

    public DynamoDMLService(IAmazonDynamoDB client)
    {
        _client = client;
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public async Task<PutItemResponse> PutItem(string tableName,Dictionary<string,AttributeValue> item) 
    {
        return await _client.PutItemAsync(new PutItemRequest
        {
            TableName = tableName,
            Item = item
        });
    }

    public async Task<UpdateItemResponse> UpdateItem(string tableName, Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> updates)
    {
        return await _client.UpdateItemAsync(new UpdateItemRequest
        {
            TableName = tableName,
            Key = key,
            AttributeUpdates = updates
        });
    }

    public async Task<GetItemResponse> GetItem(string tableName, Dictionary<string,AttributeValue> key)
    {
        return await _client.GetItemAsync(new GetItemRequest
        {
            TableName = tableName,
            Key = key
        });
    }

    public async Task<DeleteItemResponse> DeleteItem(string tableName, Dictionary<string, AttributeValue> key)
    {
        return await _client.DeleteItemAsync(new DeleteItemRequest
        {
            TableName = tableName,
            Key = key
        });
    }

    public async Task<long> BatchWriteItem(string tableName,List<Product> products)
    {
        var context = new DynamoDBContext(_client);
        var batch = context.CreateBatchWrite<Product>();
        batch.AddPutItems(products);
        await batch.ExecuteAsync();
        return products.Count;
    }

    public async Task<BatchGetItemResponse> BatchGetItems(BatchGetItemRequest request)
    {
        return await _client.BatchGetItemAsync(request);
    }

    public async Task<List<Dictionary<string, object>>> Query(string tableName)
    {
        var records = new List<Dictionary<string,object>>();
        var table = Table.LoadTable(_client, tableName);
        var filter = new QueryFilter("Id",QueryOperator.Equal,"2");
        var config = new QueryOperationConfig
        {
            Limit = 10,
            Select = SelectValues.SpecificAttributes,
            AttributesToGet = new List<string> { "Category", "Name" },
            ConsistentRead = true,
            Filter = filter
        };

        var search = table.Query(config);

        do
        {
            var result = await search.GetNextSetAsync();
            foreach(var row in result)
            {
                Dictionary<string, object> keyValues = new();
                var keys = row.Keys;
                var values = row.Values;
                for(int i=0; i<keys.Count; i++)
                    keyValues.Add(keys.ElementAt(i),values.ElementAt(i));
                records.Add(keyValues);
            }
        }
        while (!search.IsDone);

        return records;
    }

    public async Task<List<ScanResponse>> Scan(string tableName)
    {
        var records = new List<ScanResponse>();
        var request = new ScanRequest
        {
            TableName = tableName,
            ExpressionAttributeNames = new Dictionary<string, string> { { "#category", "Category" } },
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> { { ":category", new AttributeValue { S = "Cloth" } } },
            FilterExpression = "#category = :category",
            Limit = 10
        };

        var response = new ScanResponse();

        do
        {
            records.Add(await _client.ScanAsync(request));
            request.ExclusiveStartKey = response.LastEvaluatedKey;
        } while (response.LastEvaluatedKey.Count > 0);

        return records;
    }
}
