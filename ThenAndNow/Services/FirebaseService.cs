using Microsoft.JSInterop;
using ThenAndNow.Constants;
using ThenAndNow.Interfaces;
using ThenAndNow.Models;
using ThenAndNow.Models.Configuration;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class FirebaseService(
        AppConfiguration appConfiguration,
        IJSRuntime jsRuntime) : IFirebaseService, IAsyncDisposable
    {
        #region Auth

        public async Task<User> GetCurrentUser()
        {
            try
            {
                return await jsRuntime.InvokeAsync<User>(JsInteropKeys.GetCurrentUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(GetCurrentUser)} error: {ex.Message}");
                return null;
            }
        }

        public async Task<User> SignInWithGoogle()
        {
            return await SignIn(JsInteropKeys.SignInWithGoogle);
        }

        public async Task SignOut()
        {
            try
            {
                await jsRuntime.InvokeAsync<User>(JsInteropKeys.SignOut);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(UpdateRating)} error: {ex.Message}");
            }
        }

        private async Task<User> SignIn(string functionKey)
        {
            try
            {
                return await jsRuntime.InvokeAsync<User>(functionKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(SignIn)} error: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Rating

        public async Task<Rating> GetRatingById(int id)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.RatingDb, id);
                var result = await jsRuntime.InvokeAsync<Rating>(JsInteropKeys.GetRatingById, refPath);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(GetRatingById)} error: {ex.Message}");
                return new Rating { Score = 0, Total = 0 };
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

        private async Task<bool> UpdateRating(Rating rating)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.RatingDb, rating.Id);
                return await jsRuntime.InvokeAsync<bool>(JsInteropKeys.UpdateRating, refPath, rating);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(UpdateRating)} error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Replies

        public async Task<(bool, Reply)> AddReply(Reply reply)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.ReplyDb, reply.EntryId);
                var result = await jsRuntime.InvokeAsync<bool>(JsInteropKeys.AddReply, refPath, reply);
                return (result, reply);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(AddReply)} error: {ex.Message}");
                return (false, reply);
            }
        }

        public async Task<Reply[]> GetRepliesById(int id)
        {
            try
            {
                var refPath = GetRefPath(appConfiguration.ReplyDb, id);
                var result = await jsRuntime.InvokeAsync<Reply[]>(JsInteropKeys.GetRepliesById, refPath);
                return result.OrderBy(x => x.Timestamp).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FirebaseService)}.{nameof(GetRepliesById)} error: {ex.Message}");
                return [];
            }
        }

        #endregion

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
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
    }
}