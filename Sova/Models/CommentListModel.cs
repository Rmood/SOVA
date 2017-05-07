using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sova.Models
{
    public class CommentListModel
    {
        public int Id { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
    }
}
