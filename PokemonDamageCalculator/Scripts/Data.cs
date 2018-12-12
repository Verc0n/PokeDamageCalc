using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDamageCalculator
{
    public static class Data
    {
        public static List<string> Natures;
        public static List<string> Items;
        public static List<string> Types;
        public static List<string> Moves;
        public static List<string> Pokemon;

        public static void Init()
        {
            Natures = File.ReadAllLines("DB/Natures.txt").ToList();
            Items = File.ReadAllLines  ("DB/Items.txt").ToList();
            Types = File.ReadAllLines  ("DB/Types.txt").ToList();
            Moves = File.ReadAllLines  ("DB/Moves.txt").ToList();
            Pokemon = File.ReadAllLines("DB/Pokedex.txt").ToList();
        }
    }
}