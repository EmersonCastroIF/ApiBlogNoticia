using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly DataContext context;

    public LoginController(DataContext Context)
    {
        context = Context;
    }


    [HttpPost]
    public async  Task<ActionResult<Usuario>> Login([FromBody] LoginViewModel loginDTO)
    {
        // Busca o usuário pelo email
        var usuario = await context.Usuario.Include(p => p.TipoUsuario).SingleOrDefaultAsync(u => u.Email == loginDTO.email);

        if (usuario == null)
        {
            return BadRequest("Usuário inválido");
        }

        //Verifica se a senha está correta
        if (usuario.Senha != loginDTO.senha && usuario.Email == loginDTO.email)
        {
            return BadRequest("Senha incorreta");
        }

        // Se o usuário e a senha estiverem corretos, retorna os dados do usuário
        return Ok(usuario);
    }   
}