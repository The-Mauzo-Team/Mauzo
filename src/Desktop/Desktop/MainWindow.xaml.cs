using Desktop.Views.Dialogs;
using Desktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Desktop
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            manager.Show();
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            ContentPresenter content = new ContentPresenter();
            content.Content = "Nueva Venta";
            content.ContentTemplate = (DataTemplate)Resources["SaleMenuItem"];

            ActivityList.Items.Add(content);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Mostrar formulario correspondiente.
        }

        private void ContextRefund_Click(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Se va a proceder a devolver la venta seleccionada.", "¿Estás seguro de que deseas continuar?");
            warning.Show();

            warning.Acceptance += (o, i) =>
            {
                // Clonando el objeto original.
                string cloneXaml = XamlWriter.Save(ActivityList.SelectedItem);
                StringReader stringReader = new StringReader(cloneXaml);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                ContentPresenter newContent = (ContentPresenter)XamlReader.Load(xmlReader);

                newContent.ContentTemplate = (DataTemplate)Resources["ReturnMenuItem"];

                ActivityList.Items.Add(newContent);

                // Eliminar origen.
                ActivityList.Items.Remove(ActivityList.SelectedItem);

                int lastPosition = ActivityList.Items.Count - 1;
                ActivityList.SelectedItem = ActivityList.Items.GetItemAt(lastPosition);
            };
        }

        private void ContextEdit_Click(object sender, RoutedEventArgs e)
        {
            // Clonando el objeto original.
            string cloneXaml = XamlWriter.Save(ActivityList.SelectedItem);
            StringReader stringReader = new StringReader(cloneXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            ContentPresenter newContent = (ContentPresenter)XamlReader.Load(xmlReader);

            newContent.ContentTemplate = (DataTemplate)Resources["EditMenuItem"];

            // Se añade el elemento y se selecciona.
            ActivityList.Items.Add(newContent);

            // Eliminar origen.
            ActivityList.Items.Remove(ActivityList.SelectedItem);

            int lastPosition = ActivityList.Items.Count - 1;
            ActivityList.SelectedItem = ActivityList.Items.GetItemAt(lastPosition);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
