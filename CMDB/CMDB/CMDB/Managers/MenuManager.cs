using System;
using System.Collections.Generic;
using System.Text;

namespace CMDB.Managers
{
    public class MenuManager
    {
        private readonly List<string> _menuItems;
        
        public MenuManager(List<string> menuItems)
        {
            _menuItems = menuItems;
        }

    }
}
