using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokemonEngine.Model;
using PokemonEngine.Model.Unique;
using PokemonEngine.Model.Battle;

using ModelUnitTests.PokemonImpl;
using ModelUnitTests.MoveImpl;
using PokemonEngine.Model.Battle.Actions;

namespace ModelUnitTests.Tests
{
    [TestClass]
    public class BattleTest : IBattleInputProvider
    {

        private PokemonEngine.Model.Unique.ITrainer makeTrainer(String name, int level, int numBulbasaurs)
        {
            List<PokemonEngine.Model.Unique.IPokemon> list = new List<PokemonEngine.Model.Unique.IPokemon>(numBulbasaurs);
            for (int i = 0; i < numBulbasaurs; i++) { list.Add(Bulbasaur.ConstructSimple(level)); }
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
            Random random = new Random(1234567);
            IBattle battle = new Battle(random, this, new Team[] { team1, team2 });

            battle.OnUseMove += OnUseMove;
            battle.OnMoveDamageInflicted += OnMoveDamageInflicted;
            battle.OnStatStageShifted += OnStatStageShifted;

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

        private void OnStatStageShifted(object sender, StatStageShiftedEventArgs e)
        {
            if (e.Action.Delta == 0) return;

            int currentStage = e.Action.Pokemon.Stats.Stage(e.Action.Stat);
            if (currentStage == 6 && e.Action.Delta > 0)
            {
                Trace.WriteLine($"{e.Action.Pokemon.Species}'s {e.Action.Stat.ToString()} won't go any higher!");
            }
            if (currentStage == -6 && e.Action.Delta < 0)
            {
                Trace.WriteLine($"{e.Action.Pokemon.Species}'s {e.Action.Stat.ToString()} won't go any lower!");
            }

            string word1 = "";
            if (e.Action.Delta == 2) word1 = "greatly ";
            if (e.Action.Delta >= 3) word1 = "sharply ";
            if (e.Action.Delta == -2) word1 = "harshly ";
            if (e.Action.Delta <= -3) word1 = "severely ";

            string word2 = e.Action.Delta > 0 ? "rose" : "fell";

            Trace.WriteLine($"{e.Action.Pokemon.Species}'s {e.Action.Stat.ToString()} {word1}{word2}!");
        }

        private void OnMoveDamageInflicted(object sender, MoveDamageInflictedEventArgs e)
        {
            foreach (Slot slot in e.Action.Targets)
            {
                PokemonEngine.Model.Battle.ITrainer trainer = slot.Participant as PokemonEngine.Model.Battle.ITrainer;
                Trace.WriteLine($"{trainer.UID}'s {slot.Pokemon.Species} took {e.Action.Damage(slot)} damage.");
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

                List<PokemonEngine.Model.Battle.IMove> validMoves = new List<PokemonEngine.Model.Battle.IMove>(4);
                foreach (PokemonEngine.Model.Battle.IMove move in request.Slot.Pokemon.Moves)
                {
                    if (move != null) validMoves.Add(move);
                }

                actions.Add(new UseMove(request.Slot, validMoves[battle.RNG.Next(validMoves.Count)], new List<Slot> { opposingTeam[0] }));
            }
            return actions;
        }

        public IList<SwapPokemon> ProvideSwapPokemon(IBattle battle, IList<Request> requests)
        {
            return new List<SwapPokemon>();
        }
    }
}
