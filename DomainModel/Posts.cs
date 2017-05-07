using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Posts
    {
        public int Id { get; set; }
        public int Post_Type_Id { get; set; }
        public int Parent_Id { get; set; }
        public int Accepted_Answer_Id { get; set; }
        public DateTime Creation_Date { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime Closed_Date { get; set; }
        public string Title { get; set; }
        public int User_Id { get; set; }
    }
}
