namespace ShopShare.Domain.Common.Models
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");

        public Error(string code, string message)
        {
            Code = [code];
            Message = [message];
        }

        public Error(IEnumerable<string> code, IEnumerable<string> message)
        {
            Code = code.ToArray();
            Message = message.ToArray();
        }

        public string[] Code { get; }
        public string[] Message { get; }


        public static implicit operator string[](Error error) => error.Code;

        public bool Equals(Error? other)
        {
            if (other is null)
            {
                return false;
            }

            return Code == other.Code && Message == other.Message;
        }

        public override bool Equals(object? obj) => obj is Error error && Equals(error);
        public override int GetHashCode() => HashCode.Combine(Code, Message);
        public override string ToString() => String.Join(Environment.NewLine, Code);

    }
}
