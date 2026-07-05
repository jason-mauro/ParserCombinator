namespace ParserCombinator;

public class Optional<T>
    {
        public class OptionalHasNoValueException : Exception { }

        private T? _data;
        private bool _hasValue;

        private bool HasData => _hasValue;

        public static Optional<T> Empty()
        {
            return new Optional<T>();
        }

        public static Optional<T> Of(T data)
        {
            return new Optional<T>() { _data = data, _hasValue = true };
        }

        public static Optional<T> OfNullable(T? data)
        {
            return data == null ? Empty() : Of(data);
        }

        public Optional<T> Where(Func<T, bool> predicate)
        {
            return (_data != null && predicate(_data)) ? this : Empty();
        }

        public Optional<TR> Select<TR>(Func<T, TR> converter)
        {
            return _data != null ? Optional<TR>.Of(converter(_data)) : Optional<TR>.Empty();
        }

        public T GetOrThrow()
        {
            if (_data != null)
                return _data;
            throw new OptionalHasNoValueException();
        }

        public T GetOrThrow(Exception e)
        {
            if (_data != null)
                return _data;
            throw e;
        }

        public T? GetOrNull()
        {
            return _data ?? default;
        }

        public T GetOrElse(T value) => _data != null ? _data : value;

        public T GetOrElse(Func<T> supplier) => _data != null ? _data : supplier();

        public void Set(T value)
        {
            _data = value;
        }

        public void SetIfEmpty(Func<T> supplier)
        {
            _data = supplier();
        }

        public void DoIfPresent(Action<T> ifPresent)
        {
            if (_data != null)
                ifPresent(_data);
        }

        public void DoOrElse(Action<T> ifPresent, Action orElse)
        {
            if (_data != null)
                ifPresent(_data);
            else
                orElse();
        }

        public bool CheckIfPresent(Func<T, bool> predicate)
        {
            if (IsPresent)
                    return predicate(_data);
            return false;
        }

        public bool IsPresent => HasData;

        public bool IsEmpty => !HasData;

        public void IfPresent(Action<T> doIt)
        {
            if (HasData && _data != null)
                doIt(_data);
        }
        

        public Optional<U> Cast<U>()
        {
            if (!HasData)
            {
                return Optional<U>.Empty();
            }

            if (_data is U casted)
            {
                return Optional<U>.Of(casted);
            }
            
            return Optional<U>.Empty();
        }

        public Optional<T> Or(Optional<T> other)
        {
            return HasData ? this : other;
        }

        public static implicit operator Optional<T>(T value) => Of(value);
        public static bool operator true(Optional<T> opt) => opt.IsPresent;
        public static bool operator false(Optional<T> opt) => opt.IsEmpty; 

        public override string ToString()
        {
            return _data?.ToString() ?? "NOTHING";
        }
    }