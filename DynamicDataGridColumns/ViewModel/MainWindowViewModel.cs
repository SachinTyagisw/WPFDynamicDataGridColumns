using DynamicDataGridColumns.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DynamicDataGridColumns.Helpers;

namespace DynamicDataGridColumns.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductSaleInfo> _productSales = new ObservableCollection<ProductSaleInfo>();
        public ObservableCollection<ProductSaleInfo> ProductSales
        {
            get { return _productSales; }
            set
            {
                _productSales = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DataGridColumn> dgColumns = new ObservableCollection<DataGridColumn>();

        public ObservableCollection<DataGridColumn> DgColumns
        {
            get
            {
                return dgColumns;
            }
            set
            {
                dgColumns = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            GenerateData();
            GenerateDgColumns();
        }

        private void GenerateData()
        {
            var data = new List<ProductSaleInfo>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new ProductSaleInfo()
                {
                    ProductName = "Product" + i.ToString(),
                    SalesData = new List<SaleInfo>(){new SaleInfo(){Date=DateTime.Now.AddDays(1), SaleNumber = 10 *i }, new SaleInfo() { Date = DateTime.Now.AddDays(2), SaleNumber = 20 * i } },
                    IsDomestic = ((i%2)>0)?true:false
                });
            }

            ProductSales = new ObservableCollection<ProductSaleInfo>(data);
        }

        private void GenerateDgColumns()
        {
            this.DgColumns.Add(
                new DataGridTextColumn { Header = "Product", Binding = new Binding("ProductName") });
            var resourceDictionary = ResourceDictionaryResolver.GetResourceDictionary("Style.xaml");
            var checkBoxColumnStyle = resourceDictionary["CheckBoxColumnStyle"] as Style;
            var binding = new Binding
            {
                //RelativeSource =
                //    new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridCell), 1),
                Path = new PropertyPath("IsDomestic"),
                Mode = BindingMode.TwoWay
            };

            var dataGridCheckBoxColumn = new DataGridCheckBoxColumn
            {
                Header = "Made in India",
                Binding = binding,
                IsThreeState = false,
                CanUserSort = false,
                ElementStyle = checkBoxColumnStyle,
            };

            this.DgColumns.Add(dataGridCheckBoxColumn);
            for (int i = 0; i < 2; i++)
            {
                this.DgColumns.Add(
                    new DataGridTextColumn { Header = ProductSales[0].SalesData[i].Date, Binding = new Binding("SalesData[" + i  +"].SaleNumber") });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProductSaleInfo
    {
        public string ProductName { get; set; }
        public List<SaleInfo> SalesData { get; set; }
        public bool IsDomestic { get; set; }
    }

    public class SaleInfo
    {
        public DateTime Date { get; set; }
        public int SaleNumber { get; set; }
    }
}

