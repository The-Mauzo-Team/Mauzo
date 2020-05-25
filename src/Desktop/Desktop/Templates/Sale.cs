using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Templates
{
    class Sale
    {
        private int Id 
        {
            get { return Id; }
            set { Id = value; }
        }

        private DateTime StampRef 
        {
            get { return StampRef; }
            set { StampRef = value; }
        }

        private int UserId
        { 
            get { return UserId; }
            set { UserId = value; }
        }

        private int ProdId 
        {
            get { return ProdId; }
            set { ProdId = value; }
        }

        private int DiscId
        {
            get { return DiscId; }
            set { DiscId = value; }
        }

        private int RefundId
        {
            get { return RefundId; }
            set { RefundId = value; }
        }
    }
}
