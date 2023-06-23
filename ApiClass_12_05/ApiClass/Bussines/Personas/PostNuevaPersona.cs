using ApiClass.Data;
using ApiClass.Models;
using ApiClass.Resultado.Personas;
using FluentValidation;
using MediatR;

namespace ApiClass.Bussines.Personas
{
    public class PostNuevaPersona
    {
        public class Post_NuevaPersona : IRequest<ListadoPersonas>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int IdCategoria { get; set; }
        }

        public class EjecutarValidacion : AbstractValidator<Post_NuevaPersona>
        {
            public EjecutarValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty(); // ni null ni vacia
            }
        }

        public class Manejador : IRequestHandler<Post_NuevaPersona, ListadoPersonas>
        {
            private readonly ContextDB _contexto;

            public Manejador(ContextDB contexto)
            {
                _contexto = contexto;

            }

            public async Task<ListadoPersonas> Handle(Post_NuevaPersona comando, CancellationToken cancellationToken)
            {
                var result = new ListadoPersonas();

                var persona = new Persona
                {
                    Apellido = comando.Apellido,
                    Nombre = comando.Nombre,
                    FechaAlta = DateTime.Now,
                    IdCategoria = comando.IdCategoria

                };
                await _contexto.Personas.AddAsync(persona);
                await _contexto.SaveChangesAsync();

                var personaItem = new ItemPersona
                {
                    Apellido = persona.Apellido,
                    Nombre = persona.Nombre,
                    Id = persona.Id
                };
                result.ListPersonas.Add(personaItem);
                return result;
            }
        }
    }
}
