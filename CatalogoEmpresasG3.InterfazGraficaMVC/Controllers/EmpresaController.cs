using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CatalogoEmpresasG3.LogicaDeNegocio;
using CatalogoEmpresasG3.EntidadesDeNegocio;

namespace CatalogoEmpresasG3.InterfazGraficaMVC.Controllers
{
    public class EmpresaController : Controller
    {
        EmpresaBL empresaBL = new EmpresaBL();//Instancia de acceso a los datos
        ContactoBL contactoBL = new ContactoBL();

        // Acción que muestra el detalle de un registro
        public async Task<ActionResult> Index(Empresa pEmpresa = null)
        {
            if (pEmpresa == null)
                pEmpresa = new Empresa();
            if (pEmpresa.top_aux == 0)//Si no ha puesto cuantos quiere mostrar
                pEmpresa.top_aux = 10; //Trae 10 registros por default
            else if (pEmpresa.top_aux == -1)//Reseteamos
                pEmpresa.top_aux = 0;

            var empresas = await empresaBL.BuscarIncluirContactoAsync(pEmpresa);
            ViewBag.Contactos = await contactoBL.ObtenerTodosAsync();
            return View(empresas);
        }
        // GET: EmpresaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var empresa = await empresaBL.ObtenerPorIDAsync(new Empresa { ID = id });
            empresa.Contacto = await contactoBL.ObtenerPorIDAsync(new Contacto { ID = empresa.ContactoID });
            return View(empresa);
        }

        //Acción que muestra el formulario para crear una nueva empresa
        public async Task<IActionResult> Create()
        {
            ViewBag.Contactos = await contactoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }
        //Acción que recibe datos del formulario y los envía a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa pEmpresa)
        {
            try
            {
                int result = await empresaBL.CrearAsync(pEmpresa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Contactos = await contactoBL.ObtenerTodosAsync();
                return View(pEmpresa);
            }
        }

        // GET: EmpresaController/Edit/5
        public async Task<IActionResult> Edit(Empresa pEmpresa)
        {
            var empresa = await empresaBL.ObtenerPorIDAsync(pEmpresa);
            ViewBag.Contactos = await contactoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View(empresa);
        }

        // POST: EmpresaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa pEmpresa)
        {
            try
            {
                int result = await empresaBL.ModificarAsync(pEmpresa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Contactos = await contactoBL.ObtenerTodosAsync();
                return View(pEmpresa);
            }
        }

        // GET: EmpresaController/Delete/5
        public async Task<IActionResult> Delete(Empresa pEmpresa)
        {
            var empresa = await empresaBL.ObtenerPorIDAsync(pEmpresa);
            ViewBag.Contactos = await contactoBL.ObtenerPorIDAsync(new Contacto { ID = pEmpresa.ContactoID});
            ViewBag.Error = "";
            return View(empresa);
        }

        // POST: EmpresaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Empresa pEmpresa)
        {
            try
            {
                var empresa = await empresaBL.EliminarAsync(pEmpresa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception mk)
            {
                ViewBag.Error = mk.Message;
                var empresa = await empresaBL.ObtenerPorIDAsync(pEmpresa);
                if (empresa == null)
                    empresa = new Empresa();

                if (empresa.ID>0)
                    empresa.Contacto = await contactoBL.ObtenerPorIDAsync(new Contacto { ID = empresa.ContactoID});
                return View(pEmpresa);
            }
        }
    }
}