using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.IO;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

public class Function
{
    private static readonly AmazonS3Client s3Client = new AmazonS3Client();
    private static readonly AmazonDynamoDBClient dynamoDbClient = new AmazonDynamoDBClient();

    // The DynamoDB table name where image metadata will be stored
    private const string TableName = "ImageMetadata";

    public async Task FunctionHandler(S3EventNotification.S3EventNotificationRecord record, ILambdaContext context)
    {
        string bucketName = record.S3.Bucket.Name;
        string objectKey = record.S3.Object.Key;

        try
        {
            // Download the image from S3
            var getObjectResponse = await s3Client.GetObjectAsync(bucketName, objectKey);
            using var stream = getObjectResponse.ResponseStream;

            // Process the image (for simplicity, we just log the file size here)
            var fileSize = stream.Length;
            Console.WriteLine($"Processed image: {objectKey}, Size: {fileSize} bytes");

            // Store metadata in DynamoDB
            var item = new Document();
            item["FileName"] = objectKey;
            item["FileSize"] = fileSize;
            item["UploadTime"] = DateTime.UtcNow.ToString();

            var table = Table.LoadTable(dynamoDbClient, TableName);
            await table.PutItemAsync(item);

            Console.WriteLine("Image metadata stored in DynamoDB.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing image: {ex.Message}");
        }
    }
}
