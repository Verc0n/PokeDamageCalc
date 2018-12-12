using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PokeAPI;
using System.Threading;

namespace PokemonDamageCalculator.ViewModels
{
    [POCOViewModel]
    public class MainWindowVM
    {
        //-- Static Data Lists
        public virtual List<string> StaticPokemonNames { get; set; }
        public virtual List<string> StaticNatures { get; set; }
        public virtual List<string> StaticTypes { get; set; }
        public virtual List<string> StaticItems { get; set; }
        public virtual List<Status> StaticStatus { get; set; }

        public virtual List<string> Abilities { get; set; }


        public virtual string SelectedPokemon { get; set; }

        public virtual string SelectedType1 { get; set; }
        public virtual string SelectedType2 { get; set; }
        public virtual string SelectedNature { get; set; }
        public virtual string SelectedAbility { get; set; }
        public virtual string SelectedItem { get; set; }
        public virtual Status SelectedStatus { get; set; }






        public MainWindowVM()
        {
            Data.Init();
            DataFetcher.DataBackend = new VerconHttpBackend();


            StaticPokemonNames = Data.Pokemon;
            StaticNatures = Data.Natures;
            StaticTypes = Data.Types;
            StaticItems = Data.Items;
            StaticStatus = Helper.GetEnumValues<Status>().ToList();

            Abilities = new List<string>();

            SelectedPokemon = "";
            SelectedType1 = "";
            SelectedType2 = "";
            SelectedAbility = "";
            SelectedItem = "";
            SelectedStatus = Status.None;
        }

        public async void UpdateData()
        {
            if (SelectedPokemon == null) return;
            if (!StaticPokemonNames.Contains(SelectedPokemon)) return;

            Pokemon p = await DataFetcher.GetNamedApiObject<Pokemon>(SelectedPokemon);

            SelectedType1 = p.Types[0].Type.Name.ToUpperFirst();
            SelectedType2 = p.Types.Count() == 2 ? p.Types[1].Type.Name.ToUpperFirst() : "";

            Abilities = p.Abilities.Select(a => a.Ability.Name).ToList();
            SelectedAbility = Abilities[0];
        }

        public async Task<Pokemon> GetPokemon()
        {
            return await DataFetcher.GetNamedApiObject<Pokemon>("excadrill");
        }

    }



}
