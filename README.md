# Amazon SQS Local Dev POC

## Getting Started
- Install Docker and Docker Compose
- Clone this repository to `<dir>`
- `cd <dir>/Sqs && docker-compose up`
- Open browser to http://localhost:9325 to see queue contents
  - Sometimes, the initial queue takes a moment to be created
- Open browser to http://localhost/sqs to push messages to the queue


## What's included?
- Docker build for .NET Core 3.0 application
- Docker compose set-up for API (.NET Core) and SQS for local development
- SQS container based off of elasticmq, a SQS compliant API (In memory, so restarting server will clear out the queue)
- SQS Insight to view the contents of queues being created
- Single API endpoint that pushes a message to SQS queue 
- Leverages AWS IAmazonSQS SDK via dependency injection and a wrapper service injected as a singleton to abstract the AmazonSQS API