using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace PrikazFilmova.Forme
{
    /// <summary>
    /// Interaction logic for FrmGlumci.xaml
    /// </summary>
    public partial class FrmGlumci : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        public FrmGlumci(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            //txtImeGlumca.Focus();
        }

        public FrmGlumci()
        {
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand //objekat klase
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ImeGlumca", SqlDbType.NVarChar).Value = txtImeGlumca.Text; //parametri
                cmd.CommandText = @"Insert into TblGlumac(ImeGlumca)
                                values(@ImeGlumca)";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch(SqlException)
            {
                MessageBox.Show("Unos vrednosti nije validan.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null) 
                { 
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
