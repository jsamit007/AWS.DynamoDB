using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace DynamoDB.Service;

public class ServiceProvider
{
    public static IAmazonDynamoDB GetDynamoDbClient(bool isLocal=true)
    {
        if(!isLocal)
            return new AmazonDynamoDBClient(RegionEndpoint.EUNorth1);

        string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY")!;
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECURITY_KEY")!;
        var region = RegionEndpoint.EUNorth1;

        return new AmazonDynamoDBClient(new BasicAWSCredentials(accessKey,secretKey),region);
    }
}
