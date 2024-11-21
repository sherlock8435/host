using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class MailBoxList : List<MailBox>
    {
        public MailBoxList() { }
        public MailBoxList(IEnumerable<MailBox> list) : base(list) { }
        public List<MailBox> OrderByMailBoxDate()
        {
            if (Count > 0)
                return this.OrderBy(item => item.msgDate).ToList();
            return null;
        }
    }
}
