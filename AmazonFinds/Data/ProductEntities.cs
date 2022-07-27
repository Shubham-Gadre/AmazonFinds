using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonFinds.Data
{
    public class ProductEntities
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductLink { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
