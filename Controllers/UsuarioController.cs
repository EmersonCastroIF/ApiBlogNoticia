using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnviaEmail;
using System.Text;
using System.Security.Cryptography;

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
            var codigo = "";
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

            if (tipoUsuario.Id == 2)
            {
                //Gera código aleatório
                const string CaracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                const int tamanho = 5;
                var random = new Random();

                var sb = new StringBuilder();
                for (int i = 0; i < tamanho; i++)
                {
                    int index = random.Next(CaracteresPermitidos.Length);
                    sb.Append(CaracteresPermitidos[index]);
                }
                codigo = sb.ToString();

                // Criptografa a senha do usuário
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Computa o hash da senha
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(codigo));

                    // Converte o hash para uma string hexadecimal
                    StringBuilder sb3 = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb3.Append(bytes[i].ToString("x2"));
                    }
                    model.Senha = sb3.ToString();
                }


                model.TipoUsuario = tipoUsuario; // associa o TipoUsuario ao Usuario
                dataContext.Usuario.Add(model);
                await dataContext.SaveChangesAsync();

                SendEmail.Send(model.Email, codigo, model.TipoUsuario.Id, "CadastroAutor");
                return Ok("E-mail ao autor com sucesso !!!");
            }
            else
            {
                // Criptografa a senha do usuário
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Computa o hash da senha
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(model.Senha));

                    // Converte o hash para uma string hexadecimal
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb.Append(bytes[i].ToString("x2"));
                    }
                    model.Senha = sb.ToString();
                }
            }

            model.TipoUsuario = tipoUsuario; // associa o TipoUsuario ao Usuario
            dataContext.Usuario.Add(model);
            await dataContext.SaveChangesAsync();

            return Ok("Usuario salvo com sucesso");
        }
        catch (Exception ex)
        {
            // Captura a mensagem da inner exception, se existir
            string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return BadRequest("Falha ao inserir o Usuario informado: " + errorMessage);
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