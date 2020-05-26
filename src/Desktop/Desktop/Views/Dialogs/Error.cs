using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.Views.Dialogs
{
    class Error
    { 
        public void print(string message)
        {
            // Definimos la ventana.
            Window root = new Window();
            root.Width = 590;
            root.Height = 192;
            root.Title = "Error";
            root.ResizeMode = ResizeMode.NoResize;

            // Definimos el grid.
            Grid grid = new Grid();
            grid.Width = root.Width;
            grid.Height = root.Height;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            // Definimos las columnas del grid.
            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength((15 * root.Width) / 100);
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = new GridLength((80 * root.Width) / 100);
            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);

            // Definimos las filas del grid.
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            rowDef1.Height = new GridLength((65 * root.Height) / 100);
            rowDef2.Height = new GridLength((25 * root.Height) / 100);
            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);

            // Creamos la imagen de error.
            Label errImage = new Label();

            // Definimos los atributos de la imagen de error.
            errImage.Height = 50;
            errImage.Width = 50;
            errImage.FontFamily = new FontFamily("Segoe MDL2 Assets");
            errImage.FontSize = 32;
            errImage.HorizontalAlignment = HorizontalAlignment.Right;
            errImage.VerticalAlignment = VerticalAlignment.Center;
            errImage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e81123"));
            errImage.Content = Char.ConvertFromUtf32(0xEA39);

            // Definimos los margenes de la imagen de error.
            Thickness marginImage = new Thickness();
            marginImage.Left = 0;
            marginImage.Right = 10;
            marginImage.Top = 0;
            marginImage.Bottom = 0;
            errImage.Margin = marginImage;

            // Establecemos el lugar en el grid.
            Grid.SetRow(errImage, 0);
            Grid.SetColumn(errImage, 0);

            // Creamos el texto de error.
            Label errMessage = new Label();

            // Definimos los margenes del texto de error.
            errMessage.Height = 75;
            errMessage.Width = (80 * root.Width) / 100;
            errMessage.MinHeight = 75;
            errMessage.MaxHeight = 100;
            errMessage.VerticalAlignment = VerticalAlignment.Center;
            errMessage.HorizontalAlignment = HorizontalAlignment.Left;
            errMessage.Content = new TextBlock() { Text = message, TextWrapping = TextWrapping.Wrap };

            // Establecemos el lugar en el grid.
            Grid.SetRow(errMessage, 0);
            Grid.SetColumn(errMessage, 1);

            // Creamos el botón de aceptar.
            Button errButton = new Button();

            // Definimos los margenes del botón de aceptar.
            errButton.Height = 30;
            errButton.Width = 100;
            errButton.VerticalAlignment = VerticalAlignment.Top;
            errButton.HorizontalAlignment = HorizontalAlignment.Right;
            errButton.Content = "Aceptar";

            // Establecemos la función lambda del botón de aceptar.
            errButton.Click += (o, i) => {
                root.Close();
            };

            // Establecemos el lugar en el grid.
            Grid.SetRow(errButton, 1);
            Grid.SetColumn(errButton, 1);

            // Añadimos los elementos al grid.
            grid.Children.Add(errImage);
            grid.Children.Add(errMessage);
            grid.Children.Add(errButton);

            // Asociamos el grid a la ventana y la mostramos.
            root.Content = grid;
            root.Show();
        }
    }
}
