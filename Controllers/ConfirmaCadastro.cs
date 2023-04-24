using EnviaEmail;
using Microsoft.AspNetCore.Mvc;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class ConfirmaCadastroController : ControllerBase
{

    private readonly DataContext context;

    public ConfirmaCadastroController(DataContext Context)
    {
        context = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> ConfirmaCadastro([FromBody] ConfirmaCadastroViewModel Usuario)
    {
        try
        {

            var DadosUsuario = await context.Usuario.FindAsync(Usuario.IdUser);
            if (DadosUsuario == null)
            {
                return BadRequest($"Usuário não foi encontrado");
            }

            if (DadosUsuario.CodigoAtivacao == Usuario.CodigoAtivacao)
            {
                DadosUsuario.Ativo = true;

                try
                {
                    await context.SaveChangesAsync(); // atualiza o registro no banco de dados
                }
                catch
                {
                    return BadRequest("Falha ao confirmar o código");
                }

                return Ok("Cadastro Confirmado com sucesso !!!");
            }
            else{
                return BadRequest("Código Inválido !!");
            }
        }
        catch
        {
            return BadRequest("Falha ao enviar e-mail");
        }
    }
}