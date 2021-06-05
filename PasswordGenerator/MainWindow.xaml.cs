using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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
        // 32 je space, 
        // 33 !   // 58 :     // 123 {
        // 34 "   // 59 ;     // 124 |
        // 35 #   // 60 <     // 125 }
        // 36 $   // 61 =     // 126 ~
        // 37 %   // 62 >     //
        // 38 &   // 63 ?
        // 39 '   // 64 @
        // 40 (   //
        // 41 )   // 91 [
        // 42 *   // 92 \
        // 43 +   // 93 ]
        // 44 ,   // 94 ^
        // 45 -   // 95 _
        // 46 .   // 96 `
        // 47 /   //

        //Numbers(48–57): These numbers include the ten Arabic numerals from 0 - 9.

        //Letters(65–90 / 97–122):
        //Letters are divided into two blocks,
        //with the first group containing the uppercase letters(65-90) and the second group containing the lowercase(97–122).

        private int _isNumber;
        public List<char> AsciiList { get; set; }
        public List<char> AddedAsciiCharsForGeneratorList { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            AsciiList = new List<char>();
            AddedAsciiCharsForGeneratorList = new List<char>();

            for (int i = 32; i < 127; i++)
            {
                var c = (char)i;
                AsciiList.Add(c);
            }
        }

        public static string GeneratePassword(int length, List<char> list)
        {
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(list[rnd.Next(list.Count)]);
            }
            return res.ToString();
        }

        private void GeneratePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(InputBox.Text, out _isNumber);

            if (_isNumber == 0)
            {
                MessageBox.Show("The cautious seldom err! Enter password length. \n\nQuote from: 'Confucius'!");
                return;
            }
            if (!UseAll.IsChecked.Value && !UseNumbers.IsChecked.Value && !UseSmallLetters.IsChecked.Value
                    && !UseCapitalLetters.IsChecked.Value && !UseSymbols.IsChecked.Value && !UseSpaceCharacter.IsChecked.Value)
            {
                MessageBox.Show("Check some box you must mmmMmmM\n\nQuote from: 'Master Yoda'!");
                return;
            }

            if (UseAll.IsChecked.Value)
                PasswordOutput.Text = GeneratePassword(_isNumber, AsciiList.GetRange(1, AsciiList.Count-1));
            else 
            {
                if (UseNumbers.IsChecked.Value)
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(16, 10));
                if (UseSmallLetters.IsChecked.Value)
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(65, 25));
                if (UseCapitalLetters.IsChecked.Value)
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(33, 25));
                if (UseSymbols.IsChecked.Value)
                {
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(1, 15));
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(26, 7));
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(60, 5));
                    AddedAsciiCharsForGeneratorList.AddRange(AsciiList.GetRange(91, 4));
                }
                if (UseSpaceCharacter.IsChecked.Value)
                    AddedAsciiCharsForGeneratorList.Add(AsciiList[0]);

                PasswordOutput.Text = GeneratePassword(_isNumber, AddedAsciiCharsForGeneratorList);
                AddedAsciiCharsForGeneratorList.Clear();
            }
            //Bools:
            //UseAll
            //UseNumbers
            //UseSmallLetters
            //UseCapitalLetters
            //UseSymbols
            //UseSpaceCharacter

        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(PasswordOutput.Text);
        }
    }
}
