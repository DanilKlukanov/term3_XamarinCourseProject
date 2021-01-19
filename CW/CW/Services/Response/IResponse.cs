namespace CW.Services
{
    public interface IResponse<T>
    {
        T Value { get; set; }
        string ErrorMessage { get; set; }
        bool IsSuccessful { get; set; }
    }
}