using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entity
{
    public class Chapter : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string videoUrl { get; set; }
        public Boolean isPulished { get; set; }
        public Boolean isFree { get; set; }
        public int Position { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
