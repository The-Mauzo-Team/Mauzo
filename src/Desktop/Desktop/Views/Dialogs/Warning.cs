using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop.Views.Dialogs
{
    class Warning
    {
        private readonly Button accept;
        private readonly Button cancel;
        private readonly Window win;

        public event RoutedEventHandler Acceptance;
        public event RoutedEventHandler Cancellation;

        public Warning(string advice, string answer)
        {
            win = new Window
            {
                Width = 590,
                Height = 192,
                Title = "Advertencia",
                ResizeMode = ResizeMode.NoResize
            };

            Grid grid = new Grid
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7f7f7"))
            };


            Label icon = new Label
            {
                Height = 50,
                Width = 50,
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                FontSize = 32,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff8c00")),
                Content = Char.ConvertFromUtf32(0xE7BA),

                HorizontalAlignment = HorizontalAlignment.Left
            };
            Thickness marginIcon = new Thickness
            {
                Left = 15,
                Top = 20
            };
            icon.Margin = marginIcon;


            Label message = new Label
            {
                Height = 75,
                Content = new TextBlock()
                {
                    Text = advice + "\n\n" + answer,
                    TextWrapping = TextWrapping.Wrap
                },

                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Thickness marginMsg = new Thickness
            {
                Left = 75,
                Right = 20,
                Top = 5,
                Bottom = 65
            };
            message.Margin = marginMsg;


            accept = new Button
            {
                Height = 30,
                Width = 100,
                Content = "Aceptar",

                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Thickness marginAcpt = new Thickness
            {
                Right = 20,
                Bottom = 20
            };
            accept.Margin = marginAcpt;


            cancel = new Button
            {
                Height = 30,
                Width = 100,
                Content = "Cancelar",

                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Thickness marginCacl = new Thickness
            {
                Right = 140,
                Bottom = 20
            };
            cancel.Margin = marginCacl;


            grid.Children.Add(icon);
            grid.Children.Add(message);
            grid.Children.Add(accept);
            grid.Children.Add(cancel);

            win.Content = grid;
            win.Show();

            accept.Click += (o, i) =>
            {
                win.Close();
                Acceptance(this, i);
            };
            
            cancel.Click += (o, i) =>
            {
                win.Close();
                Cancellation(this, i);
            };
        }

        public void Close()
        {
            win.Close();
        }
    }
}
