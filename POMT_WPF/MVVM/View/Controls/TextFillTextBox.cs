using System.Windows;
using System.Windows.Controls;

namespace POMT_WPF.MVVM.View.Controls
{
    public class TextFillTextBox : TextBox
    {
        public static readonly DependencyProperty TextFillProperty =
            DependencyProperty.Register(
                nameof(TextFill),
                typeof(string),
                typeof(TextFillTextBox),
                new PropertyMetadata(string.Empty));

        public string TextFill
        {
            get { return (string)GetValue(TextFillProperty); }
            set { SetValue(TextFillProperty, value); }
        }

        static TextFillTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextFillTextBox), new FrameworkPropertyMetadata(typeof(TextFillTextBox)));
        }

        public TextFillTextBox()
        {
            this.TextChanged += TextFillTextBox_TextChanged;
        }

        private void TextFillTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Explicitly notify property change
            SetCurrentValue(TextProperty, this.Text);
        }
    }
}
