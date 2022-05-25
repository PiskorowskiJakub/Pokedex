using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Data;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

using PokemonTcgSdk;
using PokemonTcgSdk.Models;

namespace Pokedex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            bool completeInitializeCard = InitializeCardList().IsCompleted;

            //Binding List
            if (completeInitializeCard)
            {
                List<CardClass> CardList = InitializeCardList().Result;
                cardListView.ItemsSource = CardList;
            }

        }

        public class CardClass
        {
            public int id { get; set; } // Id of element
            public string idCard { get; set; } // Id
            public string name { get; set; }   // Name
            public int hp { get; set; } // Hp
            public string attackName { get; set; } // Attack[0].Name
            public int attackDamage { get; set; } // Attack[0].Damage
            public string rarity { get; set; }  // Rarity
            public string series { get; set; } // Series
            public string image { get; set; }   //ImageUrlHires

            public CardClass(int cId, string cIdCard, string cName, int cHp, string cAttackName, int cAttackDamage, string cRarity, string cSeries, string cImages)
            {
                id = cId;
                idCard = cIdCard;
                name = cName;
                hp = cHp;
                attackName = cAttackName;
                attackDamage = cAttackDamage;
                rarity = cRarity;
                image = cImages;
                series = cSeries;
            }
        }

        public Task<List<CardClass>> InitializeCardList() 
        {

            List<CardClass> CardList = new List<CardClass>();

            var cards = Card.All().ToArray();
            int size = cards.Length;
            int tempHp, tempAttackDamage, id = 0, idsum = 0;
            string tempAttackName = "-";

            for (var i = 0; i < size; i++)
            {
                tempAttackName = "-";
                if (cards[i].SuperType == "Pokémon" &&  
                    cards[i].Series == "Base" && 
                    cards[i].EvolvesFrom == "-" && 
                    cards[i].Rarity != null)
                {
                    
                    if (cards[i].Attacks == null || cards[i].Attacks[0].Damage == ""){
                        tempAttackName = " ";
                        tempAttackDamage = 0;
                    }
                    else if (cards[i].Attacks[0].Damage.Contains("×") || 
                        cards[i].Attacks[0].Damage.Contains("+") ||
                        cards[i].Attacks[0].Damage.Contains("＋"))
                        tempAttackDamage = Convert.ToInt32(cards[i].Attacks[0].Damage.Replace("×", "").Replace("+", "").Replace("＋",""));
                    else
                        tempAttackDamage = Convert.ToInt32(cards[i].Attacks[0].Damage);

                    tempAttackName = tempAttackName == " " ? " " : cards[i].Attacks[0].Name;
                    tempHp = cards[i].Hp == "None" ? 0 : Convert.ToInt32(cards[i].Hp);

                    var mObj = new CardClass(
                        id,
                        cards[i].Id,
                        cards[i].Name,
                        tempHp,
                        tempAttackName,
                        tempAttackDamage,
                        cards[i].Rarity,
                        cards[i].Series,
                        cards[i].ImageUrlHiRes);

                    CardList.Add(mObj);
                    id++;
                }
                idsum += 1;
                
                
            }

            return Task.FromResult(CardList);


        }
    }
   
}
