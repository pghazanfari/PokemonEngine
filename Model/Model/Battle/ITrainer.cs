namespace PokemonEngine.Model.Battle
{
    public interface ITrainer : Unique.ITrainer, IParticipant
    {
        new ITrainer Clone();
    }
}
