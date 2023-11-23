using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string CourseId { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
    }
}
