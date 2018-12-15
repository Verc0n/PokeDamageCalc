using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeAPI;

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
        
        public virtual Calcmon CalcMon { get; set; }



        public MainWindowVM()
        {
            Data.Init();
            DataFetcher.DataBackend = new VerconHttpBackend();
            CalcMon = new Calcmon();

            StaticPokemonNames = Data.Pokemon;
            StaticNatures = Data.Natures;
            StaticTypes = Data.Types;
            StaticItems = Data.Items;
            StaticStatus = Helper.GetEnumValues<Status>().ToList();

            Abilities = new List<string>();

            SelectedPokemon = "";
        }

        public async void UpdateData()
        {
            if (SelectedPokemon == null) return;
            if (!StaticPokemonNames.Contains(SelectedPokemon)) return;

            Pokemon p = await DataFetcher.GetNamedApiObject<Pokemon>(SelectedPokemon);

            CalcMon.BasePokemon = p;
            Abilities = p.Abilities.Select(a => a.Ability.Name).ToList();
            CalcMon.Ability = Abilities[0];

            this.RaisePropertyChanged(x => x.CalcMon);
        }

        public async Task<Pokemon> GetPokemon()
        {
            return await DataFetcher.GetNamedApiObject<Pokemon>("excadrill");
        }

    }

}
