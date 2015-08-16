﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockTiles.Interfaces
{
    public interface ISplitDockTile : IDockTile
    {
        IDockTile LeftNode { get; set; }
        IDockTile RightNode { get; set; }
    }
}
