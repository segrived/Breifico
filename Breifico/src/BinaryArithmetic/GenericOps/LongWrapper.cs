using System;

namespace Breifico.BinaryArithmetic.GenericOps
{
    public class LongWrapper : Num<long>
    {
        public LongWrapper(long value) : base(value) { }

        public override Num<long> Xor(long other)
            => new LongWrapper(this.Value ^ other);

        public override Num<long> And(long other)
            => new LongWrapper(this.Value & other);

        public override Num<long> Or(long other)
            => new LongWrapper(this.Value | other);

        public override Num<long> Not() =>
            new LongWrapper(~this.Value);

        public override Num<long> Add(long other)
            => new LongWrapper(this.Value + other);

        public override Num<long> Sub(long other)
            => new LongWrapper(this.Value - other);

        public override Num<long> Neg()
            => new LongWrapper(-this.Value);

        public override int Size { get; } = sizeof(long);

        public override string StringView
            => Convert.ToString(this.Value, 2).PadLeft(this.Size * 8, '0');

        public override long Zero { get; } = 0L;
        public override long One { get; } = 1L;
    }
}