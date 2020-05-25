using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Templates
{
    class Sale
    {
        public int Id 
        {
            get { return Id; }
            set { Id = value; }
        }

        public DateTime StampRef 
        {
            get { return StampRef; }
            set { StampRef = value; }
        }

        public int UserId
        { 
            get { return UserId; }
            set { UserId = value; }
        }

        public int ProdId 
        {
            get { return ProdId; }
            set { ProdId = value; }
        }

        public int DiscId
        {
            get { return DiscId; }
            set { DiscId = value; }
        }

        public int RefundId
        {
            get { return RefundId; }
            set { RefundId = value; }
        }
    }
}
