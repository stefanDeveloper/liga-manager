using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace LigaManagerBettorClient.Views
{
    /// <summary>
    /// Interaction logic for DetailMatchWindow.xaml
    /// </summary>
    public partial class DetailMatchWindow : Window
    {
        public DetailMatchWindow()
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
            if (textBox.Text != string.Empty && int.Parse(textBox.Text) > 24)
            {
                textBox.Text = "24";
            }
        }
    }
}
