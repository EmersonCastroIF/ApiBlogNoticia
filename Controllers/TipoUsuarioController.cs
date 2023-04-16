using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private readonly DataContext context;

    public TipoUsuarioController(DataContext Context)
    {
        context = Context;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] TipoUsuario model)
    {
        try
        {
            context.TipoUsuario.Add(model);
            await context.SaveChangesAsync();
            return Ok("Tipo de Usuário salvo com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao inserir o Tipo de Usuário informado");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoUsuario>>> Get()
    {
        try
        {
            return Ok(await context.TipoUsuario.ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao obter os Tipos de Usuários");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] TipoUsuario model)
    {
        if (id != model.Id)
            return BadRequest();

        try
        {
            if (await context.TipoUsuario.AnyAsync(p => p.Id == id) == false)
                return NotFound();

            context.TipoUsuario.Update(model);
            await context.SaveChangesAsync();
            return Ok("Tipo de Usuário Atualizado com sucesso");
        }
        catch
        {
            return BadRequest();
        }
    }
}