using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ASCII has 128 characters 0-127

        //Control Characters(0–31 & 127): Control characters are not printable characters.
        //They are used to send commands to the PC or the printer and are based on telex technology.
        //With these characters, you can set line breaks or tabs.
        //Today, they are mostly out of use.

        //Special Characters(32–47 / 58–64 / 91–96 / 123–126):
        //Special characters include all printable characters that are neither letters nor numbers.
        //These include punctuation or technical, mathematical characters.
        //ASCII also includes the space(a non - visible but printable character), and,
        //therefore, does not belong to the control characters category, as one might suspect.

        //Numbers(30–39): These numbers include the ten Arabic numerals from 0 - 9.

        //Letters(65–90 / 97–122):
        //Letters are divided into two blocks,
        //with the first group containing the uppercase letters(65-90) and the second group containing the lowercase(97–122).

        private int _isNumber;
        public List<char> ASCII { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            if (UseSmallLetters.IsChecked.Value)
            {
                UseBigAndSmallLetters.IsChecked = false;
            }

            ASCII = new List<char>();
            for (int i = 32; i < 127; i++)
            {
                var c = (char)i;
                ASCII.Add(c);
            }
        }

        public static string CreatePasswordAllChars(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }            
            return res.ToString();
        }

        public static string CreatePasswordNumbersOnly(int length)
        {
            const string valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreatePasswordNumbersSmallLetters(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreatePasswordBigAndSmallLetters(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreatePasswordSmallLetters(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private void GeneratePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(InputBox.Text, out _isNumber);

            if (_isNumber == 0)
            {
                MessageBox.Show("Input Error, Integer Input you must!\n\nQuote from: 'Master Yoda'!");
            }
            else if (!UseNumbers.IsChecked.Value && !UseSmallLetters.IsChecked.Value && !UseBigAndSmallLetters.IsChecked.Value)
            {
                MessageBox.Show("Check some box you must mmmMmmM\n\nQuote from: 'Master Yoda'!");
            }
            else if (UseNumbers.IsChecked.Value && UseBigAndSmallLetters.IsChecked.Value
                || (UseNumbers.IsChecked.Value && UseBigAndSmallLetters.IsChecked.Value && UseSmallLetters.IsChecked.Value))
            {
                PasswordOutput.Text = CreatePasswordAllChars(int.Parse(InputBox.Text));
            }
            else if (UseNumbers.IsChecked.Value && UseSmallLetters.IsChecked.Value)
            {
                PasswordOutput.Text = CreatePasswordNumbersSmallLetters(int.Parse(InputBox.Text));
            }
            else if (UseSmallLetters.IsChecked.Value && !UseBigAndSmallLetters.IsChecked.Value)
            {
                PasswordOutput.Text = CreatePasswordSmallLetters(int.Parse(InputBox.Text));
            }
            else if (UseNumbers.IsChecked.Value)
            {
                PasswordOutput.Text = CreatePasswordNumbersOnly(int.Parse(InputBox.Text));
            }
            else if (UseBigAndSmallLetters.IsChecked.Value || (UseSmallLetters.IsChecked.Value && UseBigAndSmallLetters.IsChecked.Value))
            {
                PasswordOutput.Text = CreatePasswordBigAndSmallLetters(int.Parse(InputBox.Text));
            }           
                                         
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(PasswordOutput.Text);
        }
    }
}
