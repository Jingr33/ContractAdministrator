namespace Contract_Administrator.Models
{
    /// <summary>
    /// Model pro pomocnou tabulku mezi Contract a Adviser.
    /// </summary>
    public class ContractAdviser
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int AdviserId { get; set; }
        public Adviser Adviser { get; set; }
        public bool IsAdmin { get; set; }
    }
}
