using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop.Views.Dialogs
{
    class Error
    {
        private readonly Window root;
        private readonly Grid grid;
        private readonly Label errImage;
        private readonly Label errMessage;
        private readonly Button errButton;

        public event RoutedEventHandler Acceptance;

        public Error(string message)
        {
            // Definimos la ventana.
            root = new Window()
            {
                Width = 590,
                Height = 192,
                Title = "Error",
                ResizeMode = ResizeMode.NoResize
            };

            // Definimos el grid.
            grid = new Grid() 
            {
                Width = root.Width,
                Height = root.Height,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7f7f7"))
            };

            // Definimos las columnas del grid.
            ColumnDefinition colDef1 = new ColumnDefinition() 
            {
                Width = new GridLength((15 * root.Width) / 100)
            };
            
            grid.ColumnDefinitions.Add(colDef1);

            ColumnDefinition colDef2 = new ColumnDefinition() 
            {
                Width = new GridLength((80 * root.Width) / 100)
            };

            grid.ColumnDefinitions.Add(colDef2);

            // Definimos las filas del grid.
            RowDefinition rowDef1 = new RowDefinition() 
            {
                Height = new GridLength((65 * root.Height) / 100)
            };
            
            grid.RowDefinitions.Add(rowDef1);

            RowDefinition rowDef2 = new RowDefinition() 
            {
                Height = new GridLength((25 * root.Height) / 100)
            };

            grid.RowDefinitions.Add(rowDef2);

            // Creamos la imagen de error.
            errImage = new Label() 
            {
                // Definimos los atributos de la imagen de error.
                Height = 50,
                Width = 50,
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                FontSize = 32,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e81123")),
                Content = Char.ConvertFromUtf32(0xEA39),

                // Definimos los margenes de la imagen de error.
                Margin = new Thickness()
                {
                    Left = 0,
                    Right = 10,
                    Top = 0,
                    Bottom = 0
                }
            };

            // Establecemos el lugar en el grid.
            Grid.SetRow(errImage, 0);
            Grid.SetColumn(errImage, 0);

            // Creamos el texto de error.
            errMessage = new Label() 
            {
                // Definimos los margenes del texto de error.
                Height = 75,
                Width = (80 * root.Width) / 100,
                MinHeight = 75,
                MaxHeight = 100,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = new TextBlock() { Text = message, TextWrapping = TextWrapping.Wrap }
            };

            // Establecemos el lugar en el grid.
            Grid.SetRow(errMessage, 0);
            Grid.SetColumn(errMessage, 1);

            // Creamos el botón de aceptar.
            errButton = new Button()
            {
                // Definimos los margenes del botón de aceptar.
                Height = 30,
                Width = 100,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "Aceptar"
            };

            // Establecemos la función lambda del botón de aceptar.
            errButton.Click += (o, i) => {
                root.Close();
                Acceptance?.Invoke(this, i);
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
        }

        public void Show()
        {
            root.Show();
        }

        public void Close()
        {
            root.Close();
        }
    }
}
