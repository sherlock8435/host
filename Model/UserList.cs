using System.Collections.Generic;

namespace Model
{
    public class UserList : List<Users>
    {
        public UserList() { }
        public UserList(IEnumerable<Users> users) : base(users) { }
    }
}
