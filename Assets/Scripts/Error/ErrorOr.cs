namespace Error
{
    public class ErrorOr<T>
    {
        private string _reason;
        private T _value;
        private bool _isOk = false;

        public static ErrorOr<T> CreateError(string reason)
        {
            return new ErrorOr<T>
            {
                _reason = reason, _isOk = false
            };
        }

        public static ErrorOr<T> CreateValue(T value)
        {
            return new ErrorOr<T>
            {
                _value = value, _isOk = true
            };
        }

        public bool IsOk(out T value)
        {
            if (_isOk)
            {
                value = _value;
                return true;
            }

            value = default;
            return false;
        }
    }
}