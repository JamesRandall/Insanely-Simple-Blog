using System;
using System.Collections.Generic;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services
{
    public interface IPostsService
    {
        PostViewModel Get(int postId);
        IEnumerable<PostViewModel> RecentPosts(int pageNumber, int pageSize, int? categoryId, DateTime? startDate, DateTime? endDate);
    }
}
