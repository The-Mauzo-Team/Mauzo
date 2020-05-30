using Desktop.Connectors;
using Desktop.Views.Windows;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MainWindow window = null;

        public App()
        {
            Login login = new Login();
            login.Show();

            login.Closing += (o, i) =>
            {
                if (LoginConn.User != null && window == null)
                {
                    window = new MainWindow();
                    window.Show();
                }

            };
        }
    }
}
