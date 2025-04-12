# Serverless Image Upload and Processing Service

This project demonstrates how to build a serverless image upload and processing service using AWS Lambda, S3, and DynamoDB. The application allows users to upload images to an S3 bucket, and once the image is uploaded, AWS Lambda processes the image (for example, logging its size), and stores metadata in a DynamoDB table.

## Features
- **Image Upload**: Users can upload images to an S3 bucket.
- **Lambda Processing**: Upon image upload, AWS Lambda is triggered to process the image.
- **Metadata Storage**: Image metadata (such as file name, size, and upload time) is stored in DynamoDB.
- **Serverless Architecture**: The entire service uses AWS serverless technologies like Lambda, S3, and DynamoDB.

## Prerequisites
Before you start, you need the following:
- **AWS Account**: If you don't have one, create it at [AWS](https://aws.amazon.com/).
- **AWS CLI**: Make sure the AWS CLI is installed and configured with the necessary credentials. You can install it from [here](https://aws.amazon.com/cli/).

## Step-by-Step Guide

### Step 1: Set Up S3 Bucket
1. **Create an S3 bucket**:
   - Go to the AWS S3 Console: [S3 Console](https://s3.console.aws.amazon.com/s3).
   - Click **Create Bucket**.
   - Choose a globally unique name and a region.
   - Leave the default settings, and click **Create Bucket**.

### Step 2: Set Up DynamoDB
1. **Create a DynamoDB Table**:
   - Go to the AWS DynamoDB Console: [DynamoDB Console](https://console.aws.amazon.com/dynamodb).
   - Click **Create Table**.
   - Set the **Table name** as `ImageMetadata`.
   - Set the **Partition Key** as `FileName` (String).
   - You can leave other settings as default, then click **Create Table**.

### Step 3: Create the Lambda Function
1. **Create a Lambda Function**:
   - Go to the AWS Lambda Console: [Lambda Console](https://console.aws.amazon.com/lambda).
   - Click **Create function**.
   - Choose **Author from scratch**.
   - Name the function (e.g., `ImageProcessingFunction`).
   - Set the runtime to **C# (.NET Core)**.
   - Choose or create a new IAM role that has **permissions to read from S3** and **write to DynamoDB**.
   - Click **Create function**.

2. **Upload the Lambda Code**:
   - Paste the code from `Function.cs` into the inline editor in Lambda or upload it as a `.zip` file if needed.
   - Make sure your function has the following AWS permissions:
     - **S3 Read**: To access images from the S3 bucket.
     - **DynamoDB Write**: To write metadata into DynamoDB.

### Step 4: Set Up the S3 Event Notification
1. **Configure S3 Bucket to Trigger Lambda**:
   - Go to the **S3 Console**.
   - Select your bucket and navigate to the **Properties** tab.
   - Scroll down to **Event notifications**, and click **Create event notification**.
   - Set the event type to **All object create events**.
   - Under **Send to**, choose **Lambda function** and select the Lambda function you just created.
   - Save the event notification.

### Step 5: Deploy and Test
1. **Upload an Image to S3**:
   - Go to the **S3 Console**.
   - Select your bucket and click **Upload**.
   - Choose an image file to upload.

2. **Check the Lambda Logs**:
   - Go to the **CloudWatch Logs** in the AWS Console: [CloudWatch Console](https://console.aws.amazon.com/cloudwatch).
   - You should see logs indicating that the image was processed, including the file size.

3. **View Metadata in DynamoDB**:
   - Go to the **DynamoDB Console**.
   - Open the `ImageMetadata` table.
   - You should see metadata for the image you uploaded (file name, file size, upload time).

### Step 6: Clean Up
After testing, be sure to clean up the resources to avoid unnecessary charges:
- Delete the S3 bucket.
- Delete the DynamoDB table.
- Delete the Lambda function.

## Additional Information
- **AWS Lambda**: Lambda runs your code without provisioning or managing servers.
- **Amazon S3**: Object storage service that provides scalable storage for images.
- **Amazon DynamoDB**: A fully managed NoSQL database service that stores image metadata.

## How to Contribute
Feel free to fork this repository, contribute enhancements, or fix issues. You can also open issues if you have any questions or improvements.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments
- [AWS Documentation](https://aws.amazon.com/documentation/)
- [Amazon S3](https://aws.amazon.com/s3/)
- [Amazon DynamoDB](https://aws.amazon.com/dynamodb/)
