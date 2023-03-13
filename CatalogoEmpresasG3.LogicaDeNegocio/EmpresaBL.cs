using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatalogoEmpresasG3.AccesoADatos;
using CatalogoEmpresasG3.EntidadesDeNegocio;

namespace CatalogoEmpresasG3.LogicaDeNegocio
{
    public class EmpresaBL
    {
        public async Task<int> CrearAsync(Empresa pEmpresa)
        {
            return await EmpresaDAL.CrearAsync(pEmpresa);
        }
        public async Task<int> ModificarAsync(Empresa pEmpresa)
        {
            return await EmpresaDAL.ModificarAsync(pEmpresa);
        }
        public async Task<int> EliminarAsync(Empresa pEmpresa)
        {
            return await EmpresaDAL.EliminarAsync(pEmpresa);
        }
        public async Task<Empresa> ObtenerPorIDAsync(Empresa pEmpresa)
        {
            return await EmpresaDAL.ObtenerPorIDAsync(pEmpresa);
        }
        public async Task<List<Empresa>> ObtenerTodosAsync()
        {
            return await EmpresaDAL.ObtenerTodosAsync();
        }
        public async Task<List<Empresa>> BuscarAsync(Empresa pEmpresa)
        {
            return await EmpresaDAL.BuscarAsync(pEmpresa);
        }
        public async Task<List<Empresa>> BuscarIncluirContactoAsync(Empresa pEmpresa)
        {
            return await EmpresaDAL.BuscarIncluirContactoAsync(pEmpresa);
        }
    }
}
