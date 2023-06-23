using ApiClass.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiClass.Data
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options) 
        {
            // options va tener la cadena de base de datos
            //recibe un objeto option, q va tener la conexion string
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
