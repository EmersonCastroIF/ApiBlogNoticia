using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly DataContext context;

    public UsuarioController(DataContext Context)
    {
        context = Context;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Usuario model)
    {
        try
        {
            var tipoUsuario = await context.TipoUsuario.FindAsync(model.TipoUsuario.Id);
            if (tipoUsuario == null)
            {
                return BadRequest($"O TipoUsuario com ID {model.TipoUsuario.Id} não foi encontrado");
            }

            model.TipoUsuario = tipoUsuario; // associa o TipoUsuario ao Usuario
            context.Usuario.Add(model);
            await context.SaveChangesAsync();
            return Ok("Usuario salvo com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest("Falha ao inserir o Usuario informado: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> Get()
    {
        try
        {
            return Ok(await context.Usuario.Include(p => p.TipoUsuario).ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao obter os Usuarios");
        }
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<TipoCurso>> Get([FromRoute] int id)
    // {
    //     try
    //     {
    //         if (await context.TipoCursos.AnyAsync(p => p.Id == id))
    //             return Ok(await context.TipoCursos.FindAsync(id));
    //         else
    //             return NotFound();
    //     }
    //     catch
    //     {
    //         return BadRequest();
    //     }
    // }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Usuario model)
    {
        if (id != model.Id)
            return BadRequest();

        try
        {
            if (await context.Usuario.AnyAsync(p => p.Id == id) == false)
                return NotFound();

            var tipoUsuario = await context.TipoUsuario.FindAsync(model.TipoUsuario.Id);
            if (tipoUsuario == null)
            {
                return BadRequest($"O TipoUsuario com ID {model.TipoUsuario.Id} não foi encontrado");
            }

            model.TipoUsuario = tipoUsuario; // associa o TipoUsuario ao Usuario                

            context.Usuario.Update(model);
            await context.SaveChangesAsync();
            return Ok("Usuario Atualizado com sucesso");
        }
        catch
        {
            return BadRequest();
        }
    }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete([FromRoute] int id)
    // {
    //     try
    //     {
    //         TipoCurso model = await context.TipoCursos.FindAsync(id);

    //         if (model == null)
    //             return NotFound();

    //         context.TipoCursos.Remove(model);
    //         await context.SaveChangesAsync();
    //         return Ok("Tipo de curso removido com sucesso");
    //     }
    //     catch
    //     {
    //         return BadRequest("Falha ao remover o tipo de curso");
    //     }
    // }
}