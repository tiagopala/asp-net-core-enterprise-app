namespace EnterpriseApp.BFF.Compras.Models
{
    public class AddressDTO
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighbourhood { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
