using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMSBusinessLogic
{
    public class MenusBLL : TMSBaseBLL
    {
        public MenusBLL()
            : base()
        {
        }

        public MenusBLL(string connectionString)
            : base(connectionString)
        {
        }

        public List<TMSMenu> GetSideMenus()
        {
            return (from menu in entities.TMSMenus
                    select menu).ToList();
        }
    }
}
