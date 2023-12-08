using System.ComponentModel.DataAnnotations;

namespace BecarioAPI.Models.Solicitantes
{
    //validacion para que no se repitan las notas
    #region Validacion para que no se repitan las notas
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoDuplicadosSolicitanteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (BecarioDBContext)validationContext.GetService(typeof(BecarioDBContext));
            var solicitante = (Solicitante)validationContext.ObjectInstance;

            // Validamos que no exista un solicitante con los mismos valores
            if (dbContext.Solicitantes.Any(s =>
                s.Nombre == solicitante.Nombre &&
                s.Apellido == solicitante.Apellido &&
                s.FechaNacimiento == solicitante.FechaNacimiento &&
                s.EstadoCivil == solicitante.EstadoCivil &&
                s.Dui == solicitante.Dui &&
                s.Pasaporte == solicitante.Pasaporte &&
                s.FechaExpedicionPasaporte == solicitante.FechaExpedicionPasaporte &&
                s.LugardeExpedicion == solicitante.LugardeExpedicion &&
                s.Direccion == solicitante.Direccion &&
                s.Email == solicitante.Email &&
                s.Telefono == solicitante.Telefono))
            {
                return new ValidationResult("Ya existe un solicitante con los mismos valores.");
            }

            return ValidationResult.Success;
        }
    }

    #endregion


    #region Validacion Fecha Futura
    public class FechaFuturaValidaAttribute : ValidationAttribute
    {
        //Validacion de la fecha de nacimiento no sea mayor a la fecha actual
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? fechaNacimiento = (DateTime?)value;

            if (fechaNacimiento.HasValue && fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult("La fecha no puede estar en el futuro.");
            }

            return ValidationResult.Success;
        }
    }
    #endregion

    #region Validacion Estado Civil
    public class EstadoCivilValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Validacion de estado civil
            var estadoCivil = value as string;
            //Validacion de estado civil
            if (estadoCivil != null)
            {
                var estadosValidos = new string[] { "Soltero", "Casado", "Divorciado", "Viudo" }; // Puedes agregar más estados según sea necesario
                if (!Array.Exists(estadosValidos, estado => estado.Equals(estadoCivil, StringComparison.OrdinalIgnoreCase)))
                {
                    return new ValidationResult("El estado civil no es válido.");
                }
            }
            return ValidationResult.Success;
        }
    }
    #endregion

    #region Tabla Solicitante
    public class Solicitante
    {
        [Key]
        public int IdSolicitante { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El nombre no cumple los requerimientos")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El Apellido no cumple los requerimientos")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Apellido { get; set; }

        [Required]
        [FechaFuturaValida(ErrorMessage = "La fecha de nacimiento no puede estar en el futuro.")]
        public DateTime FechaNacimiento { get; set; }

        [EstadoCivilValido(ErrorMessage = "El estado civil no es válido.")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? EstadoCivil { get; set; }

        [RegularExpression("^[0-9]{8}-[0-9]{1}$", ErrorMessage = "Formato de DUI no válido. Ejemplo: 12345678-9")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Dui { get; set; }

        [StringLength(12, MinimumLength = 9, ErrorMessage = "El pasaporte no cumple los requerimientos. Ejemplo: ABC123456")]
        [RegularExpression("^[A-Z0-9]{9}$", ErrorMessage = "Formato de pasaporte no válido. Ejemplo: ABC123456")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Pasaporte { get; set; }

        [Required(ErrorMessage = "La fecha de expedición del pasaporte es requerida.")]
        [FechaFuturaValida(ErrorMessage = "La fecha de expedición del pasaporte no puede estar en el futuro.")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public DateTime FechaExpedicionPasaporte { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "El lugar de expedición del pasaporte no cumple los requerimientos.")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? LugardeExpedicion { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "La dirección no cumple los requerimientos.")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Direccion { get; set; }


        [EmailAddress(ErrorMessage = "El email no es válido.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El email no cumple los requerimientos.")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Email { get; set; }

        [RegularExpression("^[0-9]{4}-[0-9]{4}$", ErrorMessage = "Formato de teléfono no válido. Ejemplo: 1234-5678")]
        [NoDuplicadosSolicitante(ErrorMessage = "Ya existe un solicitante con los mismos valores.")]
        public string? Telefono { get; set; }
    }
    #endregion
}
