using Repository.Logic.Models;

namespace Repository.Logic.Repos
{
    public class TrackRepository : Repository<Models.Track>
    {
        public TrackRepository()
            : base()
        {

        }
        public TrackRepository(string filePath)
            : base(filePath) 
        {

        }
        public override Track Create()
        {
            return new Track();
        }
    }
}
