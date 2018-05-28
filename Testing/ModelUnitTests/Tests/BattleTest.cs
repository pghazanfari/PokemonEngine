using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokemonEngine.Model;
using PokemonEngine.Model.Unique;
using PokemonEngine.Model.Battle;

using ModelUnitTests.Pokemons;
using ModelUnitTests.Moves;
using PokemonEngine.Model.Battle.Actions;

namespace ModelUnitTests.Tests
{
    [TestClass]
    public class BattleTest : IBattleInputProvider
    {
        private PokemonEngine.Model.Unique.IPokemon makeBulbasaur(int level)
        {
            MoveSet<PokemonEngine.Model.Unique.IMove> moves = new MoveSet<PokemonEngine.Model.Unique.IMove>(new PokemonEngine.Model.Unique.Move(ModelUnitTests.Move.Tackle));
            return new PokemonEngine.Model.Unique.Pokemon(Pokemon.Bulbasaur, Gender.Male, Nature.Adamant, new IVSet(1), new EVSet(1), moves, level);
        }

        private PokemonEngine.Model.Unique.ITrainer makeTrainer(String name, int level, int numBulbasaurs)
        {
            List<PokemonEngine.Model.Unique.IPokemon> list = new List<PokemonEngine.Model.Unique.IPokemon>(numBulbasaurs);
            for (int i = 0; i < numBulbasaurs; i++) { list.Add(makeBulbasaur(level)); }
            Party party = new Party(list);
            return new PokemonEngine.Model.Unique.Trainer(name, party);
        }

        private Team makeTeam(string name, int level, int numBulbasaurs)
        {
            List<PokemonEngine.Model.Battle.IParticipant> list = new List<IParticipant>(numBulbasaurs);
            for (int i = 0; i < numBulbasaurs; i++)
            {
                list.Add(new PokemonEngine.Model.Battle.Trainer(makeTrainer(name, level, numBulbasaurs), 1));
            }
            return new Team(list);
        }

        [TestMethod]
        public void TestBattle()
        {
            Team team1 = makeTeam("Trainer1", 10, 1);
            Team team2 = makeTeam("Trainer2", 10, 1);
            IBattle battle = new Battle(this, team1, team2);

            battle.OnUseMove += OnUseMove;

            int turnCounter = 0;
            while (!battle.IsComplete())
            {
                Trace.WriteLine($"Turn {++turnCounter} ===============================================");
                battle.ExecuteTurn();

                PokemonEngine.Model.Unique.IPokemon pokemon1 = team1[0].Pokemon;
                PokemonEngine.Model.Unique.IPokemon pokemon2 = team2[0].Pokemon;

                Trace.WriteLine($"Team1 has a Bulbasaur with {pokemon1.HP}/{pokemon1.Stats[PokemonEngine.Model.Statistic.HP]} HP");
                Trace.WriteLine($"Team2 has a Bulbasaur with {pokemon2.HP}/{pokemon2.Stats[PokemonEngine.Model.Statistic.HP]} HP");
            }


        }

        private void OnUseMove(object sender, UseMoveEventArgs e)
        {
            PokemonEngine.Model.Battle.ITrainer trainer = e.Action.Slot.Participant as PokemonEngine.Model.Battle.ITrainer;
            Trace.WriteLine($"{trainer.UID}'s {e.Action.Slot.Pokemon.Species} used {e.Action.Move.Name}");
        }

        public IList<IAction> ProvideActions(IBattle battle, IList<Request> requests)
        {
            List<IAction> actions = new List<IAction>(requests.Count);
            foreach (Request request in requests)
            {
                Team opposingTeam = battle.Teams.First(x => !x.Equals(request.Slot.Team));
               
                actions.Add(new UseMove(request.Slot, request.Slot.Pokemon.Moves[0], new List<Slot> { opposingTeam[0] } ));
            }
            return actions;
        }

        public IList<SwapPokemon> ProvideSwapPokemon(IBattle battle, IList<Request> requests)
        {
            return new List<SwapPokemon>();
        }
    }
}
