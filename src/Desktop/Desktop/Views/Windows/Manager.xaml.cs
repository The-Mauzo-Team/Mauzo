using System;
using System.Windows;
using System.Windows.Media;
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

            // En caso de que el usuario no sea administrador, bloqueamos posibles opciones.
            if (!LoginConn.User.IsAdmin) 
            {
                ProductButton.IsEnabled = true;
                DiscountButton.IsEnabled = false;
                UserButton.IsEnabled = false;
                InformButton.IsEnabled = false;
            }

            ProductIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
            ProductLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));

            DiscountIcon.Foreground = new SolidColorBrush(Colors.Black);
            DiscountLabel.Foreground = new SolidColorBrush(Colors.Black);

            UserIcon.Foreground = new SolidColorBrush(Colors.Black);
            UserLabel.Foreground = new SolidColorBrush(Colors.Black);

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        // Botones del master
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Visible;
            DiscountFormView.Visibility = Visibility.Hidden;
            UserFormView.Visibility = Visibility.Hidden;
            InformsFormView.Visibility = Visibility.Hidden;

            ProductIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
            ProductLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));

            DiscountIcon.Foreground = new SolidColorBrush(Colors.Black);
            DiscountLabel.Foreground = new SolidColorBrush(Colors.Black);

            UserIcon.Foreground = new SolidColorBrush(Colors.Black);
            UserLabel.Foreground = new SolidColorBrush(Colors.Black);

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Hidden;
            DiscountFormView.Visibility = Visibility.Visible;
            UserFormView.Visibility = Visibility.Hidden;
            InformsFormView.Visibility = Visibility.Hidden;

            ProductIcon.Foreground = new SolidColorBrush(Colors.Black);
            ProductLabel.Foreground = new SolidColorBrush(Colors.Black);

            DiscountIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
            DiscountLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));

            UserIcon.Foreground = new SolidColorBrush(Colors.Black);
            UserLabel.Foreground = new SolidColorBrush(Colors.Black);

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Hidden;
            DiscountFormView.Visibility = Visibility.Hidden;
            UserFormView.Visibility = Visibility.Visible;
            InformsFormView.Visibility = Visibility.Hidden;

            ProductIcon.Foreground = new SolidColorBrush(Colors.Black);
            ProductLabel.Foreground = new SolidColorBrush(Colors.Black);

            DiscountIcon.Foreground = new SolidColorBrush(Colors.Black);
            DiscountLabel.Foreground = new SolidColorBrush(Colors.Black);

            UserIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
            UserLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void InformButton_Click(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Estas a punto de generar un informe, esta operacion va a bloquear la base de datos del servidor hasta que se complete la operación, impidiendo agregar ventas u otros elementos en el momento de generación. ¿Estás seguro de querer ejecutar la operación ahora mismo?", "");

            warning.Acceptance += (o, i) =>
            {
                // TODO: Añadir los datos del informe obtenido a la vista.
                ProductFormView.Visibility = Visibility.Hidden;
                DiscountFormView.Visibility = Visibility.Hidden;
                UserFormView.Visibility = Visibility.Hidden;
                InformsFormView.Visibility = Visibility.Visible;

                ProductIcon.Foreground = new SolidColorBrush(Colors.Black);
                ProductLabel.Foreground = new SolidColorBrush(Colors.Black);

                DiscountIcon.Foreground = new SolidColorBrush(Colors.Black);
                DiscountLabel.Foreground = new SolidColorBrush(Colors.Black);

                UserIcon.Foreground = new SolidColorBrush(Colors.Black);
                UserLabel.Foreground = new SolidColorBrush(Colors.Black);

                InformIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
                InformLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
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
            Discount discount = new Discount();

            discount.Code = CodeText.Text;
            discount.Desc = DiscountText.Text;
            discount.PriceDisc = float.Parse(PriceDiscountText.Text);

            try
            {
                DiscountsConn discountsConn = new DiscountsConn();
                discountsConn.Add(discount);

                Info infoWindow = new Info("Se ha guardado correctamente, en el servidor, el descuento" + discount.Code);
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
            catch (ServerException ex)
            {
                Error errorWindow = new Error(ex.Message);
                errorWindow.Show();
            }
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();

            user.Firstname = FirstnameText.Text;
            user.Lastname = LastnameText.Text;
            user.Email = EmailText.Text;
            user.Password = PasswordText.Password;
            user.Username = UsernameText.Text;
            user.IsAdmin = (bool) isAdmin.IsChecked;

            try
            {
                UsersConn usersConn = new UsersConn();
                usersConn.Add(user);

                Info infoWindow = new Info("Se ha guardado correctamente, en el servidor, el usuario " + user.Username);
                infoWindow.Show();
            } 
            catch (AdminForbiddenException ex)
            {
                // En caso de que no tenga privilegios el usuario, se le informa de ello.
                Error errorWindow = new Error(ex.Message);
                errorWindow.Show();

            } 
            catch (ExpiredLoginException ex) 
            {
                // En el caso de que la sesión haya caducado, mostramos un cuadro de dialogo indicando la posibilidad de iniciar sesión de nuevo.
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
            catch (ServerException ex)
            {
                // En caso de que el servidor tenga algún tipo de error, se lo mostramos por pantalla.
                Error errorWindow = new Error(ex.Message);
                errorWindow.Show();
            }

        }
    }
}
