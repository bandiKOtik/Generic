using System;

public class ReactiveVariable<TValue> where TValue : IEquatable<TValue>
{
    public event Action<TValue> Changed;
    public event Action<TValue, TValue> OldNewChanged;

    private TValue _value;

    public ReactiveVariable(TValue value = default) => _value = value;

    public TValue Value
    {
        get => _value;
        set
        {
            TValue oldValue = _value;

            _value = value;

            if (_value.Equals(oldValue) == false)
            {
                Changed?.Invoke(Value);
                OldNewChanged?.Invoke(oldValue, Value);
            }
        }
    }
}