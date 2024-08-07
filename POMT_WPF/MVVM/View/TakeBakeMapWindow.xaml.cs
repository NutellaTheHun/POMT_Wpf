using Petsi.Units;
using POMT_WPF.MVVM.ObsModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for TakeBakeMapWindow.xaml
    /// </summary>
    public partial class TakeBakeMapWindow : Window
    {
        public bool ControlBool;
        public ObservableCollection<CatalogItemPetsi> TakeBakeList { get; set; }

        private CatalogItemPetsi _selection;
        public CatalogItemPetsi Selection
        {
            get { return _selection; }
            set
            {
                if (_selection != value)
                {
                    _selection = value;
                    OnPropertyChanged(nameof(Selection));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TakeBakeMapWindow()
        {
            InitializeComponent();
            TakeBakeListBox.MouseDoubleClick += DoneButton_Click;
            DataContext = this;
            List<CatalogItemPetsi> items = ObsCatalogModelSingleton.Instance.CatalogItems.Where(x => x.ItemName.ToLower().Contains("take 'n bake")).ToList();
            TakeBakeList = new ObservableCollection<CatalogItemPetsi>(items);
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Selection != null) ControlBool = true;
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ControlBool = false;
            Close();
        }
    }
}
