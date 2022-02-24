using System.Collections.ObjectModel;

namespace MVCCrud_Many_To_Many_.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public Post()
        {
            PostCategories = new Collection<PostCategory>();
        }
    }
}
