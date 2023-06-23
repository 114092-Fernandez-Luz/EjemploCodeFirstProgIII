using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClass.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsu { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
    }
}
