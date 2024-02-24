using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace PrikazFilmova.Forme
{
    /// <summary>
    /// Interaction logic for FrmFilmovi.xaml
    /// </summary>
    public partial class FrmFilmovi : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        private DataRowView pomocniRed;

        public FrmFilmovi()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            try
            {
                konekcija = kon.KreirajKonekciju();
                konekcija.Open();

                string vratiŽanrove = @"select ŽanrID, NazivŽanrova from tblŽanr";
                SqlDataAdapter daŽanra = new SqlDataAdapter(vratiŽanrove, konekcija);
                DataTable dtŽanra = new DataTable();
                daŽanra.Fill(dtŽanra);
                cbZanr.ItemsSource = dtŽanra.DefaultView;
                daŽanra.Dispose();
                dtŽanra.Dispose();

                string vratiGlumce = @"select GlumacID, ImeGlumca from tblGlumac";
                SqlDataAdapter daGlumca = new SqlDataAdapter(vratiGlumce, konekcija);
                DataTable dtGlumca = new DataTable();
                daGlumca.Fill(dtGlumca);
                cbGlumci.ItemsSource = dtGlumca.DefaultView;
                daGlumca.Dispose();
                dtGlumca.Dispose();

                string vratiRezisere = @"select ReziserID, ImeRezisera from tblReziser";
                SqlDataAdapter daRezisera = new SqlDataAdapter(vratiRezisere, konekcija);
                DataTable dtRezisera = new DataTable();
                daRezisera.Fill(dtRezisera);
                cbReziser.ItemsSource = dtRezisera.DefaultView;
                daRezisera.Dispose();
                dtRezisera.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
            txtNaslov.Focus();
        }

        public FrmFilmovi(bool azuriraj, DataRowView red)
        {
            this.azuriraj = azuriraj;
            this.red = red;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija = kon.KreirajKonekciju();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand //objekat klase
                {
                    Connection = konekcija
                };
                //string vratiFilmove = @"select FilmID, Naslov from tblFilm";
                //SqlDataAdapter daŽanra = new SqlDataAdapter(vratiFilmove, konekcija);
                //DataTable dtŽanra = new DataTable();
                //daŽanra.Fill(dtŽanra);
                cmd.Parameters.Add("@Naslov", SqlDbType.NVarChar).Value = txtNaslov.Text;
                cmd.Parameters.Add("@Trajanje", SqlDbType.NVarChar).Value = txtTrajanje.Text;
                cmd.Parameters.Add("@ŽanrID", SqlDbType.Int).Value = cbZanr.SelectedValue;
                cmd.Parameters.Add("@GlumacID", SqlDbType.Int).Value = cbGlumci.SelectedValue;
                cmd.Parameters.Add("@ReziserID", SqlDbType.Int).Value = cbReziser.SelectedValue;
                if (azuriraj)
                {
                    DataRowView red = pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update TblFilm set Naslov=@Naslov, 
                    Trajanje=@Trajanje, ŽanrID=@ŽanrID, GlumacID=@GlumacID, ReziserID=@ReziserID
                    where FilmID=@id";
                    pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"Insert into TblFilmovi(Naslov, Trajanje, ŽanrID, GlumacID, ReziserID)
                                    values(@Naslov,@Trajanje,@ŽanrID,@GlumacID,@ReziserID);";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
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
