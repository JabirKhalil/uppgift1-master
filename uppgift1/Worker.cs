using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using uppgift1.Models;

namespace uppgift1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        private readonly Random rnd = new Random();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var data = new TemperaturesModel
                    {
                        Temperature = rnd.Next(25, 40)
                    };

                    if (data.Temperature >= 30)

                        _logger.LogInformation($"The temperature is highest than 30°C the temperature now is: {data.Temperature}°C", DateTimeOffset.Now);


                    else
                        _logger.LogInformation($"The temperature now is: {data.Temperature}°C", DateTimeOffset.Now);


                }

                catch(Exception ex)
                {
                    _logger.LogInformation($"Failed - {ex.Message}");
                }



                await Task.Delay(10*1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has been started.");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has been stopped.");
            return base.StopAsync(cancellationToken);
        }
    }
}
