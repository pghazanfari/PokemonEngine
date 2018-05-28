﻿using PokemonEngine.Model.Unique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonEngine.Model.Battle
{
    public interface IParticipant
    {
        IReadOnlyList<IPokemon> Battlers { get; }
        Party Party { get; }
    }

    public static class IBattleParticipantImpl
    {
        public static bool HasLost(this IParticipant participant)
        {
            return participant.Party.All(x => x.HasFainted());
        }
    }
}