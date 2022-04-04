using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AchiveClubServer.Services
{
    public class RatingTimerUpdater : IHostedService, IDisposable
    {
        private readonly ILogger<RatingTimerUpdater> _logger;
        private ClubRatingStorage _clubRatingStorage;
        private UserRatingStorage _userRatingStorage;
        private ClubRatingService _clubRatingService;
        private UserRatingService _userRatingService;
        private Timer _timer = null!;
        private double _delay = 480;

        public RatingTimerUpdater(
            ILogger<RatingTimerUpdater> logger,
            ClubRatingStorage clubRatingStorage,
            UserRatingStorage userRatingStorage,
            ClubRatingService clubRatingService,
            UserRatingService userRatingService)
        {
            _logger = logger;
            _clubRatingStorage = clubRatingStorage;
            _userRatingStorage = userRatingStorage;
            _clubRatingService = clubRatingService;
            _userRatingService = userRatingService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ValueStorageTimerUpdater running.");

            _timer = new Timer(_=>DoWork(), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(_delay));

            return Task.CompletedTask;
        }

        private void DoWork()
        {
            _logger.LogInformation("Work start!");

            _userRatingStorage.UserRating = _userRatingService.GetUserRating();
            _clubRatingStorage.ClubRating = _clubRatingService.GetClubRating();

            _logger.LogInformation("Work end!");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}