using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for FrmKarta.xaml
    /// </summary>
    public partial class FrmKarta : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;

        public FrmKarta()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();

            try
            {
                konekcija.Open();
                string vratiFilmove = @"select FilmID, Naslov from tblFilm";
                SqlDataAdapter daFilma = new SqlDataAdapter(vratiFilmove, konekcija);
                DataTable dtFilma = new DataTable();
                daFilma.Fill(dtFilma);
                cbxFilm.ItemsSource = dtFilma.DefaultView;
                daFilma.Dispose();
            }
            catch(SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally 
            {
                if( konekcija != null) 
                { 
                    konekcija.Close();
                }
            }
            cbxFilm.Focus();
        }

        public FrmKarta(bool azuriraj, DataRowView red)
        {
            this.azuriraj = azuriraj;
            this.red = red;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
               //if (Datum.SelectedDate != null)
                DateTime date = (DateTime)Datum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                SqlCommand cmd = new SqlCommand //objekat klase
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@FilmID", SqlDbType.Int).Value = cbxFilm.SelectedValue;
                cmd.Parameters.Add("@datum", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@BrojKarata", SqlDbType.Int).Value = txtBrojKarata.Text;
                cmd.Parameters.Add("@Sedište", SqlDbType.Int).Value = txtSedište.Text;
                cmd.Parameters.Add("@Sala", SqlDbType.NVarChar).Value = txtSala.Text;
                cmd.Parameters.Add("@Cena", SqlDbType.Money).Value = txtCena.Text;
                cmd.CommandText = @"Insert into TblKarta(FilmID, datum, BrojKarata, Sedište, Sala, Cena)
                                values(@FilmID,@datum,@BrojKarata, @Sedište, @Sala,@Cena);";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos vrednosti nije validan.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Odaberite datum!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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

        }
    }
}
