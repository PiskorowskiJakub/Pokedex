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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCardList();
        }

        public class CardClass
        {
            public string? id { get; set; } // Id
            public string? name { get; set; }   // Name
            public int hp { get; set; } // Hp
            public string attackName { get; set; } // Attack[0].Name
            public int attackDamage { get; set; } // Attack[0].Damage
            public string rarity { get; set; }  //Rarity
            public string images { get; set; } //ImageUrlHires

            public CardClass(string cId, string cName, int cHp, string cAttackName, int cAttackDamage, string cRarity, string cImages)
            {
                id = cId;
                name = cName;
                hp = cHp;
                attackName = cAttackName;
                attackDamage = cAttackDamage;
                rarity = cRarity;
                images = cImages;
            }
        }
        public List<CardClass> CardList = null;

        private async void BindCardList()
        {
            //grdCards.ItemsSource = ;
            //TextCard.Text = ;

            
            var cards = Card.All().ToArray();
            int size = cards.Length;
            

            

            // -----------------------------------------
            /*
            int size = cards.Length;
            StringBuilder sb = new StringBuilder();



            foreach (var item in cards)
            {
                sb.Append(item.Name + '\n');
            }*/
            /*
            for (int i = 0; i < size; i++)
            {
                sb.AppendFormat( "{0}. {1} \n", i, cards[i].Name);
            }

            string app_ouput = sb.ToString();
            tbMultiLine.Text = app_ouput;
            */
            // ------------------------------------------
            /*
            var cards = Card.All().ToArray();
            int size = cards.Length;
            string[] numA = new string[size];

            for(int i = 0; i < size; i++)
            {
                numA[i] = cards[i].Name;
            }
            tbMultiLine.Text = string.Join("\n", numA).ToString();
            */


        }
    }
   
}
