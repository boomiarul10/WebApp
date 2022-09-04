using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace Am.Pages
{
    public class MovieListModel : PageModel
    {
        public List<Model.Movie> lstMovie = new List<Model.Movie>();

        public void OnGet()
        {

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-H4UAIVJ;Initial Catalog=AZ;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT Sno,MovieName,MovieYear,MovieDescription,Actors,Genre FROM MovieDB",con);
            con.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    lstMovie.Add(new Model.Movie
                    {
                        Sno = (int)sdr["Sno"],
                        MovieName = sdr["MovieName"].ToString(),
                        MovieYear = (int)sdr["Movieyear"],
                        MovieDescription = sdr["MovieDescription"].ToString(),
                        Actors = sdr["Actors"].ToString(),
                        Genre = sdr["Genre"].ToString()
                    }) ;

                }
                

            }
            con.Close();

        }

        public IActionResult OnGetDelete(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-H4UAIVJ;Initial Catalog=AZ;Integrated Security=True");
            try
            {                
                SqlCommand cmd = new SqlCommand("DELETE FROM MovieDB where Sno = " + id, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception eX)
            {
                throw new Exception(eX.Message);
            }
            finally
            {
                con.Close();
            }
            return RedirectToPage("Index");
        }
       
    }
}

