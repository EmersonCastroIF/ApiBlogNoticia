using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class NoticiaPublicadaGeralController : ControllerBase
{
    private readonly DataContext context;

    public NoticiaPublicadaGeralController(DataContext Context)
    {
        context = Context;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Noticia model)
    {
        try
        {

            context.Noticia.Add(model);
            await context.SaveChangesAsync();
            return Ok("Noticia foi salva com sucesso");
        }
        catch (Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return BadRequest("Falha ao inserir o Usuario informado: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Noticia>>> Get()
    {
        try
        {
            return Ok(await context.Noticia.Include(p => p.Usuario).ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao obter os Noticias");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Noticia>> Get([FromRoute] int id)
    {
        try
        {
            var noticia = await context.Noticia.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id);
            if (noticia != null)
                return Ok(noticia);
            else
                return NotFound();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Noticia model)
    {
        if (id != model.Id)
            return BadRequest();

        try
        {
            if (await context.Noticia.AnyAsync(p => p.Id == id) == false)
                return NotFound();

            model.DataAlteracao = DateTime.Now; // adiciona a data atual no campo DataAlteracao
            context.Noticia.Update(model);
            await context.SaveChangesAsync();

            if (model.Publicado)
                return Ok("Noticia Publicada com sucesso");
            else
                return Ok("Noticia salva com sucesso");
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Noticia model = await context.Noticia.FindAsync(id);

            if (model == null)
                return NotFound();

            context.Noticia.Remove(model);
            await context.SaveChangesAsync();
            return Ok("Notícia removida com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover a Notícia");
        }
    }
}