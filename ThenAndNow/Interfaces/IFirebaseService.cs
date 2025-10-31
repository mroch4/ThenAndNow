using ThenAndNow.Models.DTO;

namespace ThenAndNow.Interfaces
{
    public interface IFirebaseService
    {
        Task<Rating> AddReply(int id);
        public Task<Rating> GetRatingById(int id);
        public Task<Details> GetDetailsById(int id);
        public Task<(bool Success, Rating Rating)> ThumbsDown(int id);
        public Task<(bool Success, Rating Rating)> ThumbsUp(int id);
    }
}
