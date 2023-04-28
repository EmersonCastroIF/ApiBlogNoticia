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

    
}