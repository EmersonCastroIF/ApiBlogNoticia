using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuario { get; set; } = null!;
    public DbSet<TipoUsuario> TipoUsuario { get; set; } = null!;
    public DbSet<Noticia> Noticia { get; set; } = null!;
    public DbSet<Comentario> Comentario { get; set; } = null!;
    public DbSet<Reacoes> Reacoes { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");
            entity.Property(e => e.Id).HasColumnName("Id");
        });

        modelBuilder.Entity<Usuario>()
            .Property(p => p.DataNascimento)
            .HasColumnType("date");
    }
}

public class LoginViewModel
{
    public string email { get; set; }
    public string senha { get; set; }
}

public class EmailViewModel
{
    public int IdUser { get; set; }
    public string TipoCodigo { get; set; }
}

public class ConfirmaCadastroViewModel
{
    public int IdUser { get; set; }
    public string CodigoAtivacao { get; set; }
}

public class ConfirmaAlteracaoSenhaViewModel
{
    public int IdUser { get; set; }
    public string CodigoAtivacao { get; set; }
    public string Senha { get; set; }
}

public class ConfirmaAlteracaoEmailViewModel
{
    public int IdUser { get; set; }
    public string CodigoAtivacao { get; set; }
    public string Email { get; set; }
}

public class RedefineNomeAutorViewModel
{
    public int IdUser { get; set; }
    public string Nome { get; set; }
}

public class PublicaNoticiaViewModel
{
    public int IdNoticia { get; set; }
}

public class ComentarioViewModel
{
    public int Id { get; set; }
    public string Texto { get; set; }
}

public class ReacoesUserViewModel
{
    public int IdUser { get; set; }
    public int IdNoticia { get; set; }
}


public class UsuarioComNoticias : Usuario
{
    public int QuantidadeNoticias { get; set; }
}