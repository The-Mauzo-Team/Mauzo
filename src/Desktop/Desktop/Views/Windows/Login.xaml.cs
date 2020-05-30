using Desktop.Connectors;
using Desktop.Exceptions;
using Desktop.Templates;
using Desktop.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            //Centramos la pantalla
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Username = UserBox.Text;
            user.Password = PasswordBox.Password;
            try
            {
                //Registramos el usuario
                LoginConn loginConn = new LoginConn();
                loginConn.LoginUser(user);
                this.Close();
            }
            catch (ExpiredLoginException ex)
            {
                //En caso de que haya expirado lanzamos una excepción y mostramos la ventana de error
                Error error = new Error(ex.Message);
                error.Show();
            } 
            catch(ServerException ex)
            {
                //En caso de que haya habido problemas con el servidor lanzamos una excepción y mostramos la ventana de error
                Error error = new Error(ex.Message);
                error.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Al darle a cancelar cerramos la ventana
            this.Close();
        }
    }
}
