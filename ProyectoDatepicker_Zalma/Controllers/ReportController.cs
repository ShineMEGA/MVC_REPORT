using Microsoft.AspNetCore.Mvc;
using ProyectoDatepicker.Models;
using System.Data.SqlClient;
using System.Data;

public class ReportController : Controller
{
    private readonly string _connectionString;

    public ReportController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // GET: Reporte
    public IActionResult Index()
    {
        var model = new ReporteViewModel
        {
            FechaInicio = DateTime.Now.AddMonths(-1).Date, // Default date
            FechaFin = DateTime.Now.Date, // Defaul
            Reportes = new List<ReporteServicio>() // Initializa lista vacia
        };
        return View(model);
    }


    [HttpPost]
    public IActionResult Index(ReporteViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("ObtenerReporteServiciosZalma", connection) // se asigna el nombre del SP de SQL
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@FechaInicio", model.FechaInicio); // envio de parametro fecha inicio
                    command.Parameters.AddWithValue("@FechaFin", model.FechaFin); // envio de parametro fecha fin

                    connection.Open();
                    var reader = command.ExecuteReader();

                    model.Reportes = new List<ReporteServicio>();
                    while (reader.Read())
                    {
                        model.Reportes.Add(new ReporteServicio
                        {
                            NrReporte = reader.GetInt32(0),
                            SvIndice = reader.GetString(1),
                            SvFhReporte = reader.GetDateTime(2),
                            PrNombre = reader.GetString(3),
                            PrRFC = reader.GetString(4),
                            ClNombre = reader.GetString(5),
                            ClEstatus = reader.GetString(6),
                            SuNombre = reader.GetString(7),
                            SuSupervisoria = reader.GetString(8)
                        });
                    }
                    connection.Close();
                }
            }
        }
        catch (Exception ex)
        {
            // Log 
            model.Reportes = new List<ReporteServicio>();
            ModelState.AddModelError("", "Ocurrió un error al obtener los reportes.");
        }
        return View(model);
    }


}
