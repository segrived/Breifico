using System;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Compression.RLE
{
    /// <summary>
    /// RLE (Run-Length кодирование)
    /// </summary>
    public sealed class RleEncoder
    {
        private enum State
        {
            Undetermined,
            IdenticalSeq,
            DifferentSeq
        }

        private readonly MyList<byte> _output = new MyList<byte>();

        int _startIndex;
        State _state = State.Undetermined;
        byte? _lastByte;
        private readonly byte[] _input;

        void AppendSeq(byte s, int count, int newStartIndex) {
                this._output.Add((byte)count);
                this._output.Add(s);

                this._startIndex = newStartIndex;
                this._state = State.Undetermined;
        }

        public RleEncoder(byte[] input) {
            this._input = input;
        }

        public byte[] Encode() {
            if (this._input.Length == 0) {
                return new byte[0];
            }

            for (int i = 0; i < this._input.Length; i++) {
                if (i - this._startIndex == Byte.MaxValue) {
                    if (this._state == State.IdenticalSeq) {
                        this.AppendSeq(this._lastByte.Value, i - this._startIndex, i);
                    } else if (this._state == State.DifferentSeq) {
                        var arr = new byte[i - this._startIndex - 1];
                        this._output.Add(0);
                        this._output.Add((byte)(i - this._startIndex - 1));
                        Array.Copy(this._input, this._startIndex, arr, 0, i - this._startIndex - 1);
                        this._output.AddRange(arr);

                        this._startIndex = i - 1;
                        this._state = State.IdenticalSeq;
                    }
                }
                byte currentByte = this._input[i];
                switch (this._state) {
                    case State.Undetermined:
                        if (this._lastByte != null) {
                            if (this._lastByte == currentByte) {
                                this._state = State.IdenticalSeq;
                            } else if (this._lastByte != currentByte) {
                                this._state = State.DifferentSeq;
                            }
                        }
                        break;
                    case State.IdenticalSeq:
                        if (currentByte != this._lastByte) {
                            this.AppendSeq(this._lastByte.Value, i - this._startIndex, i);
                        }
                        break;
                    case State.DifferentSeq:
                        if (currentByte == this._lastByte) {
                            var arr = new byte[i - this._startIndex - 1];

                            this._output.Add(0);
                            this._output.Add((byte)(i - this._startIndex - 1));
                            Array.Copy(this._input, this._startIndex, arr, 0, i - this._startIndex - 1);
                            this._output.AddRange(arr);

                            this._startIndex = i - 1;
                            this._state = State.IdenticalSeq;
                        }
                        break;
                }
                this._lastByte = currentByte;
            }

            switch (this._state) {
                case State.IdenticalSeq:
                    this.AppendSeq(this._lastByte.Value, this._input.Length - this._startIndex, 0);
                    break;
                case State.DifferentSeq:
                case State.Undetermined:
                    var arr = new byte[this._input.Length - this._startIndex];
                    this._output.Add(0);
                    this._output.Add((byte)(this._input.Length - this._startIndex));
                    Array.Copy(this._input, this._startIndex, arr, 0, this._input.Length  - this._startIndex);
                    this._output.AddRange(arr);
                    break;
            }

            return this._output.ToArray();
        }
    }
}