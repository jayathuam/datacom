﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataCom.modals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Key: TreeViewItem
    {
        public Key()
        {
            Background = null;
        }
        public Key(string header)
        {
            Header = header;
            Background = null;
        }
    }
}
