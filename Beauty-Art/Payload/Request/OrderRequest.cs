namespace Beauty_Art.Payload.Request
{
    public class OrderRequest
    {
        public OrderRequest() {
            CourseOrder = new List<CourseOrder>();
        }
        public Guid User_Id { get; set; }
        public List<CourseOrder> CourseOrder { get; set; }
        public int TotalPrice { get; set; }
    }
    public class CourseOrder
    {
        public Guid CourseId { get; set; }
        public int quanity { get; set; }
        public int price { get; set; }
    }
}
