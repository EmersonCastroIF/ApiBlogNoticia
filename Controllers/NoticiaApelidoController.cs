using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class NoticiaApelidoController : ControllerBase
{
    private readonly DataContext context;

    public NoticiaApelidoController(DataContext Context)
    {
        context = Context;
    }


    [HttpGet("{apelido}")]
    public async Task<ActionResult<Noticia>> Get([FromRoute] string apelido)
    {
        try
        {
            // Busca o usuário pelo apelido
            var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.Apelido == apelido);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            // Busca todas as notícias do usuário encontrado
            var noticias = await context.Noticia.Include(p => p.Usuario)
                                               .Where(p => p.Usuario.Id == usuario.Id && p.Publicado == true)
                                               .ToListAsync();
            if (noticias != null)
                return Ok(noticias);
            else
                return NotFound();
        }
        catch
        {
            return BadRequest();
        }
    }
}