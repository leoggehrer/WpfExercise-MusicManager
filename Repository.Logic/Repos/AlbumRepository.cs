using Repository.Logic.Models;

namespace Repository.Logic.Repos
{
    public class AlbumRepository : Repository<Models.Album>
    {
        public AlbumRepository()
            : base()
        {

        }
        public override Album Create()
        {
            return new Album();
        }
    }
}
