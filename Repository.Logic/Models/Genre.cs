namespace Repository.Logic.Models
{
    public enum Genre : int
    {
        Classic = 1,
        Pop = 2,
        Rock = 2 * Pop,
        HardRock = 2 * Rock,
        Metal = 2 * HardRock,
        Blues = 2 * Metal,
    }
}
