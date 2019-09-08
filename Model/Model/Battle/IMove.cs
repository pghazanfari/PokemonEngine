namespace PokemonEngine.Model.Battle
{
    public interface IMove : Unique.IMove
    {
        bool IsDisabled { get; }

        new IMove Clone();
    }
}
