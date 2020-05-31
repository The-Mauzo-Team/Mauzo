using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
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
            warning.Show();

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

                // Lanzamos los datos a la vista, para que las dibuje con barras.
                List<Inform> informs = new InformsConn().List;
                ((BarSeries)mcChart.Series[0]).ItemsSource = new KeyValuePair<string, int>[] {
                    new KeyValuePair<string, int>("Diciembre", informs[11].NSales),
                    new KeyValuePair<string, int>("Noviembre", informs[10].NSales),
                    new KeyValuePair<string, int>("Octubre", informs[9].NSales),
                    new KeyValuePair<string, int>("Septiembre", informs[8].NSales),
                    new KeyValuePair<string, int>("Agosto", informs[7].NSales),
                    new KeyValuePair<string, int>("Julio", informs[6].NSales),
                    new KeyValuePair<string, int>("Junio", informs[5].NSales),
                    new KeyValuePair<string, int>("Mayo", informs[4].NSales),
                    new KeyValuePair<string, int>("Abril", informs[3].NSales),
                    new KeyValuePair<string, int>("Marzo", informs[2].NSales),
                    new KeyValuePair<string, int>("Febrero", informs[1].NSales),
                    new KeyValuePair<string, int>("Enero", informs[0].NSales)
                };

                ((BarSeries)mcChart.Series[1]).ItemsSource = new KeyValuePair<string, int>[] {                  
                    new KeyValuePair<string, int>("Diciembre", informs[11].NDiscounts),
                    new KeyValuePair<string, int>("Noviembre", informs[10].NDiscounts),
                    new KeyValuePair<string, int>("Octubre", informs[9].NDiscounts),
                    new KeyValuePair<string, int>("Septiembre", informs[8].NDiscounts),
                    new KeyValuePair<string, int>("Agosto", informs[7].NDiscounts),
                    new KeyValuePair<string, int>("Julio", informs[6].NDiscounts),
                    new KeyValuePair<string, int>("Junio", informs[5].NDiscounts),
                    new KeyValuePair<string, int>("Mayo", informs[4].NDiscounts),
                    new KeyValuePair<string, int>("Abril", informs[3].NDiscounts),
                    new KeyValuePair<string, int>("Marzo", informs[2].NDiscounts),
                    new KeyValuePair<string, int>("Febrero", informs[1].NDiscounts),
                    new KeyValuePair<string, int>("Enero", informs[0].NDiscounts)
                };

                ((BarSeries)mcChart.Series[2]).ItemsSource = new KeyValuePair<string, int>[] {
                    new KeyValuePair<string, int>("Diciembre", informs[11].NRefunds),
                    new KeyValuePair<string, int>("Noviembre", informs[10].NRefunds),
                    new KeyValuePair<string, int>("Octubre", informs[9].NRefunds),
                    new KeyValuePair<string, int>("Septiembre", informs[8].NRefunds),
                    new KeyValuePair<string, int>("Agosto", informs[7].NRefunds),
                    new KeyValuePair<string, int>("Julio", informs[6].NRefunds),
                    new KeyValuePair<string, int>("Junio", informs[5].NRefunds),
                    new KeyValuePair<string, int>("Mayo", informs[4].NRefunds),
                    new KeyValuePair<string, int>("Abril", informs[3].NRefunds),
                    new KeyValuePair<string, int>("Marzo", informs[2].NRefunds),
                    new KeyValuePair<string, int>("Febrero", informs[1].NRefunds),
                    new KeyValuePair<string, int>("Enero", informs[0].NRefunds)
                };
            };
        }

        // Botones del detail correspondientes a guardar elementos.
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();

            product.ProdName = ProductName.Text;
            product.ProdCode = ProductCode.Text;
            product.ProdDesc = ProductDescription.Text;
            product.ProdPrice = float.Parse(ProductPrize.Text);

            try
            {
                ProductsConn productsConn = new ProductsConn();
                productsConn.Add(product);

                Info infoWindow = new Info("Se ha guardado correctamente, en el servidor, el producto" + product.ProdName);
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
