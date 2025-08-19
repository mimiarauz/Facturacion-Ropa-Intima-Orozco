using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

public class CtrlRestauracion : Controller
{
    private readonly IConfiguration _configuration;

        public CtrlRestauracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult RestoreDatabase(string filePath)
        {
            try
    {
        string connectionString = _configuration.GetConnectionString("DefaultConection");

        // Agregar la ruta completa del archivo de respaldo
        string backupPath = $@"C:\Users\LENOVO\Downloads\Respaldo\{filePath}";

        using (SqlConnection masterConnection = new SqlConnection(connectionString))
        {
            masterConnection.Open();

            // Obtener el nombre de la base de datos actual
            string targetDatabaseName;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DB_NAME() AS DatabaseName", masterConnection))
            {
                targetDatabaseName = sqlCommand.ExecuteScalar() as string;
            }

            // Cambiar temporalmente a la base de datos 'master' para restaurar
            using (SqlCommand switchToMaster = new SqlCommand($"USE master", masterConnection))
            {
                switchToMaster.ExecuteNonQuery();
            }

            // Desconectar usuarios de la base de datos
using (SqlCommand sqlCommand = new SqlCommand($"ALTER DATABASE [{targetDatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", masterConnection))
{
    sqlCommand.ExecuteNonQuery();
}

// Esperar antes de intentar la restauración
System.Threading.Thread.Sleep(5000); // Espera de 5 segundos

// Restaurar la base de datos desde el archivo seleccionado
using (SqlCommand sqlCommand = new SqlCommand($"RESTORE DATABASE [{targetDatabaseName}] FROM DISK='{backupPath}' WITH REPLACE", masterConnection))
{
    sqlCommand.ExecuteNonQuery();
}

            // Cambiar de nuevo a la base de datos original
            using (SqlCommand switchBack = new SqlCommand($"USE [{targetDatabaseName}]", masterConnection))
            {
                switchBack.ExecuteNonQuery();
            }
        }
        
        return Json(new { success = true, message = "Restauración exitosa" });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error en la restauración: {ex.Message}\n{ex.StackTrace}");
            return Json(new { success = false, message = $"Error en la restauración: {ex.Message}\n{ex.StackTrace}" });
        }
    }
}
