using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly DataContext dataContext;

    public UsuarioController(DataContext Context)
    {
        dataContext = Context;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Usuario model)
    {
        try
        {
            var usuarioExistente = await dataContext.Usuario.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (usuarioExistente != null)
            {
                return BadRequest($"Email já cadastrado!");
            }        

            var tipoUsuario = await dataContext.TipoUsuario.FindAsync(model.TipoUsuario.Id);
            if (tipoUsuario == null)
            {
                return BadRequest($"O TipoUsuario com ID {model.TipoUsuario.Id} não foi encontrado");
            }

            model.TipoUsuario = tipoUsuario; // associa o TipoUsuario ao Usuario
            dataContext.Usuario.Add(model);
            await dataContext.SaveChangesAsync();
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
            return Ok(await dataContext.Usuario.Include(p => p.TipoUsuario).ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao obter os Usuarios");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> Get([FromRoute] int id)
    {
        try
        {
            if (await dataContext.Usuario.AnyAsync(p => p.Id == id))
                return Ok(await dataContext.Usuario.FindAsync(id));
            else
                return NotFound();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Usuario model)
    {
        if (id != model.Id)
            return BadRequest();

        try
        {
            if (await dataContext.Usuario.AnyAsync(p => p.Id == id) == false)
                return NotFound();

            var tipoUsuario = await dataContext.TipoUsuario.FindAsync(model.TipoUsuario.Id);
            if (tipoUsuario == null)
            {
                return BadRequest($"O TipoUsuario com ID {model.TipoUsuario.Id} não foi encontrado");
            }

            model.TipoUsuario = tipoUsuario; // associa o TipoUsuario ao Usuario                

            dataContext.Usuario.Update(model);
            await dataContext.SaveChangesAsync();
            return Ok("Usuario Atualizado com sucesso");
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
            Usuario model = await dataContext.Usuario.FindAsync(id);

            if (model == null)
                return NotFound();

            dataContext.Usuario.Remove(model);
            await dataContext.SaveChangesAsync();
            return Ok("Usuario removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover o Usuario");
        }
    }
}