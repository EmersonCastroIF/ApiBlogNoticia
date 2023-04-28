using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Reacoes
{

    public int Id { get; set; }
    public int NoticiaId { get; set; }
    public int UsuarioId { get; set; }
    public Boolean Like { get; set; }
    public Boolean DesLike { get; set; }

}

