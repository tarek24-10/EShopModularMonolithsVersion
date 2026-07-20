using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handling Request = {@RequestType} - Responce = {@Response} - RequestData = {@RequestData} ", typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;

            if (timeTaken.TotalSeconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The Request = {@RequestType} took {TimeTaken} seconds to handle", typeof(TRequest).Name, timeTaken.TotalSeconds);
            }

            logger.LogInformation("[END] Handling {@RequestType} with {@Response} - Time Taken = {TimeTaken}", typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.TotalSeconds);

            return response;
        }
    }
}
