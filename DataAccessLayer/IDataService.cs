using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DataAccessLayer
{
    public interface IDataService
    {
        IList<Tag> GetTags();
        Tag GetTag(int id);
        IList<Comment> GetComments(ResourceParameters resourceParameters);
        Comment GetComment(int id);
        IList<Posttype> GetPosttypes();
        Posttype GetPosttype(int id);
        void CreatePosttype(Posttype posttype);
        void UpdatePosttype(Posttype posttype);
        void DeletePosttype(Posttype posttype);
        int GetCommentsCount();
    }
}
