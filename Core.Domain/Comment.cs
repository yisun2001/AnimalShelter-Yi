using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public Animal Animal { get; set; }
        public int AnimalId { get; set; }

        public Client CommentMadeBy { get; set; }
        public int ClientNumber{ get; set; }
        public string CommentText { get; set; }
        public DateTime DateTime { get; set; }

        

    }
}
