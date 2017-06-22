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
    /// Interaction logic for AddMatchWindow.xaml
    /// </summary>
    public partial class AddMatchWindow : Window
    {
        public AddMatchWindow()
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

        private void OnHourChanged(object sender, TextChangedEventArgs e)
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

        private void OnMinuteChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
            if (textBox.Text != string.Empty && int.Parse(textBox.Text) > 60)
            {
                textBox.Text = "60";
            }
        }
    }
}
