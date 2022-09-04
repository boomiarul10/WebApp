using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApp.Pages
{
    public class EditMovieModel : PageModel
    {
        public int Sno = 0;
        public string movieName = "";
        public string movieYear = "";
        public string movieDescription = "";
        public string actors = "";
        public string genre = "";

        public void OnGet(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-H4UAIVJ;Initial Catalog=AZ;Integrated Security=True");
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Sno,MovieName,MovieYear,MovieDescription,Actors,Genre FROM MovieDB WHERE Sno=" + id, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    Sno = (int)sdr["Sno"];
                    movieDescription = sdr["MovieDescription"].ToString();
                    movieName = sdr["MovieName"].ToString();
                    actors = sdr["Actors"].ToString();
                    genre = sdr["Genre"].ToString();
                    movieYear = sdr["MovieYear"].ToString();
                    sdr.Close();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        public IActionResult OnPost()
        {
            Sno = int.Parse(Request.Form["Sno"]);
            movieName = Request.Form["movieName"];
            movieYear = Request.Form["movieYear"];
            movieDescription = Request.Form["movieDescription"];
            actors = Request.Form["actors"];
            genre = Request.Form["genre"];

            string moviedbTableUpdateQuery = "UPDATE MovieDB SET MovieYear = '"+movieYear+"',MovieName='"+movieName+"' WHERE Sno="+Sno;
            string ConString = "Data Source=DESKTOP-H4UAIVJ;Initial Catalog=AZ;Integrated Security=True";

            SqlConnection con = new SqlConnection(ConString);
            try
            {
                con.Open();

                SqlCommand moviedbcmd = new SqlCommand(moviedbTableUpdateQuery, con);
                moviedbcmd.Connection = con;
                moviedbcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception("Exception Near Insertion", ex);
            }
            finally
            {
                con.Close();
            }
            return RedirectToPage("Index");

        }

        public void OnPostClear()
        {
            this.movieName = "";
            this.movieYear = "";
            this.movieDescription = "";
            this.actors = "";
            this.genre = "";

        }
    }
}
