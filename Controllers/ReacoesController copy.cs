using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ReacoesController : ControllerBase
{
    private readonly DataContext dataContext;

    public ReacoesController(DataContext Context)
    {
        dataContext = Context;
    }

    [HttpPost]
    public async Task<ActionResult<Reacoes>> Post([FromBody] Reacoes model)
    {
        try
        {
            var existingReacao = await dataContext.Reacoes.FirstOrDefaultAsync(r => r.NoticiaId == model.NoticiaId && r.UsuarioId == model.UsuarioId);

            if (existingReacao != null)
            {
                dataContext.Reacoes.Remove(existingReacao);
            }

            dataContext.Reacoes.Add(model);
            await dataContext.SaveChangesAsync();

            //Atualiza o total de likes e deslikes na notícia relacionada a essa reação
            var noticia = await dataContext.Noticia.FirstOrDefaultAsync(n => n.Id == model.NoticiaId);
            if (noticia != null)
            {
                var likes = await dataContext.Reacoes.CountAsync(r => r.NoticiaId == model.NoticiaId && r.Like == true);
                var deslikes = await dataContext.Reacoes.CountAsync(r => r.NoticiaId == model.NoticiaId && r.DesLike == true);
                noticia.QtdLike = likes;
                noticia.QtdDesLike = deslikes;
                await dataContext.SaveChangesAsync();
            }

            return Ok("Reação salva com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest("Falha ao salvar Reação: " + ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Reacoes>> BuscaReacao([FromBody] ReacoesUserViewModel Reacoes)
    {
        try
        {

            var ReacaoUser = await dataContext.Reacoes.CountAsync(r => r.NoticiaId == Reacoes.IdNoticia && r.UsuarioId == Reacoes.IdUser);

            var likes = ReacaoUsser.
            var deslikes = await dataContext.Reacoes.CountAsync(r => r.NoticiaId == model.NoticiaId && r.DesLike == true);
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

            DadosUsuario.CodigoAtivacao = codigo;

            try
            {
                await context.SaveChangesAsync(); // atualiza o registro no banco de dados
            }
            catch
            {
                return BadRequest("Falha ao salvar o código");
            }


            SendEmail.Send(DadosUsuario.Email, codigo);
            return Ok("E-mail enviado com sucesso !!!");
        }
        catch
        {
            return BadRequest("Falha ao enviar e-mail");
        }
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Comentario>>> Get()
    // {
    //     try
    //     {
    //         return Ok(await dataContext.Comentario.ToListAsync());
    //     }
    //     catch
    //     {
    //         return BadRequest("Erro ao obter os Usuarios");
    //     }
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<List<Comentario>>> Get([FromRoute] int id)
    // {
    //     try
    //     {
    //         var comentarios = await dataContext.Comentario
    //         .Where(p => p.IdNoticia == id)
    //         .OrderBy(p => p.Data)
    //         .Include(p => p.Usuario)
    //         .ToListAsync();

    //         if (comentarios.Any())
    //         {
    //             return Ok(comentarios);
    //         }
    //         else
    //         {
    //             return NotFound();
    //         }
    //     }
    //     catch
    //     {
    //         return BadRequest();
    //     }
    // }

    // [HttpPut]
    // public async Task<ActionResult<Comentario>> AtualizarComentario([FromBody] ComentarioViewModel Comentario)
    // {
    //     try
    //     {

    //         var DadosComentario = await dataContext.Comentario.FindAsync(Comentario.Id);
    //         if (DadosComentario == null)
    //         {
    //             return BadRequest($"Comentário não foi encontrado");
    //         }


    //         DadosComentario.Texto = Comentario.Texto;

    //         try
    //         {
    //             await dataContext.SaveChangesAsync(); // atualiza o registro no banco de dados
    //             return Ok("Comentário atualizado com sucesso !!!");
    //         }
    //         catch
    //         {
    //             return BadRequest("Falha ao atualizar o Comentário");
    //         }
    //     }
    //     catch
    //     {
    //         return BadRequest("Falha ao atualizar o Comentário");
    //     }
    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Comentario([FromRoute] int id)
    // {
    //     try
    //     {
    //         Comentario model = await dataContext.Comentario.FindAsync(id);

    //         if (model == null)
    //             return NotFound();

    //         dataContext.Comentario.Remove(model);
    //         await dataContext.SaveChangesAsync();
    //         return Ok("Comentário removido com sucesso");
    //     }
    //     catch
    //     {
    //         return BadRequest("Falha ao remover o Comentário");
    //     }
    // }
}