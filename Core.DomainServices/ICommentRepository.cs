using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAllComments();

        Comment GetComment(int Id);

/*        Comment DeleteComment(int Id);*/

        Comment CreateComment(Comment comment, int Id);
        Comment CreateComment(Comment comment);

        Comment UpdateComment(Comment comment, int Id);
        Comment UpdateComment(Comment comment);
    }
}
