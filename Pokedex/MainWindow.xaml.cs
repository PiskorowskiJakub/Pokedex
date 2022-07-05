using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FontAwesome.WPF;
using PokemonTcgSdk;

namespace Pokedex
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
        
        private void changeView(object sender, RoutedEventArgs e)
        {
            if (detailedView.IsChecked == true) cardListView.Visibility = Visibility.Visible;
            else if (listView.IsChecked == true || generalView.IsChecked == true) cardListView.Visibility = Visibility.Collapsed;
        }
        private void reloadData(object sender, RoutedEventArgs e)
        {
            
            

            loadingSpinner.Visibility = Visibility.Visible;
            MessageBox.Show("Wczytywanie danych");


            var resultCardList = Task.Run(() => InitializeCardList());
            resultCardList.Wait();
            bool completeInitializeCard = resultCardList.IsCompleted;

            if (completeInitializeCard)
            {
                List<CardClass> CardList = resultCardList.Result;
                cardListView.ItemsSource = CardList;
                MessageBox.Show("Dane wczytane");
                loadingSpinner.Visibility = Visibility.Collapsed;
                return;
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

                    if (cards[i].Attacks == null || cards[i].Attacks[0].Damage == "")
                    {
                        tempAttackName = " ";
                        tempAttackDamage = 0;
                    }
                    else if (cards[i].Attacks[0].Damage.Contains("×") ||
                        cards[i].Attacks[0].Damage.Contains("+") ||
                        cards[i].Attacks[0].Damage.Contains("＋"))
                        tempAttackDamage = Convert.ToInt32(cards[i].Attacks[0].Damage.Replace("×", "").Replace("+", "").Replace("＋", ""));
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
