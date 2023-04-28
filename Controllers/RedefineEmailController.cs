using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnviaEmail;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class RedefineEmailController : ControllerBase
{
    private readonly DataContext dataContext;

    public RedefineEmailController(DataContext Context)
    {
        dataContext = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> ConfirmaAlteracaoEmail([FromBody] ConfirmaAlteracaoEmailViewModel Usuario)
    {
        try
        {
            //Gera código aleatório para o usuário não usar o mesmo código mais de uma vez
            const string CaracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int tamanho = 10;
            var random = new Random();

            var sb = new StringBuilder();
            for (int i = 0; i < tamanho; i++)
            {
                int index = random.Next(CaracteresPermitidos.Length);
                sb.Append(CaracteresPermitidos[index]);
            }

            string codigo = sb.ToString();

            var DadosUsuario = await dataContext.Usuario.FindAsync(Usuario.IdUser);
            if (DadosUsuario == null)
            {
                return BadRequest($"Usuário não foi encontrado");
            }

            if (DadosUsuario.CodigoRedefineEmail == Usuario.CodigoAtivacao)
            {
                DadosUsuario.Email = Usuario.Email;
                DadosUsuario.CodigoRedefineEmail = codigo;

                try
                {
                    await dataContext.SaveChangesAsync(); // atualiza o registro no banco de dados
                }
                catch
                {
                    return BadRequest("Falha ao confirmar o código");
                }

                return Ok("E-mail Alterado com sucesso !!!");
            }
            else
            {
                return BadRequest("Código Inválido !!");
            }
        }
        catch
        {
            return BadRequest("Falha ao Confirmar alteração de senha");
        }
    }
}