using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Interfaces;

namespace SocialApp.Application.UseCase.Post
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IFriendService _friendService;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, INotificationService notificationService, IFriendService friendService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _friendService = friendService;
        }

        public async Task DeletePostAsync(string postId, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null)
                throw new Exception("Post not found");

            if (post.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this post");

            // Thực hiện xóa mềm
            await _postRepository.DeleteAsync(postId, userId);
        }

        public async Task LikePostAsync(string postId, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null)
                throw new Exception("Post not found");

            // Nếu user đã like thì bỏ like (Unlike)
            if (post.LikeUserIds.Contains(userId))
            {
                post.LikeUserIds.Remove(userId);  // Bỏ like
               
            }
            else
            {
                post.LikeUserIds.Add(userId);    // Thêm like
            }

            await _postRepository.UpdateAsync(post);
            await _notificationService.CreateLikeRequestNotification(post.Id,post.UserId, userId); // Gửi thông báo khi like bài viết
        }

        public async Task<List<PostDTO>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync(); // Lấy tất cả bài viết chưa bị xóa

            // Chuyển đổi từ Post Entity sang PostDTO
            var postDtos = posts
                .Select(post => new PostDTO(post))
                .ToList();

            return postDtos;
        }

        public async Task SavePostAsync(string userId, string postId)
        {
            // Kiểm tra bài viết có tồn tại không
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            // Lưu bài viết vào danh sách yêu thích của người dùng
            await _userRepository.SavePostAsync(userId, postId);
        }
        public async Task<List<PostDTO>> GetSavedPostsAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(new List<string> { userId }); // Pass userId as a list
            if (user == null || user.Count == 0 || user[0].SavedPostIds.Count == 0)
            {
                return new List<PostDTO>(); // Return an empty list if no saved posts
            }

            // Get all saved posts
            var savedPosts = await _postRepository.GetPostsByIdsAsync(user[0].SavedPostIds);
            return savedPosts.Select(post => new PostDTO(post)).ToList();
        }

        public Task SavePost(string postId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreatePostAsync(CreatePostDTO dto)
        {
            var post = new Domain.Entities.Post
            {
                UserId = dto.UserId,  // Nếu lấy từ token, truyền từ controller vào  
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                LikeUserIds = new List<string>(),
                IsDeleted = false,
                DeletedAt = null
            };

            await _postRepository.CreateAsync(post);

            // Fix: Extract user IDs from the list of UserInfoDTO objects  
            var listFriendIds = (await _friendService.GetFriendsAsync(post.UserId))
                .Select(friend => friend.Id)
                .ToList();

            await _notificationService.CreatePostRequestNotification(post.Id,listFriendIds, post.UserId); // Gửi thông báo khi tạo bài viết  

            // Return the ID of the newly created post  
            return post.Id;
        }

        public async Task<List<Domain.Entities.Post>> GetPostsByUserIdAsync(string userId)
        {
            return await _postRepository.GetPostByUserIdAsync(userId);
        }
    }
}
