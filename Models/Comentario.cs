using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Comentario
{

    public int Id { get; set; }

    public int UsuarioId {get;set;}
    public Usuario? Usuario {get;set;}
    public int IdNoticia { get; set; }

    [MinLength(5, ErrorMessage = "O Comentário deve conter, no minimo, 5 caracteres")]
    public string Texto { get; set; }

    [DataType(DataType.Date)]
    public DateTime Data { get; set; }

}

