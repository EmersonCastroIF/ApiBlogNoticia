using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnviaEmail;
using System.Text;
using System.Text;
using System.Security.Cryptography;

[Route("api/[controller]")]
[ApiController]
public class RedefineSenhaController : ControllerBase
{
    private readonly DataContext dataContext;

    public RedefineSenhaController(DataContext Context)
    {
        dataContext = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> ConfirmaAlteracaoSenha([FromBody] ConfirmaAlteracaoSenhaViewModel Usuario)
    {
        try
        {
            //Gera código aleatório para o usuário não usar o mesmo código mais de uma vez
            const string CaracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int tamanho = 10;
            var random = new Random();

            var sb = new StringBuilder();
            for (int i = 0; i < tamanho; i++)
            {
                int index = random.Next(CaracteresPermitidos.Length);
                sb.Append(CaracteresPermitidos[index]);
            }

            string codigo = sb.ToString();

            var DadosUsuario = await dataContext.Usuario.FindAsync(Usuario.IdUser);
            if (DadosUsuario == null)
            {
                return BadRequest($"Usuário não foi encontrado");
            }

            if (DadosUsuario.CodigoRedefineSenha == Usuario.CodigoAtivacao)
            {
                // Criptografa a senha do usuário
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Computa o hash da senha
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Usuario.Senha));

                    // Converte o hash para uma string hexadecimal
                    StringBuilder sb2 = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb2.Append(bytes[i].ToString("x2"));
                    }

                    DadosUsuario.Senha = sb2.ToString();
                }

                DadosUsuario.CodigoRedefineSenha = codigo;

                try
                {
                    await dataContext.SaveChangesAsync(); // atualiza o registro no banco de dados
                }
                catch
                {
                    return BadRequest("Falha ao confirmar o código");
                }

                return Ok("Senha Alterada com sucesso !!!");
            }
            else
            {
                return BadRequest("Código Inválido !!");
            }
        }
        catch
        {
            return BadRequest("Falha ao Confirmar alteração de senha");
        }
    }
}