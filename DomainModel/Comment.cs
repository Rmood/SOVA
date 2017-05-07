using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Comment
    {
        public int Id { get; set; }
        public int postId { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public int userId { get; set; }
    }
}
