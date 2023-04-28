using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ComentarioController : ControllerBase
{
    private readonly DataContext dataContext;

    public ComentarioController(DataContext Context)
    {
        dataContext = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Comentario>> Post([FromBody] Comentario model)
    {
        try
        {
            dataContext.Comentario.Add(model);
            await dataContext.SaveChangesAsync();
            return Ok("Comentário salvo com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest("Falha ao inserir o Comentário: " + ex.Message);
        }
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Comentario>>> Get()
    {
        try
        {
            return Ok(await dataContext.Comentario.ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao obter os Usuarios");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Comentario>>> Get([FromRoute] int id)
    {
        try
        {
            var comentarios = await dataContext.Comentario
            .Where(p => p.IdNoticia == id)
            .OrderBy(p => p.Data)
            .Include(p => p.Usuario)
            .ToListAsync();

            if (comentarios.Any())
            {
                return Ok(comentarios);
            }
            else
            {
                return NotFound();
            }
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult<Comentario>> AtualizarComentario([FromBody] ComentarioViewModel Comentario)
    {
        try
        {

            var DadosComentario = await dataContext.Comentario.FindAsync(Comentario.Id);
            if (DadosComentario == null)
            {
                return BadRequest($"Comentário não foi encontrado");
            }


            DadosComentario.Texto = Comentario.Texto;

            try
            {
                await dataContext.SaveChangesAsync(); // atualiza o registro no banco de dados
                return Ok("Comentário atualizado com sucesso !!!");
            }
            catch
            {
                return BadRequest("Falha ao atualizar o Comentário");
            }
        }
        catch
        {
            return BadRequest("Falha ao atualizar o Comentário");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Comentario([FromRoute] int id)
    {
        try
        {
            Comentario model = await dataContext.Comentario.FindAsync(id);

            if (model == null)
                return NotFound();

            dataContext.Comentario.Remove(model);
            await dataContext.SaveChangesAsync();
            return Ok("Comentário removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover o Comentário");
        }
    }
}