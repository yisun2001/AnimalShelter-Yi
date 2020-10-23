using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Infrastructure
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AnimalShelterDbContext _context;

        public CommentRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }
        public Comment CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
            
        }
    }
}
