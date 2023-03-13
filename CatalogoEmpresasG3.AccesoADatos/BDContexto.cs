using CatalogoEmpresasG3.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoEmpresasG3.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contacto> Contactos { get; set; }//<> Son picoparéntesis o
        public DbSet<Empresa> Empresas { get; set; } //paréntesis angular

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=.;Initial Catalog=CatalogoEmpresaBD;Integrated Security=True");
        }

    }
}