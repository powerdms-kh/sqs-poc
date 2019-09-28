using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;

namespace Sqs
{
    public class SqsService
    {
        private readonly IConfiguration _config;
        private readonly IAmazonSQS _client;
        public SqsService(IConfiguration config)
        {
            _config = config;
            _client = _config.GetAWSOptions().CreateServiceClient<IAmazonSQS>();
        }

        public Task<CreateQueueResponse> CreateQueue(string QueueName, int Timeout)
        {
            var createQueueRequest = new CreateQueueRequest();

            createQueueRequest.QueueName = QueueName;
            var attrs = new Dictionary<string, string>();
            attrs.Add(QueueAttributeName.VisibilityTimeout, Timeout.ToString());
            createQueueRequest.Attributes = attrs;

            return _client.CreateQueueAsync(createQueueRequest);
        }

        public Task<GetQueueUrlResponse> GetQueueUrl(string QueueName, string OwnerId)
        {
            var request = new GetQueueUrlRequest
            {
                QueueName = QueueName,
                QueueOwnerAWSAccountId = OwnerId
            };

            return _client.GetQueueUrlAsync(request);
        }

        public Task<SendMessageResponse> SendMessage(string QueueUrl, string Message)
        {
            var sendMessageRequest = new SendMessageRequest(QueueUrl, Message);

            return _client.SendMessageAsync(sendMessageRequest);
        }
    }
}