﻿using System;
using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.System.Repositories;
using InsanelySimpleBlog.System.Repositories.Implementation;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services.Implementation
{
    internal class PostsService : IPostsService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper<Post, PostViewModel> _postMapper;

        public PostsService() : this(new EntityFrameworkUnitOfWorkFactory(), new PostToPostViewModelMapper())
        {
            
        }

        public PostsService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IMapper<Post, PostViewModel> postMapper)
        {
            Condition.Requires(unitOfWorkFactory, "unitOfWorkFactory").IsNotNull();
            Condition.Requires(postMapper, "postMapper").IsNotNull();

            _unitOfWorkFactory = unitOfWorkFactory;
            _postMapper = postMapper;
        }

        public PostViewModel Get(int postId)
        {
            Condition.Requires(postId, "postId").IsGreaterOrEqual(0);
            Post post = null;
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Execute(() =>
                {
                    IRepository<Post> repository = unitOfWork.GetRepository<Post>();
                    post= repository
                        .AllIncluding(x => x.Categories, x => x.Author)
                        .Single(x => x.PostID == postId);
                });
            }

            return _postMapper.Map(post);
        }

        public IEnumerable<PostViewModel> RecentPosts(int pageNumber, int pageSize, int? categoryId, DateTime? startDate, DateTime? endDate)
        {
            Condition.Requires(pageNumber, "pageNumber").IsGreaterOrEqual(0);
            Condition.Requires(pageSize, "pageSize").IsLessOrEqual(ServiceConstants.MaximumPageSize+1);
            Condition.Requires(pageSize, "pageSize").IsGreaterThan(0);

            Post[] posts = null;

            using(IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {

                unitOfWork.Execute(() =>
                                                {
                                                    IRepository<Post> repository = unitOfWork.GetRepository<Post>();
                                                    IQueryable<Post> query = repository.AllIncluding(x => x.Categories, x => x.Author);
                                                    if (categoryId.HasValue)
                                                    {
                                                        query = query.Where(x => x.Categories.Any(c => c.CategoryID == categoryId.Value));
                                                    }
                                                    if (startDate.HasValue)
                                                    {
                                                        query = query.Where(x => x.PostedAt >= startDate.Value);
                                                    }
                                                    if (endDate.HasValue)
                                                    {
                                                        query = query.Where(x => x.PostedAt <= endDate.Value);
                                                    }
                                                    posts = query
                                                        .OrderByDescending(x => x.PostedAt)
                                                        .Skip(pageNumber*pageSize)
                                                        .Take(pageSize+1)
                                                        .ToArray();
                                                });
            }

            return posts.Select(x => _postMapper.Map(x)).ToArray();
        }
    }
}
