using DevExpress.Mvvm.DataAnnotations;
using PokeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDamageCalculator
{
    [POCOViewModel]
    public class Calcmon
    {

        private Pokemon _basePokemon;
        public Pokemon BasePokemon
        {
            get { return _basePokemon; }
            set
            {
                _basePokemon = value;
                Update(_basePokemon);
            }
        }

        public virtual string Type1 { get; set; }
        public virtual string Type2 { get; set; }
        public virtual string Nature { get; set; }
        public virtual string Ability { get; set; }
        public virtual string Item { get; set; }
        public virtual Status Status { get; set; }

        public virtual string Level { get; set; }

        public virtual List<Stat> Stats { get; set; }
        public virtual List<Move> Moves { get; set; }



        public Calcmon()
        {
            Type1 = "Fire";
            Type2 = "";
            Nature = "";
            Ability = "";
            Item = "Life Orb";
            Status = Status.None;



            //--Initiate StatList
            Stats = new List<Stat>();
            for (int i = 0; i < 6; i++)
            {
                Stats.Add(new Stat((Stats)i, 0, 31, 0, 0));
            }

            Moves = new List<Move>();
            Moves.Add(new Move());
            Moves.Add(new Move());
            Moves.Add(new Move());
            Moves.Add(new Move());
            Moves[0].Name = "asdf";
        }


        public void Update(Pokemon pokemon)
        {
            //Update Stats
            for (int i = 0; i < 6; i++)
            {
                Stats[i].Base = pokemon.GetBaseStat((Stats)i);
            }

            Type1 = pokemon.Types[0].Type.Name.ToUpperFirst();
            Type2 = pokemon.Types.Count() == 2 ? pokemon.Types[1].Type.Name.ToUpperFirst() : "";


        }
    }
}
