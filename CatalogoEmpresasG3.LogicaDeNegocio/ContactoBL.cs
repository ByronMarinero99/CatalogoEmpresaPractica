using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatalogoEmpresasG3.AccesoADatos;
using CatalogoEmpresasG3.EntidadesDeNegocio;

namespace CatalogoEmpresasG3.LogicaDeNegocio
{
    public class ContactoBL
    {
        public async Task<int> CrearAsync(Contacto pContacto)
        {
            return await ContactoDAL.CrearAsync(pContacto);
        }
        public async Task<int> ModificarAsync(Contacto pContacto)
        {
            return await ContactoDAL.ModificarAsync(pContacto);
        }
        public async Task<int> EliminarAsync(Contacto pContacto)
        {
            return await ContactoDAL.EliminarAsync(pContacto);
        }

        public async Task<Contacto> ObtenerPorIDAsync(Contacto pContacto)
        {
            return await ContactoDAL.ObtenerPorIDAsync(pContacto);

        }
        public async Task<List<Contacto>> ObtenerTodosAsync()
        {
            return await ContactoDAL.ObtenerTodosAsync();

        }

        public async Task<List<Contacto>> BuscarAsync(Contacto pContacto)
        {
            return await ContactoDAL.BuscarAsync(pContacto);

        }
    }
}