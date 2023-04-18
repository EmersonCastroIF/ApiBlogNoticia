using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<TipoCurso> TipoCursos { get; set; } = null!;
    public DbSet<Usuario> Usuario { get; set; } = null!;
    public DbSet<TipoUsuario> TipoUsuario { get; set; } = null!;
    public DbSet<Noticia> Noticia { get; set; } = null!;
    public DbSet<Email> Email { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .Property(p => p.DataNascimento)
            .HasColumnType("date");
    }
}