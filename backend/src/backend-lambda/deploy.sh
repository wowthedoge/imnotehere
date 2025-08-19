#!/bin/bash

echo "Building the Lambda function..."
dotnet build -c Release

echo "Deploying to AWS Lambda..."
dotnet lambda deploy-serverless --region ap-southeast-1 --stack-name imnotehere-lambda --s3-bucket imnotehere-lambda-hong-1755516340

echo "Deployment complete!"
