namespace Beauty_Art.Payload.Request
{
    public class PostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid User_Id { get; set; }
    }
}
