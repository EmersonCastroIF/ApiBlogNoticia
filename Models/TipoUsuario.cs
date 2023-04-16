using System.ComponentModel.DataAnnotations;

public class TipoUsuario
{
    
    public int Id { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatória")]
    [MinLength(5)]
    [MaxLength(100, ErrorMessage = "A Descrição pode conter, no máximo, 100 caracteres")]
    public string Descricao { get; set; }

   
}