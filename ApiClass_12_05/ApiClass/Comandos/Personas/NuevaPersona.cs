using ApiClass.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClass.Comandos.Personas
{
    public class NuevaPersona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaAlta { get; set; }
        
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
    }
}
