using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoEmpresasG3.EntidadesDeNegocio
{
    public class Contacto
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El largo máximo son 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(100, ErrorMessage = "El largo máximo son 100 caracteres")]
        public string Email { get; set; }

        [MaxLength(15, ErrorMessage = "El largo máximo son 15 caracteres")]
        //[MinLength(8, ErrorMessage ="El largo mínimo son 8 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El numero de teléfono movil es requerido")]
        [MaxLength(15, ErrorMessage = "El largo máximo son 15 caracteres")]
        [MinLength(8, ErrorMessage = "El largo mínimo son 8 caracteres")]
        public string Movil { get; set; }

        public List<Empresa>  Empresas { get; set; } //propiedad de navegacion

        [NotMapped]
        public int top_aux { get; set; } //propiedad auxiliar que sirve para especificar
                                         //el numero de registros que queremos consultar.
    }
}