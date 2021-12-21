using Dapper.DAL.Conxtext;
using Dapper.Entities;
using Dapper.Units.Abstract;
using Dapper.Units.Concrete;
using Microsoft.EntityFrameworkCore;
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

namespace Dapper.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        IUnitWork _unitWork;
        public MainView()
        {
            InitializeComponent();
            _unitWork = new UnitWork(new ProductContext());
            dataGridCategory.ItemsSource = _unitWork.CategoryRepository.GetAll().Include(c => c.Products).ToList();
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            ProcessView w2 = new ProcessView();
            w2.btnCat.Content = "Add";
            w2.gbxCategory.Visibility = Visibility.Visible;
            w2.ShowDialog();
        }

        private void BtnRefreshCategory_Click(object sender, RoutedEventArgs e)
        {
            //dataGridCategory.ItemsSource = null;
            //dataGridCategory.ItemsSource = _unitWork.CategoryRepository.GetAll().Include(c => c.Products).ToList();
            dataGridCategory.Items.Refresh();
        }

        private void BtnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCategory.SelectedIndex >= 0)
            {
                 var category = dataGridCategory.SelectedItem as Category;
                if (category != null)
                {
                    _unitWork.CategoryRepository.Remove(category);
                    _unitWork.Complete();
                }
                dataGridCategory.ItemsSource = null;
                dataGridCategory.ItemsSource = _unitWork.CategoryRepository.GetAll().Include(c => c.Products).ToList();
            }
            else
                MessageBox.Show("Error!");

        }

        private void BtnUpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCategory.SelectedIndex != -1)
            {
                var category = dataGridCategory.SelectedItem as Category;
                ProcessView w2 = new ProcessView();
                w2.btnCat.Content = "Update";
                w2.gbxCategory.Visibility = Visibility.Visible;
                w2.Category = category!;
                //w2.txb_cId.Text = category?.Id.ToString();
                w2.txb_cName.Text = category?.Name;
                w2.ShowDialog();
            }
            else
                MessageBox.Show("Error!");
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProcessView w2 = new ProcessView();
            w2.btnProd.Content = "Add";
            w2.gbxProduct.Visibility = Visibility.Visible;
            w2.ShowDialog();
        }

        private void BtnRefreshProduct_Click(object sender, RoutedEventArgs e)
        {
            var products = _unitWork.ProductRepository.Find(p => p.Category!.Name == (dataGridCategory.SelectedItem as Category)!.Name).ToList();
            dataGridProduct.ItemsSource = products;
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedIndex != -1)
            {
                var product = dataGridProduct.SelectedItem as Product;
                _unitWork.ProductRepository.Remove(product!);
                _unitWork.Complete();
            }
            else
                MessageBox.Show("Error!");
        }

        private void BtnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedIndex != -1)
            {
                var product = dataGridProduct.SelectedItem as Product;
                ProcessView w2 = new ProcessView();
                w2.btnProd.Content = "Update";
                w2.gbxProduct.Visibility = Visibility.Visible;
                w2.Product = product!;
                w2.txb_pId.Text = product?.Id.ToString();
                w2.txb_pName.Text = product?.Name;
                w2.txb_pPrice.Text = product?.UnitPrice.ToString();
                w2.txb_pCatId.Text = product?.CategoryId.ToString();
                w2.ShowDialog();
            }
            else
                MessageBox.Show("Error!");
        }

        private void dataGridCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var products = _unitWork.ProductRepository.Find(p => p.Category!.Name == (dataGridCategory.SelectedItem as Category)!.Name).ToList();
            dataGridProduct.ItemsSource = products;
        }
    }
}
