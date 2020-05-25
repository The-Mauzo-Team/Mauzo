using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Templates
{
    class Sale
    {
        private int id;
        private long stampRef;
        private int userId;
        private int prodId;
        private int discId;
        
        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        public long StampRef 
        {
            get { return stampRef; }
            set { stampRef = value; }
        }

        public int UserId
        { 
            get { return userId; }
            set { userId = value; }
        }

        public int ProdId 
        {
            get { return prodId; }
            set { prodId = value; }
        }

        public int DiscId
        {
            get { return discId; }
            set { discId = value; }
        }
    }
}
