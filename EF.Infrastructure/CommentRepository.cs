using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
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


    /*    nog bewerken zodat het aan een animal gevoegd wordt*/
        public Comment CreateComment(Comment comment, int Id)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        /*        public Comment DeleteComment(int Id)
                {
                    Comment comment = GetComment(Id);
                    _context.Remove(GetComment(Id));
                    _context.SaveChanges();

                    return comment;
                }*/

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments;
        }

        public Comment GetComment(int Id)
        {
            return _context.Comments.Find(Id);
        }

        public Comment UpdateComment(Comment comment)
        {
            var comm = _context.Comments.Attach(comment);
            comm.State = EntityState.Modified;
            _context.SaveChanges();
            return comment;
        }

        /* nog bewerken zodat het bij een animal geupdate wordt*/
        public Comment UpdateComment(Comment comment, int Id)
        {
            var comm = _context.Comments.Attach(comment);
            comm.State = EntityState.Modified;
            _context.SaveChanges();
            return comment;
        }

    }
}
