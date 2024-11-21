using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class CitiesList : List<Cities>
    {
        public CitiesList() { }
        public CitiesList(IEnumerable<Cities> cities) : base(cities) { }
        public List<Cities> OrderByCityName()
        {
            if (Count > 0)
                return this.OrderBy(item => item.CityName).ToList();
            return null;
        }
    }
}
