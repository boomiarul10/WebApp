using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Am.Pages
{
    public class AddMovieModel : PageModel
    {
        public int Sno = 0;
        public string movieName = "";
        public string movieYear = "";
        public string movieDescription = "";
        public string actors = "";
        public string genre = "";
        

       
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            movieName = Request.Form["movieName"];
            movieYear = Request.Form["movieYear"];
            movieDescription = Request.Form["movieDescription"];
            actors = Request.Form["actors"];
            genre = Request.Form["genre"];

            string moviedbTableInsertQuery = "INSERT INTO MovieDB VALUES('" + movieName + "','" + movieYear + "','" + movieDescription + "','" + actors + "','" + genre + "');";



            string ConString = "Data Source=DESKTOP-H4UAIVJ;Initial Catalog=AZ;Integrated Security=True";

            SqlConnection con = new SqlConnection(ConString);
            try
            {
                con.Open();

                SqlCommand moviedbcmd = new SqlCommand(moviedbTableInsertQuery, con);
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
   
    
    
    
    
    

