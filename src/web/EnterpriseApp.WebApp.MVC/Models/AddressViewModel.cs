using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class AddressViewModel
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        public string Complement { get; set; }

        [Required]
        public string Neighbourhood { get; set; }

        [Required]
        [DisplayName("CEP")]
        public string Cep { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        public override string ToString()
        {
            return $"{Street}, {Number} {Complement} - {Neighbourhood} - {City} - {State}";
        }
    }
}
