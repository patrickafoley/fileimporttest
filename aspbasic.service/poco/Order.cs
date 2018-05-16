using System;
namespace aspbasic.service.poco
{
    public class Order
    {
        public int orderId{ get; set; }
        public Customer customer { get; set; }
        public StudyGuide[] studyGuides{ get; set; }
        public bool fulfilled { get; set;  }
        public DateTime dateFulfilled { get; set;}

    }
}
