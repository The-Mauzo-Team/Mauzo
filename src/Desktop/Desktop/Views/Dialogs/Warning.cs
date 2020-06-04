/*
 * MIT License
 *
 * Copyright (c) 2020 The Mauzo Team
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop.Views.Dialogs
{
    /// <summary>
    /// Clase para gestionar una ventana de advertencia.
    /// </summary>
    /// <remarks>@ant04x Antonio Izquierdo</remarks>
    class Warning
    {
        private readonly Button accept;
        private readonly Button cancel;
        private readonly Window win;

        public event RoutedEventHandler Acceptance;
        public event RoutedEventHandler Cancellation;

        /// <summary>
        /// Creamos una ventana para mostrar información de la advertencia.
        /// </summary>
        /// <param name="advice">Mensaje de advertencia</param>
        /// <param name="answer">Pregunta a confirmar</param>
        public Warning(string advice, string answer)
        {
            // Definimos la ventana.
            win = new Window
            {
                Width = 590,
                Height = 192,
                Title = "Advertencia",
                ResizeMode = ResizeMode.NoResize
            };

            // Definimos el grid.
            Grid grid = new Grid
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7f7f7"))
            };


            // Centrar Ventana
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = win.Width;
            double windowHeight = win.Height;
            win.Left = (screenWidth / 2) - (windowWidth / 2);
            win.Top = (screenHeight / 2) - (windowHeight / 2);

            // Definimos el icono
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

            // Definimos el menaje con la pregunta.
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

            // Definición del botón de aceptar
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
                Right = 140,
                Bottom = 20
            };
            accept.Margin = marginAcpt;

            // Definición del botón de cancelar
            cancel = new Button
            {
                Height = 30,
                Width = 100,
                Content = "Cancelar",

                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7")),
                Foreground = Brushes.White
            };
            Thickness marginCacl = new Thickness
            {
                Right = 20,
                Bottom = 20
            };
            cancel.Margin = marginCacl;

            // Añadimos al grid los elementos.
            grid.Children.Add(icon);
            grid.Children.Add(message);
            grid.Children.Add(accept);
            grid.Children.Add(cancel);

            win.Content = grid; // Asociamos el grid a la ventana y la mostramos.

            // Establecemos la función lambda del botón de aceptar.
            accept.Click += (o, i) =>
            {
                win.Close();
                Acceptance?.Invoke(this, i);
            };

            // Establecemos la función lambda del botón de cancelar.
            cancel.Click += (o, i) =>
            {
                win.Close();
                Cancellation?.Invoke(this, i);
            };
        }

        /// <summary>
        /// Método para mostrar la ventana de advertencia.
        /// </summary>
        public void Show()
        {
            win.Show();
        }

        /// <summary>
        /// Método para cerrar la ventana de advertencia.
        /// </summary>
        public void Close()
        {
            win.Close();
        }
    }
}
