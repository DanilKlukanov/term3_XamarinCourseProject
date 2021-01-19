namespace CW.Services
{
    public class ApiResponse<T> : IResponse<T>
    {

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                IsSuccessful = true;
                _value = value;
            }
        }

        public string ErrorMessage { get; set; } = "Успех.";
        public bool IsSuccessful { get; set; }
    }
}