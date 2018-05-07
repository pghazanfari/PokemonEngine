﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonEngine.Model.Battle.Messaging;

namespace PokemonEngine.Model.Battle.Actions
{
    public class Run : IBattleAction
    {
        private readonly BattleSlot user;
        public BattleSlot User { get { return user; } }

        public Run(BattleSlot user)
        {
            this.user = user;
        }
        
        public void Dispatch(IBattleMessageSubscriber receiver)
        {
            receiver.Receive(this);
        }
    }
}