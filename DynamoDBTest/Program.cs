using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDB.Service;
using System.Text.Json;

IAmazonDynamoDB client = null!;
DynamoDbDDLService ddlService = null!;
DynamoDMLService dmlService = null!;
PartiQLService sqlService = null!;
string tableName = string.Empty;

int choice = 0;
do
{
    Console.WriteLine("Choose: ");
    Console.WriteLine("Create Table - Enter 1");
    Console.WriteLine("Delete Table - Enter 2");
    Console.WriteLine("Describe Table - Enter 3");
    Console.WriteLine("List all Tables - Enter 4");
    Console.WriteLine("Insert Item in Table - Enter 5");
    Console.WriteLine("Update Item in Table - Enter 6");
    Console.WriteLine("Get Item from Table - Enter 7");
    Console.WriteLine("Delete Item from Table - Enter 8");
    Console.WriteLine("Perform Batch Write in Table - Enter 9");
    Console.WriteLine("Perform Batch Get in Table - Enter 10");
    Console.WriteLine("Query in Table - Enter 11");
    Console.WriteLine("Scan in Table - Enter 12");
    Console.WriteLine("PartiQL Create - Enter 13");
    Console.WriteLine("PartiQL Read - Enter 14");
    Console.WriteLine("PartiQL Update - Enter 15");
    Console.WriteLine("PartiQL Delete - Enter 16");
    Console.WriteLine("PartiQL Get All Items - Enter 17");
    Console.WriteLine("Exit - Enter -1");
    choice = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine(choice);

    try
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                ddlService = new DynamoDbDDLService(client);
                var createResponse = ddlService.Create(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(createResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 2:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                ddlService = new DynamoDbDDLService(client);
                var deleteResponse = ddlService.Delete(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(deleteResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 3:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                ddlService = new DynamoDbDDLService(client);
                var describeResponse = ddlService.Describe(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(describeResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 4:
                client = ServiceProvider.GetDynamoDbClient();
                ddlService = new DynamoDbDDLService(client);
                var listResponse = ddlService.List().Result;
                Console.WriteLine(JsonSerializer.Serialize(listResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 5:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                Dictionary<string, AttributeValue> item = new();
                bool wantToAddMore = true;
                
                do
                {
                    Console.WriteLine("Enter Attribute Name");
                    var attributeName = Console.ReadLine()!;
                    Console.WriteLine("Enter Attribute Type (S for string,N for number)");
                    var attributeType = Console.ReadLine()!;
                    Console.WriteLine("Enter Attribute Value");
                    var attributeValue = Console.ReadLine()!;
                    item.Add(attributeName, new AttributeValue
                    {
                        S = attributeValue
                    });
                    Console.WriteLine("Want to add more Attribute?");
                    wantToAddMore = Console.ReadLine()!.ToLower() == "y";
                }
                while (wantToAddMore);

                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var putItemResponse = dmlService.PutItem(tableName,item).Result;
                Console.WriteLine(JsonSerializer.Serialize(putItemResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;

            case 6:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                Dictionary<string, AttributeValue> key = new();
                wantToAddMore = true;

                do
                {
                    Console.WriteLine("Enter Key Name, Type & Type (eg: Id,S,1");
                    var keyValue = Console.ReadLine()!;
                    var tokens = keyValue.Split(',');
                    key.Add(tokens[0], new AttributeValue
                    {
                        S = tokens[1] == "S" ? tokens[2] : null,
                        N = tokens[1] == "N" ? tokens[2] : null
                    });
                    Console.WriteLine("Want to add more Attribute?");
                    wantToAddMore = Console.ReadLine()!.ToLower() == "y";
                }
                while (wantToAddMore);

                Dictionary<string, AttributeValueUpdate> updateItem = new();
                do
                {
                    Console.WriteLine("Enter Update Attribute Name, Type & update value (eg: Id,S,1");
                    var keyValue = Console.ReadLine()!;
                    var tokens = keyValue.Split(',');
                    updateItem.Add(tokens[0], new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue
                        {
                            S = tokens[1] == "S" ? tokens[2] : null,
                            N = tokens[1] == "N" ? tokens[2] : null
                        }
                    });
                    Console.WriteLine("Want to add more Attribute?");
                    wantToAddMore = Console.ReadLine()!.ToLower() == "y";
                }
                while (wantToAddMore);
                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var updateResponse = dmlService.UpdateItem(tableName, key,updateItem).Result;
                Console.WriteLine(JsonSerializer.Serialize(updateResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 7:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                key = new();
                wantToAddMore = true;

                do
                {
                    Console.WriteLine("Enter Key Name, Type & Type (eg: Id,S,1");
                    var keyValue = Console.ReadLine()!;
                    var tokens = keyValue.Split(',');
                    key.Add(tokens[0], new AttributeValue
                    {
                        S = tokens[1] == "S" ? tokens[2] : null,
                        N = tokens[1] == "N" ? tokens[2] : null
                    });
                    Console.WriteLine("Want to add more Attribute?");
                    wantToAddMore = Console.ReadLine()!.ToLower() == "y";
                }
                while (wantToAddMore);

                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var getResponse = dmlService.GetItem(tableName, key).Result;
                Console.WriteLine(JsonSerializer.Serialize(getResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 8:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                key = new();
                wantToAddMore = true;

                do
                {
                    Console.WriteLine("Enter Key Name, Type & Type (eg: Id,S,1");
                    var keyValue = Console.ReadLine()!;
                    var tokens = keyValue.Split(',');
                    key.Add(tokens[0], new AttributeValue
                    {
                        S = tokens[1] == "S" ? tokens[2] : null,
                        N = tokens[1] == "N" ? tokens[2] : null
                    });
                    Console.WriteLine("Want to add more Attribute?");
                    wantToAddMore = Console.ReadLine()!.ToLower() == "y";
                }
                while (wantToAddMore);

                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var deleteItemResponse = dmlService.DeleteItem(tableName, key).Result;
                Console.WriteLine(JsonSerializer.Serialize(deleteItemResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 9:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                var items = new List<ProductModel> 
                {
                    new ProductModel {Id = "5",Category="Cloth",Name="Mackalay Jeans"},
                    new ProductModel {Id = "6",Category="Cloth",Name="Mark Shirt"},
                    new ProductModel {Id = "7",Category="Book",Name="Let us C++"}
                };
                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var batchWriteResponse = dmlService.BatchWriteItem(tableName, items).Result;
                Console.WriteLine(JsonSerializer.Serialize(batchWriteResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 10:
                var request = new BatchGetItemRequest
                {
                    RequestItems = new Dictionary<string, KeysAndAttributes>
                    {
                        { "Product", new KeysAndAttributes
                            {
                                ConsistentRead = true,
                                Keys = new List<Dictionary<string, AttributeValue>>
                                {
                                    new Dictionary<string, AttributeValue> {{ "Id", new AttributeValue { S = "5"} } }
                                }
                            } 
                        }
                    }
                };
                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var batchGetResponse = dmlService.BatchGetItems(request).Result;
                Console.WriteLine(JsonSerializer.Serialize(batchGetResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 11:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var queryResponse = dmlService.Query(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(queryResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 12:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                dmlService = new DynamoDMLService(client);
                var scanResponse = dmlService.Scan(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(scanResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 13:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                sqlService = new PartiQLService(client);
                var statementResponse = sqlService.Insert(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(statementResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 14:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                sqlService = new PartiQLService(client);
                statementResponse = sqlService.Read(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(statementResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 15:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                sqlService = new PartiQLService(client);
                statementResponse = sqlService.Update(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(statementResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 16:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                sqlService = new PartiQLService(client);
                statementResponse = sqlService.Delete(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(statementResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case 17:
                Console.WriteLine("Enter Table Name: ");
                tableName = Console.ReadLine()!;
                client = ServiceProvider.GetDynamoDbClient();
                sqlService = new PartiQLService(client);
                statementResponse = sqlService.GetAll(tableName).Result;
                Console.WriteLine(JsonSerializer.Serialize(statementResponse, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case -1:
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
while (choice != -1);

