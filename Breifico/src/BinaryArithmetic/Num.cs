using Breifico.BinaryArithmetic.GenericOps;

namespace Breifico.BinaryArithmetic
{
    public abstract class Num<T>
    {
        public T Value { get; }

        protected Num(T value) {
            this.Value = value;
        }

        public abstract Num<T> Xor(T other);
        public Num<T> Xor(Num<T> other) => this.Xor(other.Value);

        public abstract Num<T> And(T other);
        public Num<T> And(Num<T> other) => this.And(other.Value);

        public abstract Num<T> Or(T other);
        public Num<T> Or(Num<T> other) => this.Or(other.Value);

        public abstract Num<T> Not();

        public abstract Num<T> Add(T other);
        public Num<T> Add(Num<T> other) => this.Add(other.Value);

        public abstract Num<T> Sub(T other);
        public Num<T> Sub(Num<T> other) => this.Sub(other.Value);

        public abstract Num<T> Neg();
        public Num<T> Neg(Num<T> other) => this.Neg();

        public abstract string StringView { get; }
        public abstract int Size { get; }

        public abstract T Zero { get; }
        public abstract T One { get; }

        public override string ToString() => this.StringView;

        public static ByteWrapper Wrap(byte b) => new ByteWrapper(b);
    }
}