using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataProvider.Data
{
    public class UserEquipGhostInfo : DataObject
    {
        private int _UserID;
        private int _BagType;
        private int _Place;
        private int _Level;
        private int _TotalGhost;
        public int UserID
        {
            get {
                return _UserID;
            }

            set
            {
                _UserID = value;
                _isDirty = true;
            }
        }
        public int BagType {
            get
            {
                return _BagType;
            }

            set
            {
                _BagType = value;
                _isDirty = true;
            }
        }
        public int Place {
            get
            {
                return _Place;
            }

            set
            {
                _Place = value;
                _isDirty = true;
            }
        }
        public int Level {
            get
            {
                return _Level;
            }

            set
            {
                _Level = value;
                _isDirty = true;
            }
        }
        public int TotalGhost {
            get
            {
                return _TotalGhost;
            }

            set
            {
                _TotalGhost = value;
                _isDirty = true;
            }
        }

        public int CategoryID
        {
            get { 
                switch(Place)
                {
                    case 0:
                        return 1;
                    case 4:
                        return 5;
                    case 6:
                        return 7;
                }

                return -1;
            }
        }
    }
}
