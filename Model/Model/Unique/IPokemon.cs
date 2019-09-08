using System;

namespace PokemonEngine.Model.Unique
{
    public interface IPokemon : Model.IPokemon, ICloneable
    {
        string UID { get; }

        new IStatistics Stats { get; }
        Gender Gender { get; }
        Nature Nature { get; }
        Ability Ability { get; }
        IVSet IVs { get; }
        EVSet EVs { get; }
        int Level { get; }
        int Experience { get; }
        int HP { get; }
        MoveSet<IMove> Moves { get; }

        event EventHandler<GainExperienceEventArgs> OnGainExperience;
        event EventHandler<ExperienceGainedEventArgs> OnExperienceGained;
        int GainExperience(int amount);

        event EventHandler<LevelUpEventArgs> OnLevelUp;
        event EventHandler<LevelledUpEventArgs> OnLevelledUp;
        int LevelUp();

        event EventHandler<UpdateFriendshipEventArgs> OnUpdateFriendship;
        event EventHandler<FriendshipUpdatedEventArgs> OnFriendshipUpdated;
        int UpdateFriendship(int delta);

        event EventHandler<UpdateHPEventArgs> OnUpdateHP;
        event EventHandler<HPUpdatedEventArgs> OnHPUpdated;
        int UpdateHP(int delta);

        new IPokemon Clone();
    }
    
    public static class IPokemonImpl
    {
        public static int BaseHP(Unique.IPokemon pokemon)
        {
            return (pokemon as Model.IPokemon).Stats[Model.Statistic.HP];
        }

        public static int MaxHP(this Unique.IPokemon pokemon)
        {
            return pokemon.Stats[Statistic.HP];
        }

        public static int BaseAttack(this Unique.IPokemon pokemon)
        {
            return (pokemon as Model.IPokemon).Stats[Model.Statistic.Attack];
        }

        public static int BaseSpecialAttack(this Unique.IPokemon pokemon)
        {
            return (pokemon as Model.IPokemon).Stats[Model.Statistic.SpecialAttack];
        }

        public static int BaseDefense(this Model.Unique.IPokemon pokemon)
        {
            return (pokemon as Model.IPokemon).Stats[Model.Statistic.Defense];
        }

        public static int BaseSpecialDefense(this Unique.IPokemon pokemon)
        {
            return (pokemon as Model.IPokemon).Stats[Model.Statistic.SpecialDefense];
        }

        public static int BaseSpeed(this Unique.IPokemon pokemon)
        {
            return (pokemon as Model.IPokemon).Stats[Model.Statistic.Speed];
        }

        public static bool HasFainted(this Unique.IPokemon pokemon)
        {
            return pokemon.HP == 0;
        }
    }
}
