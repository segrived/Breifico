using System;

namespace Breifico.BinaryArithmetic.GenericOps
{
    public class IntWrapper : Num<int>
    {
        public IntWrapper(int value) : base(value) { }

        public override Num<int> Xor(int other)
            => new IntWrapper(this.Value ^ other);

        public override Num<int> And(int other)
            => new IntWrapper(this.Value & other);

        public override Num<int> Or(int other)
            => new IntWrapper(this.Value | other);

        public override Num<int> Not() 
            => new IntWrapper(~this.Value);

        public override Num<int> Add(int other)
            => new IntWrapper(this.Value + other);

        public override Num<int> Sub(int other)
            => new IntWrapper(this.Value - other);

        public override Num<int> Neg()
            => new IntWrapper(-this.Value);

        public override int Size { get; } = sizeof(int);

        public override string StringView
            => Convert.ToString(this.Value, 2).PadLeft(this.Size * 8, '0');

        public override int Zero { get; } = 0;
        public override int One { get; } = 1;
    }
}