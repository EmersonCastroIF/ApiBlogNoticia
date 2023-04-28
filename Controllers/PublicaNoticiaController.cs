using EnviaEmail;
using Microsoft.AspNetCore.Mvc;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class PublicaNoticiaController : ControllerBase
{

    private readonly DataContext context;

    public PublicaNoticiaController(DataContext Context)
    {
        context = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Noticia>> PublicaNoticia([FromBody] PublicaNoticiaViewModel Noticia)
    {
        try
        {

            var NoticiaModel = await context.Noticia.FindAsync(Noticia.IdNoticia);
            if (NoticiaModel == null)
            {
                return BadRequest($"Noticia não foi encontrado");
            }



            try
            {
                NoticiaModel.Publicado = true;
                NoticiaModel.DataPublicacao = DateTime.Now;
                await context.SaveChangesAsync(); // atualiza o registro no banco de dados
            }
            catch
            {
                return BadRequest("Falha ao Publicar Noticia");
            }

            return Ok("Noticia Publicada com sucesso !!!");


        }
        catch
        {
            return BadRequest("Falha ao Publicar Noticia");
        }
    }
}