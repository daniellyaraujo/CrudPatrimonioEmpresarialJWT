using CrudPatrimonioEmpresarialJWT.Models.Marca;
using CrudPatrimonioEmpresarialJWT.Models.Patrimonio;
using CrudPatrimonioEmpresarialJWT.Models.Usuario;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudPatrimonioEmpresarialJWT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MarcaModel> Marca { get; set; }
        public DbSet<PatrimonioModel> Patrimonio { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}