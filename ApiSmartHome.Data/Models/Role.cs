using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiSmartHome.Data.Models
{
    public class Role(RoleType type)
    {
        public int Id { get; private set; } = (int)type;
        public string Name { get; private set; } = type.ToString();

        public override string ToString()
        {
            return Name;
        }
    }
}
