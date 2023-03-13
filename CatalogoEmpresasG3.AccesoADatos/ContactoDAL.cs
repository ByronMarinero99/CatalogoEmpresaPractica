using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatalogoEmpresasG3.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace CatalogoEmpresasG3.AccesoADatos
{
    public class ContactoDAL
    {
        public static async Task<int> CrearAsync(Contacto pContacto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto()) //Bloque de ejecución
            {
                bdContexto.Add(pContacto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Contacto pContacto) 
        {
            int result = 0;
            using (var bdContexto = new BDContexto()) //Bloque de ejecución
            {
                var contacto = await bdContexto.Contactos.FirstOrDefaultAsync(c => c.ID == pContacto.ID);

                contacto.Nombre = pContacto.Nombre;
                contacto.Email = pContacto.Email;
                contacto.Telefono = pContacto.Telefono;
                contacto.Movil = pContacto.Movil;

                bdContexto.Update(contacto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Contacto pContacto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto()) //Bloque de ejecución
            {
                var contacto = await bdContexto.Contactos.FirstOrDefaultAsync(c => c.ID == pContacto.ID);
                bdContexto.Contactos.Remove(contacto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Contacto> ObtenerPorIDAsync(Contacto pContacto)
        {
            var contacto = new Contacto();
            using (var bdContexto = new BDContexto())
            {
                contacto = await bdContexto.Contactos.FirstOrDefaultAsync(c => c.ID == pContacto.ID);
            }
            return contacto;
        }

        public static async Task<List<Contacto>> ObtenerTodosAsync()
        {
            var contactos = new List<Contacto>();
            using (var bdContexto = new BDContexto())
            {
                contactos = await bdContexto.Contactos.ToListAsync();
            }
            return contactos;
        }

        internal static IQueryable<Contacto> QuerySelect(IQueryable<Contacto> pQuery, Contacto pContacto)
        {
            if (pContacto.ID > 0)
                pQuery = pQuery.Where( c => c.ID == pContacto.ID);

            if(!string.IsNullOrWhiteSpace(pContacto.Nombre))
                pQuery = pQuery.Where(c => c.Nombre.Contains( pContacto.Nombre));

            if (!string.IsNullOrWhiteSpace(pContacto.Email))
                pQuery = pQuery.Where(c => c.Email.Contains(pContacto.Email));

            if (!string.IsNullOrWhiteSpace(pContacto.Telefono))
                pQuery = pQuery.Where(c => c.Telefono.Contains(pContacto.Telefono));

            if (!string.IsNullOrWhiteSpace(pContacto.Movil))
                pQuery = pQuery.Where(c => c.Movil.Contains(pContacto.Movil));

            pQuery = pQuery.OrderByDescending(c => c.ID).AsQueryable();

            if (pContacto.top_aux > 0)
                pQuery = pQuery.Take(pContacto.top_aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Contacto>> BuscarAsync(Contacto pContacto)
        {
            var contactos = new List<Contacto>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Contactos.AsQueryable();
                select = QuerySelect(select, pContacto);
                contactos = await select.ToListAsync();
            }
            return contactos;
        }


    }
}