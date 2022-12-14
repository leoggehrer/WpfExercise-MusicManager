using Repository.Logic.Models;

namespace Repository.Logic.Repos
{
    public class AlbumRepository : Repository<Models.Album>
    {
        public AlbumRepository()
            : base()
        {

        }
        public AlbumRepository(string filePath)
            : base(filePath) 
        {

        }
        public override Album Create()
        {
            return new Album();
        }
    }
}
