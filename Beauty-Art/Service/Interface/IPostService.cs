using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;

namespace Beauty_Art.Service.Interface
{
    public interface IPostService
    {
        Task<IPaginate<Post>> GetListPost(int page, int size);
        Task<Post> CreatePost(PostRequest req);
        Task<bool> DeletePost(Guid id);
        Task<Post> UpdatePost(PostRequest req, Guid id);
        Task<Post> GetPostById(Guid id);
    }
}
