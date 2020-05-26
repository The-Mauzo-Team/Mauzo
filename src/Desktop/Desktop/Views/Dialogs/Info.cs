using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop.Views.Dialogs
{
    class Info
    {
        
        public Info(string message)
        {
            TextBox textBox = new TextBox();
            textBox.Text = message;
            

            TextBox icono = new TextBox();
            icono.Text = Char.ConvertFromUtf32(0xE946);
            icono.FontFamily = new FontFamily("Segoe MDL2 Assets");



            Window window = new Window();
            Grid grid = new Grid();
            grid.Children.Add(textBox);
            grid.Children.Add(icono);

            window.Content = grid;

            window.Show();
        }
    }
}