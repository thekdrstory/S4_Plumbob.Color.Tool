////////////////////////////////////////////////////////////////////////////////
//
// 07-07-2023
// This tool creates a default.ini file that has user chosen plumbob colours
// that can be placed in their sims 4 overrideconfig folder!
//
// This was made by Kevin Rosas!
// I don't mind if people study this, it ain't worth much anyways!
//
//
// CREDITS TO THE CREATOR OF HALEY! THE COLOUR PICKER HAS SAVED MY LIFE!!!
//
//
///////////////////////////////////////////////////////////////////////////////
using Haley.Services;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Color = System.Drawing.Color;

namespace S4_Custom_Plumbob_Tool_WPF
{
    public partial class MainWindow : Window
    {
        public string newFilePath;
        public bool nameEntered;

        public MainWindow()
        {
            InitializeComponent();

            // Creates default colour codes.
            ReloadRGBACodes();
        }

        // This event fires when User clicks a colour button!
        private void Choose_Colour(object sender, RoutedEventArgs e)
        {
            Button colourButton = (Button)sender;
            ColorPickerDialog colourPicker = new ColorPickerDialog();
            bool? result = colourPicker.ShowDialog();

            if (result == true)
            {
                colourButton.Background = colourPicker.SelectedBrush;
                ReloadRGBACodes();
            }
        }

        // Event fires when User clicks the "Generate" Button!
        private void Generate_RGBA_Codes(object sender, RoutedEventArgs e)
        {
            ReloadRGBACodes();

            // Prompts user to save.
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "INI Files (*.ini)|*.ini";
            saveDialog.Title = "Save .ini file";
            saveDialog.FileName = "Default";
            bool? result = saveDialog.ShowDialog();

            // Opens file directory to save using user inputted name.
            if (result == true)
            {
                newFilePath = saveDialog.FileName;
                nameEntered = true;
            }
            else
            {
                nameEntered = false;
            }

            if(nameEntered == true)
            {
                using (StreamWriter writer = new StreamWriter(newFilePath))
                {
                    writer.WriteLine("[PlumbBob]");
                    writer.WriteLine("playeractivecolor = " + lbl_playerActiveColorCode.Content);
                    writer.WriteLine("motivestatered = " + lbl_motiveStateRedColorCode.Content);
                    writer.WriteLine("motivestateorange = " + lbl_motiveStateOrangeColorCode.Content);
                    writer.WriteLine("motivestateyellow = " + lbl_motiveStateYellowColorCode.Content);
                }
            }
            else
            {

            }

        }

        // I'm paranoid so I clear the string incase something goes through...
        // Then I display the new color codes that the USER chooses.
        private void ReloadRGBACodes()
        {
            lbl_playerActiveColorCode.Content = "";
            lbl_motiveStateYellowColorCode.Content = "";
            lbl_motiveStateOrangeColorCode.Content = "";
            lbl_motiveStateRedColorCode.Content = "";
            lbl_playerActiveColorCode.Content = GenerateRgb(btn_playerActive.Background.ToString());
            lbl_motiveStateYellowColorCode.Content = GenerateRgb(btn_motiveStateYellow.Background.ToString());
            lbl_motiveStateOrangeColorCode.Content = GenerateRgb(btn_motiveStateOrange.Background.ToString());
            lbl_motiveStateRedColorCode.Content = GenerateRgb(btn_motiveStateRed.Background.ToString());
        }
        // Converts 8 Digit Hex code to ARGB format.
        private string GenerateRgb(string bg)
        {
            Color color = ColorTranslator.FromHtml(bg);
            double a = Convert.ToInt16(color.A);
            double r = Convert.ToInt16(color.R);
            double g = Convert.ToInt16(color.G);
            double b = Convert.ToInt16(color.B);
            double sa = a / 255;
            double sr = r / 255;
            double sg = g / 255;
            double sb = b / 255;
            return string.Format("{0:N2}, {1:N2}, {2:N2}, {3:N2}", sr, sg, sb, sa);
        }

        //Credits
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "https://modthesims.info/member.php?u=10313368";
            p.Start();
        }

    }
}
