namespace Model
{
    public class Order
    {
        public int ItemId { get; set; }
        public string UEmail { get; set; }
        public string VisaNumber { get; set; }
        public int Qnty { get; set; }
        public int Price { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
