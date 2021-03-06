﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PokemonDamageCalculator.Helper;
using static PokemonDamageCalculator.Mathp;

namespace PokemonDamageCalculator
{
    public enum Stats
    {
        HP,
        Attack,
        Defense,
        SpecialAttack,
        SpecialDefense,
        Speed
    }

    public enum Status
    {
        None,
        Burned,
        Poisoned,
        BadlyPoisoned,
        Paralyzed,
        Asleep,
        Frozen
    }

    public class Stat
    {
        public Stats StatName { get; private set; }
        public int Base { get; set; }
        public int IV { get; set; }
        public int EV { get; set; }
        public int Boost { get; set; }

        public Stat(Stats name_, int base_, int iv_, int ev_, int boost_)
        {
            StatName = name_;
            Base = base_;
            IV = iv_;
            EV = ev_;
            Boost = boost_;
        }

        //public int GetStatValue(Calcmon mon)
        //{
        //    return PokeRound((((2 * Base + IV + (EV / 4)) * mon.Level) / 100 + 5) * mon.Nature.GetNatureModifierValue(StatName));
        //}

    }

    public class Move
    {
        public virtual string Name { get; set; }

        public Move()
        {
            Name = "";
        }
    }
}
