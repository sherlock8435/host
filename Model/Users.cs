using System;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Users
    {
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPassword { get; set; }
        public Cities cityId { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Telephone { get; set; }
    }
}
