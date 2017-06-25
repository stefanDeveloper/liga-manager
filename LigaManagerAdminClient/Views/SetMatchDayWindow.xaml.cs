using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace LigaManagerAdminClient.Views
{
    /// <summary>
    /// Interaction logic for SetMatchDayWindow.xaml
    /// </summary>
    public partial class SetMatchDayWindow : Window
    {
        public SetMatchDayWindow()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }
    }
}
