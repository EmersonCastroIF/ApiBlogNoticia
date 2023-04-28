using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;



[Index(nameof(Email), IsUnique = true)]
public class Usuario
{
    
    public int Id { get; set; }

    public TipoUsuario TipoUsuario { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(5)]
    [MaxLength(100, ErrorMessage = "O Nome pode conter, no máximo, 100 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "E-mail é obrigatório")]
    [MinLength(5)]
    [MaxLength(100, ErrorMessage = "O E-mail pode conter, no máximo, 100 caracteres")]
    public string Email { get; set; }

    [MaxLength(100, ErrorMessage = "O Apelido pode conter, no máximo, 100 caracteres")]
    public string Apelido { get; set; }

    [Required(ErrorMessage = "Data de Nascimento é obrigatório")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "A Senha é obrigatório")]
    [MinLength(8, ErrorMessage = "A Senha deve conter, no minimo, 8 caracteres")]
    public string Senha { get; set; }    

    public Boolean Bloqueado { get; set; }    
    public Boolean Ativo { get; set; }    
    public string CodigoAtivacao {get; set;} 
    public string CodigoRedefineEmail {get; set;}
    public string CodigoRedefineSenha {get; set;}
}