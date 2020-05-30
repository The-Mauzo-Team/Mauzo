using System;
using System.Windows;
using Desktop.Connectors;
using Desktop.Exceptions;
using Desktop.Templates;
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

        // Botones del master
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
            Warning warning = new Warning("Estas a punto de generar un informe, esta operacion va a bloquear la base de datos del servidor hasta que se complete la operación, impidiendo agregar ventas u otros elementos en el momento de generación.", "¿Estás seguro de querer ejecutar la operación ahora mismo?");

            warning.Acceptance += (o, i) =>
            {
                // TODO: Añadir los datos del informe obtenido a la vista.

                ProductFormView.Visibility = Visibility.Hidden;
                DiscountFormView.Visibility = Visibility.Hidden;
                UserFormView.Visibility = Visibility.Hidden;
                InformsFormView.Visibility = Visibility.Visible;
            };

            warning.Show();
        }

        // Botones del detail correspondientes a guardar elementos.
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Pendiente de hacer la operación con Products.
        }

        private void SaveDiscounts_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Pendiente de hacer la operación con Discounts.
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();

            user.Firstname = FirstnameText.Text;
            user.Lastname = LastnameText.Text;
            user.Email = EmailText.Text;
            user.Password = PasswordText.Password;
            user.Username = UsernameText.Text;
            user.IsAdmin = isAdmin.IsEnabled;

            try
            {
                UsersConn usersConn = new UsersConn();
                usersConn.AddUser(user);

                Info infoWindow = new Info("Se ha guardado correctamente, en el servidor, el usuario " + user.Username);
                infoWindow.Show();
            } 
            catch (AdminForbiddenException ex)
            {
                Error errorWindow = new Error(ex.Message);
                errorWindow.Show();

            } 
            catch (ExpiredLoginException ex) 
            {
                Warning warningWindow = new Warning(ex.Message, "¿Deseas iniciar la sesión?");
                warningWindow.Show();

                warningWindow.Acceptance += (o, i) =>
                {
                    warningWindow.Close();

                    Login loginWindow = new Login();
                    loginWindow.Show();
                };

                warningWindow.Cancellation += (o, i) =>
                {
                    ManagerWindow.Close();
                };
            }
            catch (Exception ex)
            {
                Error errorWindow = new Error(ex.Message);
                errorWindow.Show();
            }

        }
    }
}
