using Desktop.Templates;
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
        Dictionary<Sale, bool> history = new Dictionary<Sale, bool>();
        ContentPresenter conForm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            manager.Show();
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            manager.Show();
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            ContentPresenter conItem = new ContentPresenter();
            conItem.Content = "Nueva Venta";
            conItem.ContentTemplate = (DataTemplate)Resources["EditMenuItem"];

            ActivityList.Items.Add(conItem);

            // Cambiar selección.
            int lastPosition = ActivityList.Items.Count - 1;
            ActivityList.SelectedItem = ActivityList.Items.GetItemAt(lastPosition);

            // Cambiar formulario
            FormGrid.Children.Remove(conForm);
            conForm = new ContentPresenter();
            conForm.ContentTemplate = (DataTemplate)Resources["SaleForm"];
            FormGrid.Children.Add(conForm);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Cambiar formulario
            FormGrid.Children.Remove(conForm);
            conForm = new ContentPresenter();
            conForm.ContentTemplate = (DataTemplate)Resources["SaleForm"];

            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter);

            if (conAux.ContentTemplate != (DataTemplate)Resources["EditMenuItem"]) // TODO
                conForm.IsEnabled = false; // Al cambiar de vista nunca deja modificar.

            FormGrid.Children.Add(conForm);
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

                // Cambiar formulario.
                FormGrid.Children.Remove(conForm);
                conForm = new ContentPresenter
                {
                    IsEnabled = false,
                    ContentTemplate = (DataTemplate)Resources["SaleForm"]
                };
                FormGrid.Children.Add(conForm);
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

        private void SaleButton(object sender, RoutedEventArgs e)
        {
            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter);

            conAux.ContentTemplate = (DataTemplate)Resources["SaleMenuItem"];
            Console.WriteLine("Antiguo elemento seleccionado: " + conAux.Content);

            // Clonando el objeto original.
            string cloneXaml = XamlWriter.Save(conAux);
            StringReader stringReader = new StringReader(cloneXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            ContentPresenter newContent = (ContentPresenter)XamlReader.Load(xmlReader);

            Console.WriteLine("Elemento clonado: " + newContent.Content);
            ActivityList.Items.Add(newContent);

            // Cambiar selección.
            int lastPosition = ActivityList.Items.Count - 1;
            ActivityList.SelectedItem = ActivityList.Items.GetItemAt(lastPosition);

            ActivityList.Items.Remove(conAux);
        }
    }
}
