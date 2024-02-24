using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using PrikazFilmova.Forme;

namespace PrikazFilmova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Select upiti
        static string filmoviSelect = @"select FilmID, Naslov, Trajanje, 
            TblŽanr.NazivŽanrova as 'Žanr', TblGlumac.ImeGlumca as 'Glumac', 
            TblReziser.ImeRezisera as 'Režiser'
                from TblFilm join TblŽanr on TblFilm.ŽanrID=TblŽanr.ŽanrID
                    join TblGlumac on TblFilm.GlumacID= TblGlumac.GlumacID
                    join TblReziser on TblFilm.ReziserID= TblReziser.ReziserID";
        static string karteSelect = @"select TblFilm.Naslov as 'Naslov', Sedište as 'Sedište', 
            BrojKarata as 'Broj Karata', Sala, Cena, Datum, Sedište
                from TblKarta join TblFilm on TblKarta.FilmID=tblfilm.FilmID";
        static string žanrSelect = @"select ŽanrID as ID, NazivŽanrova as 'Žanrovi' from TblŽanr";
        static string glumciSelect = @"select GlumacID as ID, ImeGlumca as 'Glumac' from TblGlumac";
        static string reziserSelect = @"select ReziserID as ID, ImeRezisera as 'Režiser' from TblReziser";
        #endregion

        #region Delete upiti
        string filmoviDelete = @"delete from TblFilm where FilmID=";
        string karteDelete = @"delete from TblKarta where KartaID=";
        string glumciDelete = @"delete from TblGlumac where GlumacID=";
        string žanrDelete = @"delete from TblŽanr where ŽanrID=";
        string reziserDelete = @"delete from TblReziser where ReziserID=";
        
        #endregion

        #region select upiti sa uslovom
        static string selectUslovFilmovi = @"select * from TblFilm where FilmID =";
        static string selectUslovKarte = @"select * from TblKarte where KartaID =";
        static string selectUslovZanr = @"select * from TblŽanr where ŽanrID =";
        static string selectUslovGlumci = @"select * from TblGlumac where GlumacID =";
        static string selectUslovReziseri = @"select * from TblReziser where ReziserID =";
        #endregion

        string ucitanaTabela;
        bool azuriraj;

        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju(); //objekat klase sql konekcije
            UcitajPodatke(dataGridCentralni, filmoviSelect);
        }

        void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dataTable = new DataTable();


                dataAdapter.Fill(dataTable);

                if (grid != null)
                {
                    grid.ItemsSource = dataTable.DefaultView;
                }

                ucitanaTabela = selectUpit; //izvor podataka za grid, učitavanje potrebnih podataka
                dataTable.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("neuspesno ucitani podaci", "greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red[0];
                cmd.CommandText = selectUslov + "id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();

                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(filmoviSelect))
                    {
                        FrmFilmovi prozorFilm = new FrmFilmovi(azuriraj, red);

                        prozorFilm.txtNaslov.Text = citac["Naslov"].ToString();
                        prozorFilm.cbZanr.Text = citac["Zanr"].ToString();
                        prozorFilm.cbGlumci.Text = citac["Glumac"].ToString();
                        prozorFilm.cbReziser.Text = citac["Reziser"].ToString();
                        prozorFilm.txtTrajanje.Text = citac["Trajanje"].ToString();
                        prozorFilm.ShowDialog();
                    }
                    else if (ucitanaTabela == karteSelect)
                    {
                        FrmKarta prozorFilmovi = new FrmKarta(azuriraj, red);

                        prozorFilmovi.cbxFilm.Text = citac["Film"].ToString();
                        prozorFilmovi.txtBrojKarata.Text = citac["Broj Karata"].ToString();
                        prozorFilmovi.txtSala.Text = citac["Sala"].ToString();
                        prozorFilmovi.txtSedište.Text = citac["Sedište"].ToString();
                        prozorFilmovi.txtCena.Text = citac["Cena"].ToString();
                        prozorFilmovi.Datum.Text = citac["Datum"].ToString();

                        prozorFilmovi.ShowDialog();
                    }
                    else if (ucitanaTabela == glumciSelect)
                    {
                        FrmGlumci prozorFilmovi = new FrmGlumci(azuriraj, red);

                        prozorFilmovi.txtImeGlumca.Text = citac["Glumac"].ToString();

                        prozorFilmovi.ShowDialog();
                    }
                    else if (ucitanaTabela == žanrSelect)
                    {
                        FrmZanr prozorFilmovi = new FrmZanr(azuriraj, red);

                        prozorFilmovi.txtNazivŽanrova.Text = citac["Žanr"].ToString();

                        prozorFilmovi.ShowDialog();
                    }
                    else if (ucitanaTabela == reziserSelect)
                    {
                        FrmReziser prozorFilmovi = new FrmReziser(azuriraj, red);

                        prozorFilmovi.txtReziser.Text = citac["Režiser"].ToString();

                        prozorFilmovi.ShowDialog();
                    }
                }

            }
            catch
            {

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnFilmovi_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, filmoviSelect);
        }

        private void btnKarta_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, karteSelect);
        }

        private void btnGlumci_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, glumciSelect);
        }

        private void btnZanrovi_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, žanrSelect);
        }
        private void btnReziseri_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, reziserSelect);
        }

        //dodaj
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(filmoviSelect))
            {
                prozor = new FrmFilmovi();
                prozor.ShowDialog(); //nasledjivanje, podklasa?
                UcitajPodatke(dataGridCentralni, filmoviSelect);
            }
            else if (ucitanaTabela.Equals(glumciSelect))
            {
                prozor = new FrmGlumci();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, glumciSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                prozor = new FrmKarta();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, karteSelect);
            }
            else if (ucitanaTabela.Equals(žanrSelect))
            {
                prozor = new FrmZanr();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, žanrSelect);
            }
            else if (ucitanaTabela.Equals(reziserSelect))
            {
                prozor = new FrmReziser();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, reziserSelect);
            }
        }

        //brisanje
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(filmoviSelect))
            {
                ObrisiZapis(dataGridCentralni, filmoviDelete);
                UcitajPodatke(dataGridCentralni, filmoviSelect);
            }
            else if (ucitanaTabela.Equals(glumciSelect))
            {
                ObrisiZapis(dataGridCentralni, glumciDelete); //zovemo brisanje podatka iz baze
                UcitajPodatke(dataGridCentralni, glumciSelect); //refreshujemo podatke
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                ObrisiZapis(dataGridCentralni, karteDelete); //zovemo brisanje podatka iz baze
                UcitajPodatke(dataGridCentralni, karteSelect); //refreshujemo podatke
            }
            else if (ucitanaTabela.Equals(žanrSelect))
            {
                ObrisiZapis(dataGridCentralni, žanrDelete); //zovemo brisanje podatka iz baze
                UcitajPodatke(dataGridCentralni, žanrSelect); //refreshujemo podatke
            }
            else if (ucitanaTabela.Equals(reziserSelect))
            {
                ObrisiZapis(dataGridCentralni, reziserDelete); //zovemo brisanje podatka iz baze
                UcitajPodatke(dataGridCentralni, reziserSelect); //refreshujemo podatke
            }
        }

        private void ObrisiZapis(DataGrid grid, object deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                string s = red[0].ToString();

                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "Pitanje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }


        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(filmoviSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovFilmovi);
                UcitajPodatke(dataGridCentralni, filmoviSelect);
            }else if (ucitanaTabela.Equals(karteSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovKarte);
                UcitajPodatke(dataGridCentralni, karteSelect);
            }
            else if (ucitanaTabela.Equals(glumciSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovGlumci);
                UcitajPodatke(dataGridCentralni, glumciSelect);
            }
            else if (ucitanaTabela.Equals(žanrSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovZanr);
                UcitajPodatke(dataGridCentralni, žanrSelect);
            }
            else if (ucitanaTabela.Equals(reziserSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovReziseri);
                UcitajPodatke(dataGridCentralni, reziserSelect);
            }
        }
    }
}