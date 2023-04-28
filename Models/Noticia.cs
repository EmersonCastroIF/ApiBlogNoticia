using System.ComponentModel.DataAnnotations;

public class Noticia
{

    public int Id { get; set; }

    public int UsuarioId {get;set;}
    public Usuario? Usuario {get;set;}

    [Required(ErrorMessage = "Titulo é obrigatório")]
    [MinLength(5, ErrorMessage = "O Titulo deve conter, no minimo, 5 caracteres")]
    [MaxLength(100, ErrorMessage = "O Titulo pode conter, no máximo, 100 caracteres")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Texto da Notícia é obrigatório")]
    [MinLength(5, ErrorMessage = "O Texto deve conter, no minimo, 5 caracteres")]
    public string Texto { get; set; }

    [Required(ErrorMessage = "Sub-Titulo é obrigatório")]
    [MinLength(5, ErrorMessage = "O Sub-Titulo deve conter, no minimo, 5 caracteres")]
    [MaxLength(100, ErrorMessage = "O Sub-Titulo pode conter, no máximo, 100 caracteres")]    
    public string SubTitulo { get; set; }

    public int QtdLike { get; set; }
    public int QtdDesLike { get; set; }   
    public Boolean Publicado { get; set; }  

    [DataType(DataType.Date)]
    public DateTime DataPublicacao{ get; set; }  
    
    [DataType(DataType.Date)]
    public DateTime DataAlteracao{ get; set; }  
        
}