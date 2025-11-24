using ThenAndNow.Interfaces;
using ThenAndNow.Models.DTO;

namespace ThenAndNow.Services
{
    public class RatingService(IFirebaseService firebaseService, IUserService userService) : IRatingService
    {
        public async Task<Rating> GetRatingById(int id)
        {
            return await firebaseService.GetRatingById(id);
        }

        public async Task<bool> GetVotingEnabled(int id)
        {
            return await userService.GetVotingEnabled(id);
        }

        public async Task<Rating> ThumbsDown(int id)
        {
            var result = await firebaseService.ThumbsDown(id);

            if (result.Success)
            {
                await userService.SetVotes(id);
            }

            return result.Rating;
        }

        public async Task<Rating> ThumbsUp(int id)
        {
            var result = await firebaseService.ThumbsUp(id);

            if (result.Success)
            {
                await userService.SetVotes(id);
            }

            return result.Rating;
        }
    }
}
