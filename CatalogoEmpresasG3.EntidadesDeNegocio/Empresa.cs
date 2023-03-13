using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoEmpresasG3.EntidadesDeNegocio
{
    public class Empresa
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El largo máximo son 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El rubro es requerido")]
        [MaxLength(50, ErrorMessage = "El largo máximo son 50 caracteres")]
        public string Rubro { get; set; }

        [Required(ErrorMessage = "La categoría es requerido")]
        [MaxLength(25, ErrorMessage = "El largo máximo son 25 caracteres")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "El depatamento es requerido")]
        [MaxLength(25, ErrorMessage = "El largo máximo son 25 caracteres")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "El ID de contacto es requerido")]
        [ForeignKey("Contacto")]
        [Display(Name ="ID del contacto")]
        public int ContactoID { get; set; }

        public  Contacto Contacto { get; set; } //propiedad de navegacion
        [NotMapped]
        public int top_aux { get; set; } //propiedad auxiliar que sirve para especificar
                                         //el numero de registros que queremos consultar.
    }
}