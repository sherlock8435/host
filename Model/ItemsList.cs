using System.Collections.Generic;

namespace Model
{
    public class ItemsList : List<Items>
    {
        public ItemsList() { }
        public ItemsList(IEnumerable<Items> items) { }
    }
}
