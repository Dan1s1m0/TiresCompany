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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TiresCompany.Model;

namespace TiresCompany.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Core db = new Core();
        List<ProductType> productTypes;
        bool reverseType;
        public ProductPage()
        {
            InitializeComponent();
            //сортировка 
            List<string> sotrTypeList = new List<string>()
            {
                "наименование","остаток на складе","стоимость"
            };
            SortComboBox.ItemsSource = sotrTypeList;
            //фильтрация
            productTypes = new List<ProductType>
            {
                new ProductType()
                {
                    ID=0,
                    Title="Все типы"
                }
            };
            productTypes.AddRange(db.context.ProductType.ToList());
            FilterComboBox.ItemsSource = productTypes;
            UpdateUI();
        }

        private void UpdateUI()
        {
            
            ProductListView.ItemsSource = GetRows();
            CountRowsTextBlock.Text = $"{GetRows().Count()} / {db.context.Product.ToList().Count()}";

        }

        private List<Product> GetRows()
        {
            List<Product> arrayProduct = db.context.Product.ToList();
            string searchData = SearchTextBox.Text.ToUpper();
            if (!String.IsNullOrEmpty(SearchTextBox.Text) && SearchTextBox.Text!= "Введите наименование продукта")
            {
                arrayProduct = arrayProduct.Where(x=>x.Title.ToUpper().Contains(searchData)).ToList();
            }
            int filter = Convert.ToInt32(FilterComboBox.SelectedValue);
            if (FilterComboBox.SelectedIndex==0)
            {
                arrayProduct = arrayProduct.ToList();
            }
            else
            {
                arrayProduct = arrayProduct.Where(x => x.ProductTypeID==filter).ToList();
            }
            int value = SortComboBox.SelectedIndex;
            if (value == 0)
            {
                arrayProduct = arrayProduct.OrderBy(p => p.Title).ToList();
            }
            else if (value == 1)
            {
                arrayProduct = arrayProduct.OrderBy(p => p.ProductionWorkshopNumber).ToList();
            }
            else if (value == 2)
            {
                arrayProduct = arrayProduct.OrderBy(p => p.CostProduct).ToList();
            }
            if (reverseType)
            {
                arrayProduct.Reverse();
            }
            return arrayProduct;
        }

        private void SearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUI();
        }

        private void FilterComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void ReverseButtonClick(object sender, RoutedEventArgs e)
        {
            reverseType = !reverseType;
            UpdateUI();
        }

        private void SortComboBoxSelectionChanget(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }
    }
}
