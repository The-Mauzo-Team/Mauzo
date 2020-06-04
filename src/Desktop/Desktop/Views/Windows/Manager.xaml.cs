using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Desktop.Connectors;
using Desktop.Exceptions;
using Desktop.Templates;
using Desktop.Views.Dialogs;
using Microsoft.Win32;

namespace Desktop.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para Manager.xaml
    /// </summary>
    /// <remarks>@Neirth Sergio Martinez</remarks>
    public partial class Manager : Window
    {

        /// <summary>
        /// Constructor que inicializa por defecto los componentes graficos 
        /// a mostrar en esta ventana.
        /// </summary>
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

            ProductIcon.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));
            ProductLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));

            DiscountIcon.Foreground = new SolidColorBrush(Colors.Black);
            DiscountLabel.Foreground = new SolidColorBrush(Colors.Black);

            UserIcon.Foreground = new SolidColorBrush(Colors.Black);
            UserLabel.Foreground = new SolidColorBrush(Colors.Black);

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Botón que cambia el detail al detail de product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Visible;
            DiscountFormView.Visibility = Visibility.Hidden;
            UserFormView.Visibility = Visibility.Hidden;
            InformsFormView.Visibility = Visibility.Hidden;

            ProductIcon.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));
            ProductLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));

            DiscountIcon.Foreground = new SolidColorBrush(Colors.Black);
            DiscountLabel.Foreground = new SolidColorBrush(Colors.Black);

            UserIcon.Foreground = new SolidColorBrush(Colors.Black);
            UserLabel.Foreground = new SolidColorBrush(Colors.Black);

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Botón que cambia el detail al detail de discounts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            ProductFormView.Visibility = Visibility.Hidden;
            DiscountFormView.Visibility = Visibility.Visible;
            UserFormView.Visibility = Visibility.Hidden;
            InformsFormView.Visibility = Visibility.Hidden;

            ProductIcon.Foreground = new SolidColorBrush(Colors.Black);
            ProductLabel.Foreground = new SolidColorBrush(Colors.Black);

            DiscountIcon.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));
            DiscountLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));

            UserIcon.Foreground = new SolidColorBrush(Colors.Black);
            UserLabel.Foreground = new SolidColorBrush(Colors.Black);

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Botón que cambia el detail al detail de usuarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            UserIcon.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));
            UserLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));

            InformIcon.Foreground = new SolidColorBrush(Colors.Black);
            InformLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Botón que cambia el detail al detail de informes.
        /// 
        /// En este caso generará tambien su respectivo informe,
        /// consumiendo así tiempo en el servidor para generarlo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformButton_Click(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Estas a punto de generar un informe, esta operacion va a bloquear la base de datos del servidor hasta que se complete la operación, impidiendo agregar ventas u otros elementos en el momento de generación. ¿Estás seguro de querer ejecutar la operación ahora mismo?", "");
            warning.Show();

            warning.Acceptance += (o, i) =>
            {
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

                InformIcon.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));
                InformLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0078d7"));

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

        /// <summary>
        /// Botón que lanza una petición al servidor para que guarde
        /// el objeto de producto, a traves de la interfaz RESTful.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                ProdName = ProductName.Text,
                ProdCode = ProductCode.Text,
                ProdDesc = ProductDescription.Text,
                ProdPrice = float.Parse(ProductPrize.Text),
                ProdPicArr = BitmapImage2Bitmap(ProdPicLabel.Source)
            };

            try
            {
                ProductsConn productsConn = new ProductsConn();
                productsConn.Add(product);

                Info infoWindow = new Info("Se ha guardado correctamente, en el servidor, el producto " + product.ProdName);
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

        /// <summary>
        /// Botón que lanza una petición al servidor para que guarde
        /// el objeto de documento, a traves de la interfaz RESTful.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void SaveDiscounts_Click(object sender, RoutedEventArgs e)
        {
            Discount discount = new Discount
            {
                Code = CodeText.Text,
                Desc = DiscountText.Text,
                PricePerc = float.Parse(PriceDiscountText.Text)
            };

            try
            {
                DiscountsConn discountsConn = new DiscountsConn();
                discountsConn.Add(discount);

                Info infoWindow = new Info("Se ha guardado correctamente, en el servidor, el descuento " + discount.Code);
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

        /// <summary>
        /// Botón que lanza una petición al servidor para que guarde
        /// el objeto de usuario, a traves de la interfaz RESTful.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                Firstname = FirstnameText.Text,
                Lastname = LastnameText.Text,
                Email = EmailText.Text,
                Password = PasswordText.Password,
                Username = UsernameText.Text,
                IsAdmin = (bool)isAdmin.IsChecked,
                UserPicArr = BitmapImage2Bitmap(UserPicLabel.Source)
            };

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

        /// <summary>
        /// Botón que permite borrar una imagen para el producto.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void ClearImageProduct_Click(object sender, RoutedEventArgs e)
        {
            ProdPicLabel.Source = null;
        }

        /// <summary>
        /// Botón que permite añadir una imagen para el producto.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void LoadImageProduct_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                // Ponemos los filtros que veamos conveniente al tipo de la imagen.
                DefaultExt = ".png",
                Filter = "Archivos JPEG (*.jpeg)|*.jpeg|Archivos PNG (*.png)|*.png|Archivos JPG (*.jpg)|*.jpg|Archivos GIF (*.gif)|*.gif"
            };

            // Abrimos el cuadro de dialogo teniendo en cuenta que puede ser null.
            Nullable<bool> result = dlg.ShowDialog();

            // Recojemos la imagen obtenida y la cargamos a la interfaz grafica.
            if (result == true)
            {
                ProdPicLabel.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        /// <summary>
        /// Botón que permite añadir una imagen para el usuario.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void LoadImageUser_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                // Ponemos los filtros que veamos conveniente al tipo de la imagen.
                DefaultExt = ".png",
                Filter = "Archivos JPEG (*.jpeg)|*.jpeg|Archivos PNG (*.png)|*.png|Archivos JPG (*.jpg)|*.jpg|Archivos GIF (*.gif)|*.gif"
            };

            // Abrimos el cuadro de dialogo teniendo en cuenta que puede ser null.
            Nullable<bool> result = dlg.ShowDialog();

            // Recojemos la imagen obtenida y la cargamos a la interfaz grafica.
            if (result == true)
            {
                UserPicLabel.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        /// <summary>
        /// Botón que permite borrar una imagen para el usuario.
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private void ClearImageUser_Click(object sender, RoutedEventArgs e)
        {
            UserPicLabel.Source = null;
        }

        /// <summary>
        /// Utilidad para convertir un BitmapImage a un Bitmap
        /// </summary>
        /// <param name="sender">Objeto pasado por parametro</param>
        /// <param name="e">Evento pasado por parametro</param>
        private Bitmap BitmapImage2Bitmap(ImageSource image)
        {
            Bitmap result = null;

            if (image != null)
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
                    encoder.Save(outStream);
                    outStream.Flush();

                    result = (Bitmap)Image.FromStream(outStream);
                }
            }

            return result;
        }
    }
}
