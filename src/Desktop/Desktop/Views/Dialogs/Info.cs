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
            //Creamos la ventana
            Window window = new Window();
            window.Title = "Info";
            window.Width = 590;
            window.Height = 192;
            window.ResizeMode = ResizeMode.NoResize;

            //Creamos el grid con el mismo tamaño
            Grid grid = new Grid();
            grid.Width = window.Width;
            grid.Height = window.Height;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            // Definimos las filas del grid.
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            rowDef1.Height = new GridLength(124.8);
            rowDef2.Height = new GridLength(48);
            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);

            // Definimos las columnas del grid.
            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength(88.5);
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = new GridLength(472);
            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);

            //Icono de info
            Label icono = new Label();
            icono.Content = Char.ConvertFromUtf32(0xE946);
            icono.FontFamily = new FontFamily("Segoe MDL2 Assets");
            icono.Height = 50;
            icono.Width = 50;
            icono.FontSize = 32;
            icono.HorizontalAlignment = HorizontalAlignment.Right;
            icono.VerticalAlignment = VerticalAlignment.Center;
            icono.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1e88e5"));
            Grid.SetRow(icono, 0);
            Grid.SetColumn(icono, 0);
            Thickness marginImage = new Thickness();
            marginImage.Left = 0;
            marginImage.Right = 10;
            marginImage.Top = 0;
            marginImage.Bottom = 0;
            icono.Margin = marginImage;


            //Mensaje
            Label text = new Label();
            text.Height = 75;
            text.Width = 472;
            text.MinHeight = 75;
            text.MaxHeight = 100;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.Content = new TextBlock() { Text = message, TextWrapping = TextWrapping.Wrap };
            Grid.SetRow(text, 0);
            Grid.SetColumn(text, 1);

            // Creamos el botón de aceptar.
            Button aceptar = new Button();
            aceptar.Content = "Aceptar";
            aceptar.Height = 30;
            aceptar.Width = 100;
            Grid.SetRow(aceptar, 1);
            Grid.SetColumn(aceptar, 1);

            // Definimos los margenes.
            aceptar.VerticalAlignment = VerticalAlignment.Top;
            aceptar.HorizontalAlignment = HorizontalAlignment.Right;

            // Establecemos la función lambda para cerrar la ventana.
            aceptar.Click += (o, i) => {
                window.Close();
            };

            grid.Children.Add(icono);
            grid.Children.Add(text);
            grid.Children.Add(aceptar);

            window.Content = grid;
            window.Show();
        }
    }
}