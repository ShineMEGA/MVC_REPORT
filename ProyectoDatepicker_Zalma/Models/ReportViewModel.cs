namespace ProyectoDatepicker.Models
{
    public class ReporteViewModel
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<ReporteServicio> Reportes { get; set; }
    }



    public class ReporteServicio
    {
        public int NrReporte { get; set; }
        public string SvIndice { get; set; }
        public DateTime SvFhReporte { get; set; }
        public string PrNombre { get; set; }
        public string PrRFC { get; set; }
        public string ClNombre { get; set; }
        public string ClEstatus { get; set; }
        public string SuNombre { get; set; }
        public string SuSupervisoria { get; set; }
    }
}
