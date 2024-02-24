using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrikazFilmova
{
    internal class Konekcija
    {  
        public SqlConnection KreirajKonekciju()
        {
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"DESKTOP-04RJ95O\SQLEXPRESS",
                InitialCatalog = "PrikazivanjeFilmova",
                IntegratedSecurity = true
            };

            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);

            return konekcija;
        }
    }
}
