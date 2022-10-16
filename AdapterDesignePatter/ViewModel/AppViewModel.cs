using AdapterDesignePatter.Command;
using AdapterDesignePatter.ConvertJsonAndXml;
using AdapterDesignePatter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdapterDesignePatter.ViewModel
{
    public class AppViewModel:BaseViewModel
    {
        public RelayCommand ClickCommand { get; set; }
        public RelayCommand JsonCommand { get; set; }
        public RelayCommand XamlCommand { get; set; }
        private Human human;

        public Human Human
        {
            get { return human; }
            set { human = value; OnPropertyChanged();}
        }


        public AppViewModel()
        {
            ClickCommand = new RelayCommand((e) =>
            {
                Human = new Human();
                IAdapter adapter;
                foreach (var item in App.MyCanvas.Children)
                {
                    if (item is RadioButton rb)
                    {
                        if (rb.IsChecked==true && rb.Name== "JsonRb")
                        {
                            WriteJson json = new WriteJson();
                            adapter = new JsonAdapter(json);
                            adapter.Write(Human);
                        }
                        if (rb.IsChecked == true && rb.Name == "JsonRb")
                        {
                             XamlWriter xaml = new XamlWriter();
                            adapter = new XamlAdapter(xaml);
                            xaml.WriteHuman(Human);
                        }
                    }
                }

            });
        }
    }
}
