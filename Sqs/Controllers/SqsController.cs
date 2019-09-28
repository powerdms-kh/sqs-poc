using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Sqs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SqsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SqsController> _logger;
        private readonly SqsService _sqsService;

        public SqsController(ILogger<SqsController> logger, SqsService sqsService)
        {
            _logger = logger;
            _sqsService = sqsService;
        }

        public async Task SendForecastToQueue(string forecast)
        {
            var QueueUrl = await this._sqsService.GetQueueUrl("default", "OWNERID");
            await this._sqsService.SendMessage(QueueUrl.QueueUrl, forecast);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetAsync()
        {
            var rng = new Random();

            WeatherForecast[] forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            var json = JsonSerializer.Serialize<WeatherForecast[]>(forecast);

            SendForecastToQueue(json);

            return forecast;
        }
    }
}
