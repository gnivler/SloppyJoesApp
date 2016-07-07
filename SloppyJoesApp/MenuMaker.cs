using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SloppyJoesApp
{
    class MenuMaker : INotifyPropertyChanged
    {
        public int NumberOfItems { get; set; }
        public ObservableCollection<MenuItem> Menu { get; private set; }
        public DateTime GeneratedDate { get; private set; }
        public Random random = new Random();
        private string[] Meats = { "Roast beef", "Salami", "Turkey", "Ham", "Pastrami" };
        private string[] Condiments = { "yellow mustard", "brown mustard", "honey mustard", "mayo", "relish", "French dressing" };
        private string[] Breads = { "rye", "white", "wheat", "pumpernickel", "Italian bread", "a roll" };

        public event PropertyChangedEventHandler PropertyChanged;

        public MenuMaker()
        {
            Menu = new ObservableCollection<MenuItem>();
            NumberOfItems = 10;
            UpdateMenu();
        }

        public void UpdateMenu()
        {
            Menu.Clear();
            GeneratedDate = DateTime.Now;
            
            for (int i = 0; Menu.Count() < NumberOfItems; i++)
            {
                bool unique = true;
                MenuItem newItem = CreateMenuItem();
                foreach (MenuItem item in Menu)
                {
                    if (newItem.ToString() == item.ToString())
                    {
                        unique = false;
                    }
                }
                if (unique)
                {
                    Menu.Add(CreateMenuItem());
                }
            }
            OnPropertyChanged("GeneratedDate");
        }

        private MenuItem CreateMenuItem()
        {
            string meat = Meats[random.Next(Meats.Length)];
            string condiment = Condiments[random.Next(Condiments.Length)];
            string bread = Breads[random.Next(Breads.Length)];
            return new MenuItem(meat, condiment, bread);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
