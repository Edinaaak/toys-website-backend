using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Comments.Request;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface ICommentRepository : IRepository<Comments>
    {

        Task<List<Comments>> GetCommentsWithInclude(int productId);
        Task<bool> AddComment(AddCommentRequest comment);
    }
}
