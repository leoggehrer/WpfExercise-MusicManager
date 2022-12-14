namespace Repository.Logic.Models
{
    public class Track : ModelObject
    {
        public int AlbumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Composer { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} {Composer}";
        }
    }
}
