using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace LigaManagerBettorClient.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Page
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text != string.Empty && !Regex.IsMatch(textBox.Text, @"^[0-9a-zA-Z-üÜ-äÄ-öÖ-ß\.]+$"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }
    }
}
