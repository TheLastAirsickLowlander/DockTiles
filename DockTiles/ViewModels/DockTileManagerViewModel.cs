﻿using DockTiles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DockTiles.Models;

namespace DockTiles.ViewModels
{
    public class DockTileManagerViewModel : ViewModelBase, IDockTileManager
    {
        #region Properties
        private IDockTile _TreeRoot;
        public IDockTile TreeRoot
        {
            get { return _TreeRoot; }
            set
            {
                if (_TreeRoot != value)
                {
                    _TreeRoot = value;
                    OnPropertyChanged("TreeRoot");
                }
            }
        }
        #endregion //Properties

        #region Members
        protected Dictionary<object, IDockTile> ObjectToDocktileMap = new Dictionary<object, IDockTile>();
        #endregion //Members

        #region Constructor
        /// <summary>
        /// Creates the manager that will be used to be displayed in a ContentPresenter
        /// </summary>
        /// <param name="rootView"> The root ViewModel to be displayed on Items</param>
        public DockTileManagerViewModel(Object rootView)
        {
            IDockTile dockTile = new LeafViewModel() { Item = rootView };
            //add the base item to the map
            ObjectToDocktileMap.Add(rootView, dockTile);

            _TreeRoot = new RootDockTile() { Item = dockTile };

        }
        #endregion //Constructor

        #region Methods
        public void AddTile(Object baseItem, Object item, DockTileDirection dockDirection)
        {
            //if we have the item in the map
            if (ObjectToDocktileMap.ContainsKey(baseItem))
            {
                IDockTile baseDockTile = null;
                IDockTile dockedItem = new LeafViewModel() { Item = item };
                ObjectToDocktileMap.TryGetValue(baseItem, out baseDockTile);
                ObjectToDocktileMap.Add(item, dockedItem);
                if (baseDockTile.Parent != null)
                {
                    baseDockTile.Dock(dockedItem, dockDirection);
                }

            }

        }


        public bool RemoveTile(Object tile)
        {
            IDockTile baseDockTile = null;
            ObjectToDocktileMap.TryGetValue(tile, out baseDockTile);
            if (baseDockTile != null)
            {
                baseDockTile.Parent.RemoveDockTile(baseDockTile);
                return true;
            }
            return false;
        }

        #endregion //Methods
    }
}