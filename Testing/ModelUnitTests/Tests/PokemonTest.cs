using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ModelUnitTests.Util;

using PokemonEngine.Model.Battle.Messages;
using PokemonEngine.Model.Battle;

namespace ModelUnitTests.Tests
{
    [TestClass]
    public class PokemonTest
    {
        // https://pokemonshowdown.com/damagecalc/

        [TestMethod]
        public void TestStats()
        {
            PokemonEngine.Model.IPokemon basePokemon = Pokemon.Bulbasaur;
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseAttack, basePokemon.Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseDefense, basePokemon.Stats[PokemonEngine.Model.Statistic.Defense]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpecialAttack, basePokemon.Stats[PokemonEngine.Model.Statistic.SpecialAttack]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpecialDefense, basePokemon.Stats[PokemonEngine.Model.Statistic.SpecialDefense]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpeed, basePokemon.Stats[PokemonEngine.Model.Statistic.Speed]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseHP, basePokemon.Stats[PokemonEngine.Model.Statistic.HP]);

            PokemonEngine.Model.Unique.IPokemon uniquePokemon = PokemonImpl.Bulbasaur.ConstructSimple(10);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseAttack, (uniquePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseDefense, (uniquePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.Defense]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpecialAttack, (uniquePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.SpecialAttack]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpecialDefense, (uniquePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.SpecialDefense]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpeed, (uniquePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.Speed]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseHP, (uniquePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.HP]);

            Assert.AreEqual(15, uniquePokemon.Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(14, uniquePokemon.Stats[PokemonEngine.Model.Statistic.Defense]);
            Assert.AreEqual(16, uniquePokemon.Stats[PokemonEngine.Model.Statistic.SpecialAttack]);
            Assert.AreEqual(18, uniquePokemon.Stats[PokemonEngine.Model.Statistic.SpecialDefense]);
            Assert.AreEqual(14, uniquePokemon.Stats[PokemonEngine.Model.Statistic.Speed]);
            Assert.AreEqual(29, uniquePokemon.Stats[PokemonEngine.Model.Statistic.HP]);

            PokemonEngine.Model.Battle.IPokemon battlePokemon = new PokemonEngine.Model.Battle.Pokemon(uniquePokemon);
            battlePokemon.Stats.ShiftStage(PokemonEngine.Model.Statistic.Attack, -1);
            battlePokemon.Stats.ShiftStage(PokemonEngine.Model.Statistic.Defense, +2);
            battlePokemon.Stats.ShiftStage(PokemonEngine.Model.Statistic.SpecialAttack, -3);
            battlePokemon.Stats.ShiftStage(PokemonEngine.Model.Statistic.SpecialDefense, +3);
            battlePokemon.Stats.ShiftStage(PokemonEngine.Model.Statistic.Speed, -4);

            Assert.AreEqual(9, battlePokemon.Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(28, battlePokemon.Stats[PokemonEngine.Model.Statistic.Defense]);
            Assert.AreEqual(6, battlePokemon.Stats[PokemonEngine.Model.Statistic.SpecialAttack]);
            Assert.AreEqual(45, battlePokemon.Stats[PokemonEngine.Model.Statistic.SpecialDefense]);
            Assert.AreEqual(4, battlePokemon.Stats[PokemonEngine.Model.Statistic.Speed]);

            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseAttack, (battlePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseDefense, (battlePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.Defense]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpecialAttack, (battlePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.SpecialAttack]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpecialDefense, (battlePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.SpecialDefense]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseSpeed, (battlePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.Speed]);
            Assert.AreEqual(PokemonImpl.Bulbasaur.BaseHP, (battlePokemon as PokemonEngine.Model.IPokemon).Stats[PokemonEngine.Model.Statistic.HP]);

            Assert.AreEqual(15, (battlePokemon as PokemonEngine.Model.Unique.IPokemon).Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(14, (battlePokemon as PokemonEngine.Model.Unique.IPokemon).Stats[PokemonEngine.Model.Statistic.Defense]);
            Assert.AreEqual(16, (battlePokemon as PokemonEngine.Model.Unique.IPokemon).Stats[PokemonEngine.Model.Statistic.SpecialAttack]);
            Assert.AreEqual(18, (battlePokemon as PokemonEngine.Model.Unique.IPokemon).Stats[PokemonEngine.Model.Statistic.SpecialDefense]);
            Assert.AreEqual(14, (battlePokemon as PokemonEngine.Model.Unique.IPokemon).Stats[PokemonEngine.Model.Statistic.Speed]);
            Assert.AreEqual(29, (battlePokemon as PokemonEngine.Model.Unique.IPokemon).Stats[PokemonEngine.Model.Statistic.HP]);
        }

        [TestMethod]
        public void TestDamageCalculation()
        {
            PokemonEngine.Model.Battle.Team team1 = Helper.ConstructTeam("Trainer1", 10, 1);
            PokemonEngine.Model.Battle.Team team2 = Helper.ConstructTeam("Trainer2", 10, 1);

            NumberSequence sequence = new NumberSequence();
            sequence.Add(1.0); // No Critical Hit
            sequence.Add(0); // Maximum Damage

            IBattle battle = new Battle(sequence, NoOpBattleInputProvider.Instance, PokemonEngine.Model.Weather.ClearSkies, team1, team2);
            List<PokemonEngine.Model.Battle.Slot> targets = new List<PokemonEngine.Model.Battle.Slot> { team2[0] };
            InflictMoveDamage inflictDamage = new InflictMoveDamage(battle, team1[0].Pokemon.Moves[0], team1[0], targets);

            Assert.AreEqual(1.0f, inflictDamage.STABModifier);
            Assert.AreEqual(1.0f, inflictDamage.TypeModifier(targets[0]));
            Assert.AreEqual(6.0f, inflictDamage.LevelInfluence);

            Assert.AreEqual(1.0f, inflictDamage.TargetsModifier);
            Assert.AreEqual(1.0f, inflictDamage.RandomModifier);
            Assert.AreEqual(1.0f, inflictDamage.Modifier(targets[0]));

            Assert.AreEqual(7, inflictDamage.CalculateDamage(targets[0]));

            team1[0].Pokemon.Stats.ShiftStage(PokemonEngine.Model.Battle.Statistic.Attack, -1);
            sequence = new NumberSequence();
            sequence.Add(1.0); // No Critical Hit
            sequence.Add(15); // Minimum Damage
            battle = new Battle(sequence, NoOpBattleInputProvider.Instance, PokemonEngine.Model.Weather.ClearSkies, team1, team2);
            inflictDamage = new InflictMoveDamage(battle, team1[0].Pokemon.Moves[0], team1[0], targets);

            Assert.AreEqual(9, team1[0].Pokemon.Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(0.85f, inflictDamage.RandomModifier);
            Assert.AreEqual(4, inflictDamage.CalculateDamage(targets[0]));

            sequence = new NumberSequence();
            sequence.Add(1.0); // No Critical Hit
            sequence.Add(0); // Maximum Damage
            battle = new Battle(sequence, NoOpBattleInputProvider.Instance, PokemonEngine.Model.Weather.ClearSkies, team1, team2);
            inflictDamage = new InflictMoveDamage(battle, team1[0].Pokemon.Moves[0], team1[0], targets);

            Assert.AreEqual(9, team1[0].Pokemon.Stats[PokemonEngine.Model.Statistic.Attack]);
            Assert.AreEqual(1.0f, inflictDamage.RandomModifier);
            Assert.AreEqual(5, inflictDamage.CalculateDamage(targets[0]));
        }
    }
}
