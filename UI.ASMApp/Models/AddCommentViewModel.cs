using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ASMApp.Models
{
    public class AddCommentViewModel
    {

        public int CommentId { get; set; }
        public Comment comment{ get; set; }

        public int AnimalId { get; set; }

        public int VolunteerId { get; set; }
    }
}
