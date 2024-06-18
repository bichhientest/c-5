namespace NET105_ASM.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string PaymentMethod { get; set; } // "MoMo", "VN Pay", "Zalo Pay", etc.
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
