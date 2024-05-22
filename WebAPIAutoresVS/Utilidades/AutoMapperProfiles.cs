using AutoMapper;
using WebAPIAutoresVS.DTOs;
using WebAPIAutoresVS.Entidades;

namespace WebAPIAutoresVS.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();

            CreateMap<LibroCreacionDTO, Libro>();
            CreateMap<Libro, LibroDTO>();
            CreateMap<Libro,LibroDTOOnly>();

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
        }
    }
}
