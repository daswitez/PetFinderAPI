﻿namespace PetFinderAPI.input
{
    public class RecordatorioInput
    {
        public string Suministrar { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string MascotaPropiaId { get; set; } = null!;
        public string HistorialId { get; set; } = null!;
        public DateOnly FechaSuministrar { get; set; }
    }
}
