using System;

namespace Breifico.BinaryArithmetic.GenericOps
{
    public class ByteWrapper : Num<byte>
    {
        public ByteWrapper(byte value) : base(value) {}

        public override Num<byte> Xor(byte other)
            => new ByteWrapper((byte)(this.Value ^ other));

        public override Num<byte> And(byte other) 
            => new ByteWrapper((byte)(this.Value & other));

        public override Num<byte> Or(byte other) 
            => new ByteWrapper((byte)(this.Value | other));

        public override Num<byte> Not() => 
            new ByteWrapper((byte)~this.Value);

        public override Num<byte> Add(byte other)
            => new ByteWrapper((byte)(this.Value + other));

        public override Num<byte> Sub(byte other) 
            => new ByteWrapper((byte)(this.Value - other));

        public override Num<byte> Neg()
            => new ByteWrapper((byte)-this.Value);

        public override int Size { get; } = sizeof(byte);

        public override string StringView
            => Convert.ToString(this.Value, 2).PadLeft(this.Size * 8, '0');

        public override byte Zero { get; } = 0x00;
        public override byte One { get; } = 0x01;
    }
}