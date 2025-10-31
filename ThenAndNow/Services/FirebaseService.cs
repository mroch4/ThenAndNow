using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models.Configuration;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class FirebaseService(AppConfiguration appConfiguration, IJSRuntime jsRuntime) : IFirebaseService, IAsyncDisposable
    {
        #region Public methods

        public async Task<Rating> AddReply(int id)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.ReplyDb, id);
                var result = await jsRuntime.InvokeAsync<Rating>(JsInteropKeys.GetVotesById, refPath);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FirebaseService.AddReply error: {ex.Message}");
                return new Rating { Score = 0, Total = 0 };
            }
        }

        public async Task<Rating> GetRatingById(int id)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.RatingDb, id);
                var result = await jsRuntime.InvokeAsync<Rating>(JsInteropKeys.GetVotesById, refPath);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FirebaseService.GetRatingById error: {ex.Message}");
                return new Rating { Score = 0, Total = 0 };
            }
        }

        public async Task<Details> GetDetailsById(int id)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.DetailsDb, id);
                var result = await jsRuntime.InvokeAsync<Details>(JsInteropKeys.GetDetailsById, refPath);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FirebaseService.GetDetailsById error: {ex.Message}");
                return new Details();
            }
        }

        public async Task<(bool, Rating)> ThumbsDown(int id)
        {
            var original = await GetRatingById(id);

            var update = new Rating
            {
                Id = id,
                Score = original.Score - 1,
                Total = original.Total + 1
            };

            return await UpdateRating(update) ? (true, update) : (false, original);
        }

        public async Task<(bool, Rating)> ThumbsUp(int id)
        {
            var original = await GetRatingById(id);

            var update = new Rating
            {
                Id = id,
                Score = original.Score + 1,
                Total = original.Total + 1
            };

            return await UpdateRating(update) ? (true, update) : (false, original);
        }

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        #endregion

        #region Private methods

        private async Task<bool> UpdateRating(Rating rating)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.RatingDb, rating.Id);
                return await jsRuntime.InvokeAsync<bool>(JsInteropKeys.UpdateRating, refPath, rating);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FirebaseService.UpdateRating error: {ex.Message}");
                return false;
            }
        }

        private static string GetRefPath(FirebaseConfiguration config, int id)
        {
            var url = config.BasePath;

            if (config.Version.HasValue)
            {
                url += $"{config.Version.Value}";
            }

            url += $"/{id}";

            return url;
        }

        #endregion
    }
}