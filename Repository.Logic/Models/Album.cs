namespace Repository.Logic.Models
{
    public class Album : ModelObject
    {
        public string Title { get; set; } = string.Empty;
        public string Interpreter { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Title} {Interpreter}";
        }
    }
}
