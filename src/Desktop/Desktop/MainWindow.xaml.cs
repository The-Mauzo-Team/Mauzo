using Desktop.Templates;
using Desktop.Views.Dialogs;
using Desktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        Dictionary<ContentPresenter, ContentPresenter> history = new Dictionary<ContentPresenter, ContentPresenter>();
        ContentPresenter selConForm;

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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void Button_New(object sender, RoutedEventArgs e)
        {
            // Se crea el elemento de menú.
            ContentPresenter conItem = new ContentPresenter();
            conItem.Content = "Nueva Venta";
            conItem.ContentTemplate = (DataTemplate)Resources["EditMenuItem"];

            // Se crea su formulario
            ContentPresenter conForm = new ContentPresenter();
            conForm.ContentTemplate = (DataTemplate)Resources["SaleForm"];

            ActivityList.Items.Add(conItem); // Añadir al menú el elemento.
            history.Add(conItem, conForm); // Añadir elemento de menú y formulario al diccionario.

            // Cambiar selección al último
            int position = ActivityList.Items.IndexOf(conItem);
            ActivityList.SelectedItem = ActivityList.Items.GetItemAt(position);

            FormGrid.Children.Clear();
            FormGrid.Children.Add(selConForm);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú.
            ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.

            selConForm = conForm; // Cambiar formulario por el seleccionado.

            FormGrid.Children.Clear();
            FormGrid.Children.Add(selConForm);
        }

        private void ContextRefund_Click(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Se va a proceder a devolver la venta seleccionada.", "¿Estás seguro de que deseas continuar?");
            warning.Show();

            warning.Acceptance += (o, i) =>
            {
                ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú.
                conAux.ContentTemplate = (DataTemplate)Resources["ReturnMenuItem"]; // Modificar a devolución.

                ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.
                conForm.IsEnabled = false; // Cambiar estado del formulario.
            };
        }

        private void ContextEdit_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú
            conAux.ContentTemplate = (DataTemplate)Resources["EditMenuItem"]; // Cambiar tipo visual

            ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.
            conForm.IsEnabled = true; // Cambiar estado del formulario.
        }

        private void SaleButton(object sender, RoutedEventArgs e)
        {
            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú
            conAux.ContentTemplate = (DataTemplate)Resources["SaleMenuItem"]; // Cambiar tipo visual

            ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.
            conForm.IsEnabled = false; // Cambiar estado del formulario.
        }
    }
}
