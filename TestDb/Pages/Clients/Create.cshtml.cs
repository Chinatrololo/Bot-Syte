using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TestDb.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0
                || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields must be fulltified";
                return;
            }


            try
            {
                string connectionString = "Data Source=DESKTOP-5HIP9N2\\SQLEXPRESS;Initial Catalog=clients;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlInsertCommand = "INSERT INTO clients " +
                                            "(name, email, phone, address) VALUES" +
                                            "(@name, @email, @phone, @address)";

                    using (SqlCommand sqlCommand = new SqlCommand(sqlInsertCommand, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@name", clientInfo.name);
                        sqlCommand.Parameters.AddWithValue("@email", clientInfo.email);
                        sqlCommand.Parameters.AddWithValue("@phone", clientInfo.phone);
                        sqlCommand.Parameters.AddWithValue("@address", clientInfo.address);

                        sqlCommand.ExecuteNonQuery();

                    }

                        
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
                
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }


    }
}
