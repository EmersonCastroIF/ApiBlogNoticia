using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnviaEmail;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class RedefineNomeAutorController : ControllerBase
{
    private readonly DataContext dataContext;

    public RedefineNomeAutorController(DataContext Context)
    {
        dataContext = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> RedefineNomeAutor([FromBody] RedefineNomeAutorViewModel Usuario)
    {
        try
        {

            var DadosUsuario = await dataContext.Usuario.FindAsync(Usuario.IdUser);
            if (DadosUsuario == null)
            {
                return BadRequest($"Usuário não foi encontrado");
            }


            try
            {
                DadosUsuario.Nome = Usuario.Nome;
                await dataContext.SaveChangesAsync(); // atualiza o registro no banco de dados
            }
            catch
            {
                return BadRequest("Falha ao alterar o Nome do Autor");
            }

            return Ok("E-mail Alterado com sucesso !!!");

        }
        catch
        {
            return BadRequest("Falha ao Confirmar alteração de senha");
        }
    }
}