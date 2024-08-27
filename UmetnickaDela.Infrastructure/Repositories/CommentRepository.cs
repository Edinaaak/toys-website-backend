using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Comments.Request;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comments>, ICommentRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CommentRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Comments>> GetCommentsWithInclude(int productId)
        {
           var commentsByProduct = await  context.comments.Where(x => x.ProductId == productId).Include(x => x.User).Include(x => x.Product).ToListAsync();
            return commentsByProduct;
        }

        public async Task<bool> AddComment(AddCommentRequest comment)
        {
            
            var request = mapper.Map<Comments>(comment);
            context.comments.Add(request);
            var res = await context.SaveChangesAsync();
            if (res > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = await context.comments.FindAsync(id);
            context.comments.Remove(comment);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
    
}
