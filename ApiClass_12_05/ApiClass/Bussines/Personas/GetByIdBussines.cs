using ApiClass.Data;
using ApiClass.Resultado.Personas;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using MediatR;
using static ApiClass.Bussines.Personas.GetByIdBussines;
using System.ComponentModel;

namespace ApiClass.Bussines.Personas
{
    public class GetByIdBussines
    {
        public class GetPersonasByIdComando : IRequest <ListadoPersonas>
        {
            public int IdPersona { get; set; }
        }

        public class EjecutarValidacion : AbstractValidator<GetPersonasByIdComando>
        {
            public EjecutarValidacion()
            {
                RuleFor(x => x.IdPersona).NotEmpty().WithMessage("Debe ingresar un id");
                //ni nulo ni vacia
            }
        }

        public class Manejador : IRequestHandler<GetPersonasByIdComando, ListadoPersonas>
        {
            private readonly ContextDB _context;
            private readonly IValidator<GetPersonasByIdComando> _validator;

            public Manejador(ContextDB contexto, IValidator<GetPersonasByIdComando> validator)
            {
                _context= contexto;
                _validator = validator;
            }
            // método que resuelve la logica de negocio
            public async Task<ListadoPersonas> Handle(GetPersonasByIdComando comando, CancellationToken cancellation)
            {
                var result = new ListadoPersonas();
                var validation = await _validator.ValidateAsync(comando);

                if (!validation.IsValid)
                {
                    var errors = string.Join(Environment.NewLine, validation.Errors);
                    result.SetMensajeError(errors, HttpStatusCode.InternalServerError);
                    return result;
                }

                // diferentes validaciones por dato
                if (comando.IdPersona == null || comando.IdPersona < 0)
                {
                    result.SetMensajeError("El párametro id es obligatorio", HttpStatusCode.BadRequest);
                    return result;
                }

                var persona = await _context.Personas.Where(c => c.Id == comando.IdPersona).Include(c => c.Categoria).FirstOrDefaultAsync();

                if (persona != null)
                {
                    var itemPersona = new ItemPersona
                    {
                        Apellido = persona.Apellido,
                        Id = persona.Id,
                        Nombre = persona.Nombre,
                        NombreCategoria = persona.Categoria.Nombre
                    };

                    result.ListPersonas.Add(itemPersona);
                    return result;
                }
                var mensajeError = "Persona con " + comando.IdPersona.ToString() + " no encontrada";

                result.SetMensajeError(mensajeError, HttpStatusCode.NotFound);

                return result;


            }
        }
    }
}   
