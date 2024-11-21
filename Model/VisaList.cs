using System.Collections.Generic;

namespace Model
{
    public class VisaList : List<Visa>
    {
        public VisaList() { }
        public VisaList(IEnumerable<Visa> visaList) : base(visaList) { }
    }
}
