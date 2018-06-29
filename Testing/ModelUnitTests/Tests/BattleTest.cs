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
using PokemonEngine.Model.Battle.Messages;

namespace ModelUnitTests.Tests
{
    [TestClass]
    public class BattleTest : IBattleInputProvider
    {

        [TestMethod]
        public void TestBattle()
        {
            Team team1 = Helper.ConstructTeam("Trainer1", 10, 1);
            Team team2 = Helper.ConstructTeam("Trainer2", 10, 1);
            Random random = new Random(1234567);
            IBattle battle = new Battle(random, this, PokemonEngine.Model.Weather.ClearSkies, new Team[] { team1, team2 });

            battle.OnUseMove += OnUseMove;
            battle.OnDamageInflicted += OnDamageInflicted;
            battle.OnStatStageShifted += OnStatStageShifted;

            int maxTurns = 20;
            int turnCounter = 0;
            while (!battle.IsComplete() && turnCounter < maxTurns)
            {
                Trace.WriteLine($"Turn {++turnCounter} ===============================================");

                battle.ExecuteTurn();

                PokemonEngine.Model.Unique.IPokemon pokemon1 = team1[0].Pokemon;
                PokemonEngine.Model.Unique.IPokemon pokemon2 = team2[0].Pokemon;

                Trace.WriteLine($"Team1 has a Bulbasaur with {pokemon1.HP}/{pokemon1.Stats[PokemonEngine.Model.Statistic.HP]} HP");
                Trace.WriteLine($"Team2 has a Bulbasaur with {pokemon2.HP}/{pokemon2.Stats[PokemonEngine.Model.Statistic.HP]} HP");
            }

            Assert.IsFalse(turnCounter >= maxTurns);
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

        private void OnDamageInflicted(object sender, DamageInflictedEventArgs e)
        {
            foreach (Slot target in e.Action.Targets)
            {
                PokemonEngine.Model.Battle.ITrainer trainer = target.Participant as PokemonEngine.Model.Battle.ITrainer;
                if (e.Action is InflictMoveDamage && (e.Action as InflictMoveDamage).IsCriticalHit)
                {
                    Trace.WriteLine("A critical hit!");
                }

                Trace.WriteLine($"{trainer.UID}'s {target.Pokemon.Species} took {e.Action[target]} damage.");
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
