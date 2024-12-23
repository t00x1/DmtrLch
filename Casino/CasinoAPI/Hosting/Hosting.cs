using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Service;

namespace CasinoApi.Hosting
{
    public class Hosting : BackgroundService
    {
        private readonly GameWheel _gameWheel;
        private readonly ILogger<Hosting> _logger;

        public Hosting(GameWheel gameWheel, ILogger<Hosting> logger)
        {
            _gameWheel = gameWheel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("GameWheelBackgroundService запущен.");

            try
            {
                // Запуск игрового цикла
                await _gameWheel.CreateGame(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Обработка отмены задачи
                _logger.LogInformation("GameWheelBackgroundService остановлен.");
            }
            finally
            {
                _logger.LogInformation("GameWheelBackgroundService завершен.");
            }
        }
    }
}