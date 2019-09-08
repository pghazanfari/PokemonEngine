namespace PokemonEngine.Model
{
    public interface IStatistics
    {
        int this[Statistic stat] { get; }
    }
}
