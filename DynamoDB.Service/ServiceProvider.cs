using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace DynamoDB.Service;

public class ServiceProvider
{
    public static IAmazonDynamoDB GetDynamoDbClient()
    {
        string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY")!;
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECURITY_KEY")!;
        var region = Amazon.RegionEndpoint.EUNorth1;

        return new AmazonDynamoDBClient(new BasicAWSCredentials(accessKey,secretKey),region);
    }
}
