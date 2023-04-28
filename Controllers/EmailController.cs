using EnviaEmail;
using Microsoft.AspNetCore.Mvc;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{

    private readonly DataContext context;

    public EmailController(DataContext Context)
    {
        context = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> EnviaEmail([FromBody] EmailViewModel Usuario)
    {
        try
        {

            var DadosUsuario = await context.Usuario.FindAsync(Usuario.IdUser);
            if (DadosUsuario == null)
            {
                return BadRequest($"Usuário não foi encontrado");
            }

            //Gera código aleatório
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

            if (Usuario.TipoCodigo == "Reenvio-Código")
            {
                DadosUsuario.CodigoAtivacao = codigo;
            }
            else if (Usuario.TipoCodigo == "Redefinicao-Senha")
            {
                DadosUsuario.CodigoRedefineSenha = codigo;
            }
            else if (Usuario.TipoCodigo == "Redefinicao-Email")
            {
                DadosUsuario.CodigoRedefineEmail = codigo;
            }


            try
            {
                await context.SaveChangesAsync(); // atualiza o registro no banco de dados
            }
            catch
            {
                return BadRequest("Falha ao salvar o código");
            }


            SendEmail.Send(DadosUsuario.Email, codigo, 1, Usuario.TipoCodigo);
            return Ok("E-mail enviado com sucesso !!!");
        }
        catch
        {
            return BadRequest("Falha ao enviar e-mail");
        }
    }
}