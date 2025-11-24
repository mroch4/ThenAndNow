using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IRatingService
    {
        public Task<Rating> GetRatingById(int entryId);
        public Task<bool> GetVotingEnabled(int id);
        public Task<Rating> ThumbsDown(int id);
        public Task<Rating> ThumbsUp(int id);
    }
}
