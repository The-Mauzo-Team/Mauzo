using System;
using System.Windows;
using Desktop.Views.Dialogs;

namespace Desktop.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void SaveDiscounts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Visible;
            DiscountFormView.Visibility = Visibility.Hidden;
            UserFormView.Visibility = Visibility.Hidden;
            InformsFormView.Visibility = Visibility.Hidden;
        }

        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Hidden;
            DiscountFormView.Visibility = Visibility.Visible;
            UserFormView.Visibility = Visibility.Hidden;
            InformsFormView.Visibility = Visibility.Hidden;
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Hidden;
            DiscountFormView.Visibility = Visibility.Hidden;
            UserFormView.Visibility = Visibility.Visible;
            InformsFormView.Visibility = Visibility.Hidden;
        }

        private void InformButton_Click(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Estas a punto de generar un informe, esta operacion va a bloquear la base de datos del servidor hasta que se complete la operación, impidiendo agregar ventas en el momento de generación.", "¿Estás seguro de querer ejecutar la operación ahora mismo?");

            warning.Acceptance += (o, i) =>
            {
                ProductFormView.Visibility = Visibility.Hidden;
                DiscountFormView.Visibility = Visibility.Hidden;
                UserFormView.Visibility = Visibility.Hidden;
                InformsFormView.Visibility = Visibility.Visible;
            };

            warning.Show();
        }
    }
}
