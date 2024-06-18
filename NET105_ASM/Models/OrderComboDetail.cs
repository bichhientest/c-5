namespace NET105_ASM.Models
{
    public class OrderComboDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ComboId { get; set; }
        public Combo Combo { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
