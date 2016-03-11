using System;

namespace Breifico.BinaryArithmetic.GenericOps
{
    public class ShortWrapper : Num<short>
    {
        public ShortWrapper(short value) : base(value) { }

        public override Num<short> Xor(short other)
            => new ShortWrapper((short)(this.Value ^ other));

        public override Num<short> And(short other)
            => new ShortWrapper((short)(this.Value & other));

        public override Num<short> Or(short other)
            => new ShortWrapper((short)(this.Value | other));

        public override Num<short> Not() =>
            new ShortWrapper((short)~this.Value);

        public override Num<short> Add(short other)
            => new ShortWrapper((short)(this.Value + other));

        public override Num<short> Sub(short other)
            => new ShortWrapper((short)(this.Value - other));

        public override Num<short> Neg()
            => new ShortWrapper((short)-this.Value);

        public override int Size { get; } = sizeof(short);

        public override string StringView
            => Convert.ToString(this.Value, 2).PadLeft(this.Size * 8, '0');

        public override short Zero { get; } = 0;
        public override short One { get; } = 1;
    }
}