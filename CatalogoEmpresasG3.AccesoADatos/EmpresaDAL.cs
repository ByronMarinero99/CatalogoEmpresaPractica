using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatalogoEmpresasG3.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace CatalogoEmpresasG3.AccesoADatos
{
    public class EmpresaDAL
    {
        public static async Task<int> CrearAsync(Empresa pEmpresa)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pEmpresa);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Empresa pEmpresa)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var empresa = await bdContexto.Empresas.FirstOrDefaultAsync(e => e.ID == pEmpresa.ID);
                empresa.Nombre = pEmpresa.Nombre;
                empresa.Rubro = pEmpresa.Rubro;
                empresa.Categoria = pEmpresa.Categoria;
                empresa.Departamento = pEmpresa.Departamento;
                empresa.ContactoID = pEmpresa.ContactoID;

                bdContexto.Update(empresa);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Empresa pEmpresa)
        {
            int result = 0;
            using (var bdContexto = new BDContexto()) //Bloque de ejecución
            {
                var empresa = await bdContexto.Empresas.FirstOrDefaultAsync(e => e.ID == pEmpresa.ID);
                bdContexto.Empresas.Remove(empresa);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Empresa> ObtenerPorIDAsync(Empresa pEmpresa)
        {
            var empresa = new Empresa();
            using (var bdContexto = new BDContexto())
            {
                empresa = await bdContexto.Empresas.FirstOrDefaultAsync(e => e.ID == pEmpresa.ID);
            }
            return empresa;
        }

        public static async Task<List<Empresa>> ObtenerTodosAsync()
        {
            var empresas = new List<Empresa>();
            using (var bdContexto = new BDContexto())
            {
                empresas = await bdContexto.Empresas.ToListAsync();
            }
            return empresas;
        }

        internal static IQueryable<Empresa> QuerySelect(IQueryable<Empresa> pQuery, Empresa pEmpresa)
        {
            if (pEmpresa.ID > 0)
                pQuery = pQuery.Where(e => e.ID == pEmpresa.ID);

            if (pEmpresa.ContactoID > 0)
                pQuery = pQuery.Where(e => e.ContactoID == pEmpresa.ContactoID);

            if (!string.IsNullOrWhiteSpace(pEmpresa.Nombre))
                pQuery = pQuery.Where(e => e.Nombre.Contains(pEmpresa.Nombre));

            if (!string.IsNullOrWhiteSpace(pEmpresa.Rubro))
                pQuery = pQuery.Where(e => e.Rubro.Contains(pEmpresa.Rubro));

            if (!string.IsNullOrWhiteSpace(pEmpresa.Categoria))
                pQuery = pQuery.Where(e => e.Categoria.Contains(pEmpresa.Categoria));

            if (!string.IsNullOrWhiteSpace(pEmpresa.Departamento))
                pQuery = pQuery.Where(e => e.Departamento.Contains(pEmpresa.Departamento));

            pQuery = pQuery.OrderByDescending(e => e.ID).AsQueryable();

            if (pEmpresa.top_aux > 0)
                pQuery = pQuery.Take(pEmpresa.top_aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Empresa>> BuscarAsync(Empresa pEmpresa)
        {
            var empresas = new List<Empresa>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Empresas.AsQueryable();
                select = QuerySelect(select, pEmpresa);
                empresas = await select.ToListAsync();
            }
            return empresas;
        }

        public static async Task<List<Empresa>> BuscarIncluirContactoAsync(Empresa pEmpresa)
        {
            var empresas = new List<Empresa>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Empresas.AsQueryable();
                select = QuerySelect(select, pEmpresa).Include(e => e.Contacto).AsQueryable();
                empresas = await select.ToListAsync();
            }
            return empresas;
        }
    }
}