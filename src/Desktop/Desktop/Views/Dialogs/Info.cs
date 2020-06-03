using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop.Views.Dialogs
{
    class Info
    {
        private readonly Window window;
        private readonly Button accept;

        public event RoutedEventHandler Acceptance;

        /// <summary>
        /// Constructor para crear la interfaz
        /// </summary>
        /// <param name="message"></param>
        public Info(string message)
        {
            //Creamos la ventana
            window = new Window
            {
                Title = "Info",
                Width = 590,
                Height = 192,
                ResizeMode = ResizeMode.NoResize
            };

            //Centramos la ventana en la pantalla
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;
            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);

            //Creamos el grid con el mismo tamaño
            Grid grid = new Grid
            {
                Width = window.Width,
                Height = window.Height,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                //Definimos un color de fondo
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7f7f7"))

            };

            // Definimos las filas del grid.
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            rowDef1.Height = new GridLength(124.8);
            rowDef2.Height = new GridLength(48);
            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);

            // Definimos las columnas del grid.
            ColumnDefinition colDef1 = new ColumnDefinition
            {
                Width = new GridLength(88.5)
            };
            ColumnDefinition colDef2 = new ColumnDefinition
            {
                Width = new GridLength(472)
            };
            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);

            //Icono de info
            Label icono = new Label
            {
                Content = Char.ConvertFromUtf32(0xE946),
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                Height = 50,
                Width = 50,
                FontSize = 32,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1e88e5"))
            };

            Grid.SetRow(icono, 0);
            Grid.SetColumn(icono, 0);

            Thickness marginImage = new Thickness
            {
                Left = 0,
                Right = 10,
                Top = 0,
                Bottom = 0
            };
            icono.Margin = marginImage;


            //Mensaje
            Label text = new Label
            {
                Height = 75,
                Width = 472,
                MinHeight = 75,
                MaxHeight = 100,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = new TextBlock() { Text = message, TextWrapping = TextWrapping.Wrap }
            };
            Grid.SetRow(text, 0);
            Grid.SetColumn(text, 1);

            // Creamos el botón de aceptar.
            accept = new Button
            {
                Content = "Aceptar",
                Height = 30,
                Width = 100
            };
            Grid.SetRow(accept, 1);
            Grid.SetColumn(accept, 1);

            // Definimos los margenes.
            accept.VerticalAlignment = VerticalAlignment.Top;
            accept.HorizontalAlignment = HorizontalAlignment.Right;

            // Establecemos la función lambda para cerrar la ventana al dar click en aceptar.
            accept.Click += (o, i) => {
                window.Close();
                Acceptance?.Invoke(this, i);
            };

            //Añadimos los elementos al grid
            grid.Children.Add(icono);
            grid.Children.Add(text);
            grid.Children.Add(accept);

            window.Content = grid;
        }

        /// <summary>
        /// Función para que aparezca la ventana
        /// </summary>
        public void Show()
        { 
            window.Show();
        }
    }
}