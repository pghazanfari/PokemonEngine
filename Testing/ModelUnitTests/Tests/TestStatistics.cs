using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelUnitTests.Tests
{
    [TestClass]
    public class TestStatistics
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
    }
}
