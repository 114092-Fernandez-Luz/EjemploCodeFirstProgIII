﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClass.Models
{
    [Table("categorias")]
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
