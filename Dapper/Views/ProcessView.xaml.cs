using Dapper.DAL.Conxtext;
using Dapper.Entities;
using Dapper.Units.Abstract;
using Dapper.Units.Concrete;
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
    /// Логика взаимодействия для ProcessView.xaml
    /// </summary>
    public partial class ProcessView : Window
    {
        IUnitWork _unitWork;
        public Category Category { get; set; }
        public Product Product { get; set; }
        public ProcessView()
        {
            InitializeComponent();
            _unitWork = new UnitWork(new ProductContext());
            Category = new Category();
            Product = new Product();
        }
        private void BtnCat_Click(object sender, RoutedEventArgs e)
        {
            if (btnCat.Content.ToString() == "Add")
            {
                if (txb_cName.Text != "")
                {
                    var catName = txb_cName.Text;
                    _unitWork.CategoryRepository.Add(new Category() { Name = catName });
                    gbxCategory.Visibility = Visibility.Hidden;
                    Close();
                }
                else
                    MessageBox.Show("Error!");
            }
            else
            {
                if (txb_cName.Text != "")
                {
                    Category!.Name = txb_cName.Text;
                    _unitWork.CategoryRepository.Update(Category);
                    gbxCategory.Visibility = Visibility.Hidden;
                    Close();
                }
                else
                    MessageBox.Show("Error!");
            }
            _unitWork.Complete();
        }

        private void BtnProd_Click(object sender, RoutedEventArgs e)
        {
            if (btnProd.Content.ToString() == "Add")
            {
                if (txb_pName.Text != "" || txb_pPrice.Text != "" || txb_pCatId.Text != "")
                {
                    var pName = txb_pName.Text;
                    double pPrice = double.Parse(txb_pPrice.Text);
                    int pCatId = int.Parse(txb_pCatId.Text);
                    _unitWork.ProductRepository.Add(new Product() { Name = pName, UnitPrice = pPrice, CategoryId = pCatId });
                    gbxProduct.Visibility = Visibility.Hidden;
                    Close();
                }
                else
                    MessageBox.Show("Error!");
            }
            else
            {
                if (txb_pName.Text != "" || txb_pPrice.Text != "" || txb_pCatId.Text != "")
                {
                    Product!.Name = txb_pName.Text;
                    Product!.UnitPrice = double.Parse(txb_pPrice.Text);
                    Product!.CategoryId = int.Parse(txb_pCatId.Text);
                    Product!.Category = _unitWork.CategoryRepository.GetAll().FirstOrDefault(c => c.Id == Product!.CategoryId);
                    _unitWork.ProductRepository.Update(Product);
                    gbxProduct.Visibility = Visibility.Hidden;
                    Close();
                }
                else
                    MessageBox.Show("Error!");
            }
            _unitWork.Complete();
        }
    }
}
