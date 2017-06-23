using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            if (textBox.Text != string.Empty && !Regex.IsMatch(textBox.Text, @"^[0-9a-zA-Z\.]+$"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text != string.Empty && !Regex.IsMatch(textBox.Text, @"^[a-zA-Z\.]+$"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }
    }
}
