using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

public class CtrlBackup : Controller
{
    private readonly IConfiguration _configuration;

    public CtrlBackup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public ActionResult RealizarRespaldo()
    {
        try
        {
            string connectionString = _configuration.GetConnectionString("DefaultConection");
            string fechaActual = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupPath = $@"C:\Users\LENOVO\Downloads\Respaldo\Respaldo_{fechaActual}.bak";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand($"BACKUP DATABASE [{sqlConnection.Database}] TO DISK='{backupPath}' WITH FORMAT", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }

            return Json(new { success = true, message = "Respaldo exitoso" });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error en el respaldo: {ex.Message}\n{ex.StackTrace}");
            return Json(new { success = false, message = $"Error en el respaldo: {ex.Message}\n{ex.StackTrace}" });
        }
    }
}

