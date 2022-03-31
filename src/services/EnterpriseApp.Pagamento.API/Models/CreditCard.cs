namespace EnterpriseApp.Pagamento.API.Models
{
    public class CreditCard
    {
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string MonthYearDueDate { get; private set; }
        public string Cvv { get; private set; }

        protected CreditCard() {}

        public CreditCard(string cardName, string cardNumber, string monthYearDueDate, string cvv)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            MonthYearDueDate = monthYearDueDate;
            Cvv = cvv;
        }
    }
}
