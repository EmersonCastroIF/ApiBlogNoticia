using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly DataContext dataContext;

    public AutorController(DataContext Context)
    {
        dataContext = Context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioComNoticias>>> GetUsuariosComNoticias()
    {
        try
        {
            var usuariosComNoticias = await dataContext.Usuario
                .Where(u => u.TipoUsuario.Id == 2) // Filtro para trazer apenas autores
                .GroupJoin(dataContext.Noticia,
                          usuario => usuario.Id,
                          noticia => noticia.UsuarioId,
                          (usuario, noticias) => new { Usuario = usuario, Noticias = noticias })
                .Select(g => new UsuarioComNoticias
                {
                    Id = g.Usuario.Id,
                    TipoUsuario = g.Usuario.TipoUsuario,
                    Nome = g.Usuario.Nome,
                    Email = g.Usuario.Email,
                    Apelido = g.Usuario.Apelido,
                    DataNascimento = g.Usuario.DataNascimento,
                    Senha = g.Usuario.Senha,
                    Bloqueado = g.Usuario.Bloqueado,
                    Ativo = g.Usuario.Ativo,
                    CodigoAtivacao = g.Usuario.CodigoAtivacao,
                    QuantidadeNoticias = g.Noticias.Sum(x => x.Id != null ? 1 : 0)
                })
                .ToListAsync();

            return Ok(usuariosComNoticias);
        }
        catch
        {
            return BadRequest("Erro ao obter os Usuarios com noticias");
        }
    }



}