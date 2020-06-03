using Desktop.Connectors;
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
        Product selProd;
        Discount selDisc;

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
            Warning warning = new Warning("Se va a sincronizar las ventas. Se perderán las ventas no guardadas.", "¿Estás seguro de que deseas continuar?");
            warning.Show();

            warning.Acceptance += (o, i) =>
            {
                ActivityList.Items.Clear();

                SalesConn sc = new SalesConn();
                RefundsConn rc = new RefundsConn();

                // Recuperar todas las ventas.
                for (int j = 0; j < sc.List.Count; j++)
                {
                    ContentPresenter conItem = new ContentPresenter();
                    conItem.Content = sc.List[j].Id;

                    conItem.ContentTemplate = (DataTemplate)Resources["SaleMenuItem"];

                    int max = rc.List.Count;

                    // Si coincide con alguna devolución convertir elemento de menu en Return.
                    for (int k = 0; k < max; k++)
                        if (sc.List[j].Id == rc.List[k].Id)
                            conItem.ContentTemplate = (DataTemplate)Resources["ReturnMenuItem"];
                }
            };
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
            // Si se ha limpiado las ventas.
            if (ActivityList.Items.Count == 0)
            {
                history.Clear();
                FormGrid.Children.Clear();
                return;
            }

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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Obtener contenedor.
            DataTemplate auxDT = (FormGrid.Children[0] as ContentPresenter).ContentTemplate;

            // Obtener elementos de la GUI
            TextBox searchProducts = (TextBox)auxDT.FindName("ProductSearch", (ContentPresenter)FormGrid.Children[0]);
            StackPanel productsSP = (StackPanel)auxDT.FindName("Products", (ContentPresenter)FormGrid.Children[0]);

            // Crear variable y conexión para el producto.
            ProductsConn pc = new ProductsConn();
            // Product product;

            try
            {
                selProd = pc.Get(int.Parse(searchProducts.Text)); // Intentar buscar el ID transformado.
                if (selProd == null)
                    throw new Exception();
            } catch
            {
                Error error = new Error("El ID del producto introducido no existe.");
                error.Show(); // Si da algún error mostrar que ha fallado.
                return;
            }

            // Crear y añadir elemento a la lista visual.
            ContentPresenter productItem = new ContentPresenter();
            productItem.ContentTemplate = (DataTemplate)Resources["ProductItem"];
            productItem.Content = searchProducts.Text;
            productsSP.Children.Clear(); // Solo se permite uno, limpiar antes siempre.
            productsSP.Children.Add(productItem);

            // Refrescar contador de Total.

            float paying = selProd.ProdPrice;

            // Si es null Product ser 0.
            float discounting;
            if (selDisc == null)
                discounting = 0;
            else
                discounting = selProd.ProdPrice;

            float result = paying - (paying * discounting / 100);
            (auxDT.FindName("TotalCost", (ContentPresenter)FormGrid.Children[0]) as TextBlock).Text = "Total: " + result + "€";
        }

        private void AddDiscount_Click(object sender, RoutedEventArgs e)
        {
            // Obtener contenedor.
            DataTemplate auxDT = (FormGrid.Children[0] as ContentPresenter).ContentTemplate;

            // Obtener elementos de la GUI
            TextBox searchDiscounts = (TextBox)auxDT.FindName("DiscountSearch", (ContentPresenter)FormGrid.Children[0]);
            StackPanel discountSP = (StackPanel)auxDT.FindName("Discounts", (ContentPresenter)FormGrid.Children[0]);

            // Crear variable y conexión para el descuento.
            DiscountsConn dc = new DiscountsConn();
            // Discount discount;

            try
            {
                selDisc = dc.Get(int.Parse(searchDiscounts.Text)); // Intentar buscar el ID transformado.
                if (selDisc == null)
                    throw new Exception();
            }
            catch
            {
                Error error = new Error("El ID del descuento introducido no esta aceptado.");
                error.Show(); // Si da algún error mostrar que ha fallado.
                return;
            }

            // Crear y añadir elemento a la lista visual.
            ContentPresenter discountItem = new ContentPresenter();
            discountItem.ContentTemplate = (DataTemplate)Resources["DiscountItem"];
            discountItem.Content = searchDiscounts.Text;
            discountSP.Children.Clear(); // Solo se permite uno, limpiar antes siempre.
            discountSP.Children.Add(discountItem);

            // Refrescar contador de Total.

            // Si es null Product ser 0.
            float paying;
            if (selProd == null)
                paying = 0;
            else
                paying = selProd.ProdPrice;

            float discounting = selDisc.PricePerc;

            float result = paying - (paying * discounting / 100);

            (auxDT.FindName("Discounted", (ContentPresenter)FormGrid.Children[0]) as TextBox).Text = discounting.ToString();
            (auxDT.FindName("TotalCost", (ContentPresenter)FormGrid.Children[0]) as TextBlock).Text = "Total: " + result + "€";
        }

        private void SalesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = (sender as TextBox).Text;

            // Saltar en caso de que sea el primer cambio (al establecer el valor por defecto.)
            if (ActivityList == null)
                return;

            int max = ActivityList.Items.Count;

            ContentPresenter selCP;
            // Si no hay nada mostrar todo y salir.
            if (search.Length == 0)
            {
                for (int i = 0; i < max; i++)
                {
                    selCP = (ActivityList.Items[i] as ContentPresenter);
                    selCP.Visibility = Visibility.Visible;
                }
                return;
            }

            for (int i = 0; i < max; i++)
            {
                selCP = (ActivityList.Items[i] as ContentPresenter);
                if (! selCP.Content.Equals(search))
                {
                    selCP.Visibility = Visibility.Hidden; // Ocultar elemento si no coincide.
                    // ActivityList.Items.Remove(selCP);
                } else
                {
                    selCP.Visibility = Visibility.Visible; // Mostrar si coincide.
                    // ActivityList.Items.Add(history.Keys.ToList().);
                }
            }
        }
    }
}
