using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DomainModel;
using SovaDatabase;


namespace SovaDatabase
{
    public class SovaDataService : IDataService
    {
        public IList<Tag> GetTags()
        {
            using (var context = new SovaContext())
            {
                return context.Tags.ToList();
            }
        }

        public Tag GetTag(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Tags.Find(id);
            }
        }

        public IList<Comment> GetComments()
        {
            using (var context = new SovaContext())
            {
                return context.Comments.ToList();
            }
        }

        public Comment GetComment(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Comments.Find(id);
            }
        }

        public IList<Posttype> GetPosttypes()
        {
            using (var context = new SovaContext())
            {
                return context.Posttypes.ToList();
            }
        }

        public Posttype GetPosttype(int id)
        {
            using (var context = new SovaContext())
            {
                return context.Posttypes.Find(id);
            }
          
        }

        public void CreatePosttype(Posttype posttype)
        {
            using (var context = new SovaContext())
            {
                //var nextId = context.Posttypes.Max(x => x.Id) + 1;
                //posttype.Id = nextId;
                context.Posttypes.Add(posttype);
                context.SaveChanges();
            }
        }

        public void UpdatePosttype(Posttype posttype)
        {
            using (var context = new SovaContext())
            {
                context.Posttypes.Update(posttype);
                context.SaveChanges();
            }
        }

        public void DeletePosttype(Posttype posttype)
        {
            using (var context = new SovaContext())
            {
                context.Posttypes.Remove(posttype);
                context.SaveChanges();
            }
        }
    }
}
