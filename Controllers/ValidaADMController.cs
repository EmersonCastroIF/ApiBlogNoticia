using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnviaEmail;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class ValidaADMController : ControllerBase
{
    private readonly DataContext dataContext;

    public ValidaADMController(DataContext Context)
    {
        dataContext = Context;
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> Get()
    {
        try
        {
            var usuarios = await dataContext.Usuario.Include(p => p.TipoUsuario).ToListAsync();

            if (usuarios.Any(u => u.TipoUsuario.Id == 3))
            {
                return Ok(usuarios);
            }

            return BadRequest("Nenhum usuário encontrado com TipoUsuario igual a 3");
        }
        catch
        {
            return BadRequest("Erro ao obter os Usuarios");
        }

    }
}