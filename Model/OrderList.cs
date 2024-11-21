using System.Collections.Generic;

namespace Model
{
    public class OrderList : List<Order>
    {
        public OrderList() { }
        public OrderList(IEnumerable<Order> orders) : base(orders) { }
    }
}
