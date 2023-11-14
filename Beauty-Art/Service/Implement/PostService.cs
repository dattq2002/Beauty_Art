using AutoMapper;
using Beauty_Art.API.Services;
using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Interface;

namespace Beauty_Art.Service.Implement
{
    public class PostService : BaseService<PostService>, IPostService
    {
        public PostService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, ILogger<PostService> logger,
            IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Post> CreatePost(PostRequest req)
        {
            Post newPost = new Post()
            {
                Id = Guid.NewGuid(),
                Title = req.Title,
                Deflag = false,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                UserId = req.User_Id,
                DescriptionContent = req.Content
            };
            await _unitOfWork.GetRepository<Post>().InsertAsync(newPost);
            await _unitOfWork.CommitAsync();
            return newPost;
        }

        public async Task<bool> DeletePost(Guid id)
        {
            Post post =await _unitOfWork.GetRepository<Post>().SingleOrDefaultAsync(predicate: x => !x.Deflag && x.Id.Equals(id));
            if (post == null)
            {
                return false;
            }
            post.Deflag = true;
            _unitOfWork.GetRepository<Post>().UpdateAsync(post);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IPaginate<Post>> GetListPost(int page, int size)
        {
            IPaginate<Post> listPost = (IPaginate<Post>)await _unitOfWork.GetRepository<Post>()
                .GetPagingListAsync(
                               predicate: x => !x.Deflag,
                                              page: page, size: size);
            return listPost;
        }

        public async Task<Post> GetPostById(Guid id)
        {
            Post post = await _unitOfWork.GetRepository<Post>().SingleOrDefaultAsync(predicate: x => !x.Deflag && x.Id.Equals(id));
            if (post == null)
            {
                return null;
            }
            return post;
        }

        public async Task<Post> UpdatePost(PostRequest req, Guid id)
        {
            Post post = await _unitOfWork.GetRepository<Post>().SingleOrDefaultAsync(predicate: x => !x.Deflag && x.Id.Equals(id));
            if(post == null)
            {
                return null;
            }
            post.Title = req.Title;
            post.DescriptionContent = req.Content;
            post.UpsDate = DateTime.Now;
            _unitOfWork.GetRepository<Post>().UpdateAsync(post);
            await _unitOfWork.CommitAsync();
            return post;
        }
    }
}
