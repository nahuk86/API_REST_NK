using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public struct Priority
    {
        private readonly int _value;

        public const int MIN_PRIORITY = 1;
        public const int MAX_PRIORITY = 10;

        public Priority(int value)
        {
            if (value < MIN_PRIORITY || value > MAX_PRIORITY)
                throw new InvalidPriorityException(value);

            _value = value;
        }

        public int Value => _value;

        public static implicit operator int(Priority priority) => priority._value;
        public static implicit operator Priority(int value) => new Priority(value);

        public override string ToString() => _value.ToString();
        public override bool Equals(object obj) => obj is Priority other && _value == other._value;
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Priority left, Priority right) => left._value == right._value;
        public static bool operator !=(Priority left, Priority right) => !(left == right);
        public static bool operator >(Priority left, Priority right) => left._value > right._value;
        public static bool operator <(Priority left, Priority right) => left._value < right._value;
        public static bool operator >=(Priority left, Priority right) => left._value >= right._value;
        public static bool operator <=(Priority left, Priority right) => left._value <= right._value;
    }
}
