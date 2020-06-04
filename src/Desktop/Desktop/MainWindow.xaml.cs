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
    /// <remarks>@ant04x Antonio Izquierdo</remarks>
    public partial class MainWindow : Window
    {
        Dictionary<ContentPresenter, ContentPresenter> history = new Dictionary<ContentPresenter, ContentPresenter>();
        Dictionary<ContentPresenter, Product> relationProd = new Dictionary<ContentPresenter, Product>();
        Dictionary<ContentPresenter, Discount> relationDisc = new Dictionary<ContentPresenter, Discount>();

        ContentPresenter selConForm;
        Product selProd;
        Discount selDisc;

        /// <summary>
        /// Constructor que inicializa los componentes gráficos.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Update();
        }

        /// <summary>
        /// Acción al dar al botón de configuración.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            manager.Show();
        }

        /// <summary>
        /// Acción al dar al botón de actualizar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Update(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Se va a sincronizar las ventas. Se perderán las ventas no guardadas.", "¿Estás seguro de que deseas continuar?");
            warning.Show();

            warning.Acceptance += (o, i) =>
            {
                Update();
            };
        }

        /// <summary>
        /// Acción al obtener foco un cuadro de texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        /// <summary>
        /// Acción al dar al botón de nuevo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_New(object sender, RoutedEventArgs e)
        {
            // Se crea el elemento de menú.
            ContentPresenter conItem = new ContentPresenter();
            conItem.Content = "Nueva Venta";
            conItem.ContentTemplate = (DataTemplate)Resources["EditMenuItem"];

            // Si lo último es una edición, reemplazar.
            ContentPresenter tmpCP = (ActivityList.Items[ActivityList.Items.Count - 1] as ContentPresenter);
            if (tmpCP.ContentTemplate == (DataTemplate)Resources["EditMenuItem"])
            {
                Warning warning = new Warning("Se va a perder la venta anterior sin guardar.", "¿Estás seguro de que deseas continuar?");
                warning.Show();
                warning.Acceptance += (o, i) =>
                {
                    history[tmpCP] = new ContentPresenter
                    {
                        ContentTemplate = (DataTemplate)Resources["SaleForm"]
                    };

                    FormGrid.Children.Clear();
                    FormGrid.Children.Add(history[tmpCP]);

                    relationProd[tmpCP] = null;
                    relationDisc[tmpCP] = null;

                    // Se actualiza el producto y el descuento seleccionado.
                    selProd = relationProd[tmpCP];
                    selDisc = relationDisc[tmpCP];
                };
                return;
            }

            // Se crea su formulario
            ContentPresenter conForm = new ContentPresenter();
            conForm.ContentTemplate = (DataTemplate)Resources["SaleForm"];

            ActivityList.Items.Add(conItem); // Añadir al menú el elemento.
            history.Add(conItem, conForm); // Añadir elemento de menú y formulario al diccionario.
            
            // Crear valores del producto y descuento.
            relationProd.Add(conItem, null);
            relationDisc.Add(conItem, null);

            // Cambiar selección al último
            int position = ActivityList.Items.IndexOf(conItem);
            ActivityList.SelectedItem = ActivityList.Items.GetItemAt(position);

            FormGrid.Children.Clear();
            FormGrid.Children.Add(selConForm);
        }

        /// <summary>
        /// Acción al cambiar de venta en el menú master.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si se ha limpiado las ventas.
            if (ActivityList.Items.Count == 0)
            {
                return;
            }

            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú.
            ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.

            // Se actualiza el producto y el descuento seleccionado.
            selProd = relationProd[conAux];
            selDisc = relationDisc[conAux];

            selConForm = conForm; // Cambiar formulario por el seleccionado.

            FormGrid.Children.Clear();
            FormGrid.Children.Add(selConForm);
        }

        /// <summary>
        /// Acción al dar a devolver en una venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextRefund_Click(object sender, RoutedEventArgs e)
        {
            Warning warning = new Warning("Se va a proceder a devolver la venta seleccionada.", "¿Estás seguro de que deseas continuar?");
            warning.Show();

            warning.Acceptance += (o, i) =>
            {
                ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú.

                // INICIO DE ENVIO

                // Se crea la conexión Sale
                RefundsConn rc = new RefundsConn();

                // Se crea el Sale
                Refund refund = new Refund();

                DateTime dateTime = DateTime.Now;
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                long unixDateTime = (long)(dateTime.ToUniversalTime() - epoch).TotalSeconds;

                refund.SaleId = int.Parse((ActivityList.SelectedItem as ContentPresenter).Content.ToString());
                refund.DateRefund = unixDateTime;
                refund.UserId = LoginConn.User.Id;

                try
                {
                    rc.Add(refund);
                }
                catch (Exception ex)
                {
                    Error error = new Error("Devolución no admitida. " + ex.Message);
                    error.Show();
                    return;
                }

                // FIN DE ENVIO

                (ActivityList.SelectedItem as ContentPresenter).Content = "Devolución realizada a las " + DateTime.Now.ToString("HH:mm"); // Establecer id de venta temporal.

                // TODO
                Update();

                /*
                conAux.ContentTemplate = (DataTemplate)Resources["ReturnMenuItem"]; // Modificar a devolución.
                ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.
                conForm.IsEnabled = false; // Cambiar estado del formulario.

                // Se actualiza el producto y el descuento seleccionado.
                selProd = relationProd[conAux];
                selDisc = relationDisc[conAux];
                */
            };
        }

        /// <summary>
        /// Acción al dar a editar en una venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextEdit_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú
            conAux.ContentTemplate = (DataTemplate)Resources["EditMenuItem"]; // Cambiar tipo visual

            ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.
            conForm.IsEnabled = true; // Cambiar estado del formulario.

            // Se actualiza el producto y el descuento seleccionado.
            selProd = relationProd[conAux];
            selDisc = relationDisc[conAux];
        }

        /// <summary>
        /// Acción al dar a vender en una venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaleButton(object sender, RoutedEventArgs e)
        {
            ContentPresenter conAux = (ActivityList.SelectedItem as ContentPresenter); // Obtener elemento de menú

            // Se actualiza el producto y el descuento seleccionado.
            selProd = relationProd[conAux];
            selDisc = relationDisc[conAux];

            if (selProd == null)
            {
                Error error = new Error("Se debe introducir un producto por venta.");
                error.Show();
                return;
            }

            // Se crea la conexión Sale
            SalesConn sc = new SalesConn();

            // Se crea el Sale
            Sale sale = new Sale();

            DateTime dateTime = DateTime.Now;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long unixDateTime = (long)(dateTime.ToUniversalTime() - epoch).TotalSeconds;

            sale.StampRef = unixDateTime;

            sale.ProdId = selProd.Id;
            if (selDisc != null)
                sale.DiscId = selDisc.Id;
            sale.UserId = LoginConn.User.Id;

            (ActivityList.SelectedItem as ContentPresenter).Content = "Venta realizada a las " + DateTime.Now.ToString("HH:mm"); ; // Establecer id de venta temporal.

            try
            {
                sc.Add(sale);
            } catch (Exception ex)
            {
                Error error = new Error("Venta no admitida. " + ex.Message);
                error.Show();
                return;
            }

            Update();
            ActivityList.SelectedItem = ActivityList.Items[ActivityList.Items.Count - 1];

            /*
            conAux.ContentTemplate = (DataTemplate)Resources["SaleMenuItem"]; // Cambiar tipo visual

            ContentPresenter conForm = history[conAux]; // Obtener formulario de elemento de menú.
            conForm.IsEnabled = false; // Cambiar estado del formulario.
            */
        }

        /// <summary>
        /// Acción al añadir un producto en una venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                relationProd[(ActivityList.SelectedItem as ContentPresenter)] = selProd;
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
                discounting = selDisc.PricePerc;

            float result = paying - (paying * discounting / 100);
            (auxDT.FindName("TotalCost", (ContentPresenter)FormGrid.Children[0]) as TextBlock).Text = "Total: " + result + "€";
        }

        /// <summary>
        /// Acción al añadir un descuento en una venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                relationDisc[(ActivityList.SelectedItem as ContentPresenter)] = selDisc;
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

        /// <summary>
        /// Acción al cambiar la busqueda de una venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = (sender as TextBox).Text;

            // Saltar en caso de que sea el primer cambio (al establecer el valor por defecto.)
            if (ActivityList == null)
                return;

            int max = history.Count;

            ContentPresenter selCP;
            // Si no hay nada mostrar todo y salir.
            if (search.Length == 0)
            {
                ActivityList.Items.Clear();
                for (int i = 0; i < max; i++)
                {
                    ActivityList.Items.Add(history.ElementAt(i).Key);
                }
                return;
            }

            ActivityList.Items.Clear();
            for (int i = 0; i < max; i++)
            {
                selCP = (history.ElementAt(i).Key);

                if (selCP.Content.Equals(search))
                {
                    ActivityList.Items.Add(selCP);
                }
            }
        }

        private void Update()
        {
            ActivityList.Items.Clear();

            SalesConn sc = new SalesConn();
            RefundsConn rc = new RefundsConn();

            ProductsConn pc = new ProductsConn();
            DiscountsConn dc = new DiscountsConn();

            List<Sale> sales = sc.List;
            List<Refund> refunds = rc.List;

            history.Clear();
            relationProd.Clear();
            relationDisc.Clear();

            // Crear elementos.
            Sale selSale;
            Product selProduct;
            Discount selDiscount;

            // Recuperar todas las ventas.
            for (int j = 0; j < sales.Count; j++)
            {
                ContentPresenter conItem = new ContentPresenter();
                conItem.Content = sales[j].Id.ToString();

                conItem.ContentTemplate = (DataTemplate)Resources["SaleMenuItem"];

                int max = refunds.Count;

                // Si coincide con alguna devolución convertir elemento de menu en Return.
                for (int k = 0; k < max; k++)
                    if (sales[j].Id == refunds[k].SaleId)
                        conItem.ContentTemplate = (DataTemplate)Resources["ReturnMenuItem"];

                // Añadimos el elemento.
                ActivityList.Items.Add(conItem);

                // Obtener Sale, Product y Discount.
                selSale = sales[j];

                try
                {
                    selProduct = pc.Get(selSale.ProdId.Value);
                }
                catch
                {
                    selProduct = null;
                }

                try
                {
                    selDiscount = dc.Get(selSale.DiscId.Value);
                }
                catch
                {
                    selDiscount = null;
                }

                // Se crea su formulario
                ContentPresenter conForm = new ContentPresenter();
                conForm.IsEnabled = false;
                conForm.ContentTemplate = (DataTemplate)Resources["SaleForm"];
                conForm.ApplyTemplate();

                // Obtener contenedor.
                DataTemplate auxDT = conForm.ContentTemplate;

                // Obtener elementos de la GUI
                StackPanel products = (StackPanel)auxDT.FindName("Products", conForm);
                StackPanel discounts = (StackPanel)auxDT.FindName("Discounts", conForm);

                float paying = 0;
                // Agregar producto
                if (selSale.ProdId != null && selSale.ProdId != 0)
                {
                    ContentPresenter productItem = new ContentPresenter();
                    productItem.Content = selSale.ProdId;
                    productItem.ContentTemplate = (DataTemplate)Resources["ProductItem"];

                    products.Children.Clear();
                    products.Children.Add(productItem);

                    Product tmpProduct = pc.Get(int.Parse(productItem.Content.ToString()));
                    paying = tmpProduct.ProdPrice;
                }

                float discounting = 0;
                // Agregar descuento
                if (selSale.DiscId != null && selSale.DiscId != 0)
                {
                    ContentPresenter discountItem = new ContentPresenter();
                    discountItem.Content = selSale.DiscId;
                    discountItem.ContentTemplate = (DataTemplate)Resources["DiscountItem"];

                    discounts.Children.Clear();
                    discounts.Children.Add(discountItem);

                    Discount tmpDiscount = dc.Get(int.Parse(discountItem.Content.ToString()));
                    discounting = tmpDiscount.PricePerc;

                    (auxDT.FindName("Discounted", conForm) as TextBox).Text = discounting.ToString();
                }

                // INSERTAR TODOS LOS DATOS NUMERICOS

                float result = paying - (paying * discounting / 100);
                (auxDT.FindName("TotalCost", conForm) as TextBlock).Text = "Total: " + result + "€";

                // Dar de alta en History y Relations
                history.Add(conItem, conForm);
                relationProd.Add(conItem, selProduct);
                relationDisc.Add(conItem, selDiscount);
            }
        }
    }
}
