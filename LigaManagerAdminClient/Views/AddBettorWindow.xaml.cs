using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace LigaManagerAdminClient.Views
{
    /// <summary>
    /// Interaction logic for AddBettorWindow.xaml
    /// </summary>
    public partial class AddBettorWindow : Window
    {
        public AddBettorWindow()
        {
            InitializeComponent();
        }

        private void OnTextChangedNickname(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text != string.Empty && !Regex.IsMatch(textBox.Text, @"^[0-9a-zA-Z-üÜ-äÄ-öÖ-ß\.]+$"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text != string.Empty && !Regex.IsMatch(textBox.Text, @"^[a-zA-Z-üÜ-äÄ-öÖ-ß\.]+$"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }
    }
}
