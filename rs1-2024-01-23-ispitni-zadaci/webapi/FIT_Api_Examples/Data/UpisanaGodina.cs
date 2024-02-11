using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Data
{
    public class UpisanaGodina
    {
        public int Id { get; set; }
        public DateTime datumUpisa { get; set; }
        [ForeignKey(nameof(akademskaGodina))]
        public int akademskaGodinaId { get; set; }
        public virtual AkademskaGodina akademskaGodina { get; set; }
        public float cijenaSkolarine { get; set; }
        public bool obnova { get; set; }
        public DateTime datumOvjere { get; set; }
        public string napomena { get; set; }
        [ForeignKey(nameof(student))]
        public int studentId { get; set; }
        public virtual Student student { get; set; }
        [ForeignKey(nameof(evidentirao))]
        public int evidentiraoId { get; set; }
        public virtual KorisnickiNalog evidentirao { get; set; }
        public int godinaStudija { get; set; }
    }
}
