using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly DataContext context;

    public LoginController(DataContext Context)
    {
        context = Context;
    }


    [HttpPost]
    public async Task<ActionResult<Usuario>> Login([FromBody] LoginViewModel loginDTO)
    {
        // Busca o usuário pelo email
        var usuario = await context.Usuario.Include(p => p.TipoUsuario).SingleOrDefaultAsync(u => u.Email == loginDTO.email);

        if (usuario == null)
        {
            return BadRequest("Usuário inválido");
        }

        //Verifica se a senha está correta
        // Criptografa a senha do usuário
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Computa o hash da senha
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.senha));

            // Converte o hash para uma string hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            if (sb.ToString() != usuario.Senha)
            {
                return BadRequest("Senha incorreta");
            }
        }

        // Se o usuário e a senha estiverem corretos, retorna os dados do usuário
        return Ok(usuario);
    }
}