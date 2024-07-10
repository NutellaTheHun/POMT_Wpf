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
            ErrorService.Instance().SquareKeyMissingEvent += NotifyUserSquareKeyMissing;
            ErrorService.RaiseOrderViewEvents();
        }

        public void NotifyUserNewItem(object sender, EventArgs e)
        {
            NewItemEventWindow window = new NewItemEventWindow((SoiNewItemEventArgs)e);
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.Show();
        }

        public void NotifyUserMultiItemMatch(object sender, EventArgs e)
        {
            NotifyMultiItemMatchWindow view = new NotifyMultiItemMatchWindow((SoiMultiItemEventArgs)e);
            view.Owner = System.Windows.Application.Current.MainWindow;
            view.Show();
        }

        public void NotifyUserSquareKeyMissing(object sender, EventArgs e)
        {
            SquareKeyMissingWindow view = new SquareKeyMissingWindow();
            view.Owner = System.Windows.Application.Current.MainWindow;
            view.Show();
        }
    }
}

