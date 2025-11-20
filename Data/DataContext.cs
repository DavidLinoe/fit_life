using fit_life.Models;
using Microsoft.EntityFrameworkCore;


namespace fit_life.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //dotnet build 
        //dotnet ef database update      --  esse é equivalente ao Update-Database do Package Manager Console

        public DbSet<Exercicio> ExercicioTable { get; set; }
        public DbSet<Treino> TreinoTable { get; set; }
        public DbSet<Habito> HabitoTable { get; set; }


    }
}
