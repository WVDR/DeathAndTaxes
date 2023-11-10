namespace DeathAndTaxes.App.Models
{
    public class TaxScoreModel
    {
        public int TaxScoreId { get; set; }
        public string User { get; set; }
        public float Income { get; set; }
        public float Tax { get; set; }
        public DateTime DateCaptured { get; set; }
        public string PostalCode { get; set; }
    }
}
