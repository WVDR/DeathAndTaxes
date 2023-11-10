namespace DeathAndTaxes.API.DataTransferObjects
{
    public class TaxScoreDto
    {
        public int TaxScoreId { get; set; }
        public string User { get; set; }
        public float Income { get; set; }
        public float Tax { get; set; }
        public DateTime DateCaptured { get; set; }
        public int PostalCodeId { get; set; }
    }
}
