namespace NET105_ASM.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<OrderComboDetail>? OrderComboDetails { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
