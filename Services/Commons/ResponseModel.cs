using Services.Model;

namespace Services.Commons
{
    public class ResponseModel<T>
    {
        public T? Data { get; set; }
        public string? Errors { get; set; }
        public bool HasError => Errors?.Length > 0;
        public UserwithRole Role { get; set; }
        public UserView UserView { get; set; }
    }
}
