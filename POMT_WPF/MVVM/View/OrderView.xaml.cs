using System.Windows.Controls;
using Petsi.Services;
using Petsi.Events;

namespace POMT_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            ErrorService.Instance().SoiNewItem += NotifyUserNewItem;
            ErrorService.Instance().SoiMultiItem += NotifyUserMultiItemMatch;
            ErrorService.RaiseWindowEvents();
        }

        public void NotifyUserNewItem(object sender, EventArgs e)
        {
           NotifyNewCatalogItemWindow view = new NotifyNewCatalogItemWindow((SoiNewItemEventArgs)e);
           view.Show();
        }

        public void NotifyUserMultiItemMatch(object sender, EventArgs e)
        {
            NotifyMultiItemMatchWindow view = new NotifyMultiItemMatchWindow((SoiMultiItemEventArgs)e);
            view.Show();
        }
    }
}

