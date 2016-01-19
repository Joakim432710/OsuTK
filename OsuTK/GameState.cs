using System;
using JetBrains.Annotations;

namespace OsuTK
{
    public struct GameState
    {
        public readonly ulong Identifier;
        public readonly string Name;

        public GameState(byte val, [NotNull] string name)
        {
            Identifier = val;
            Name = name;
        }

        public GameState(sbyte val, [NotNull] string name)
        {
            Identifier = (byte) val;
            Name = name;
        }

        public GameState(short val, [NotNull] string name)
        {
            Identifier = (ushort) val;
            Name = name;
        }

        public GameState(ushort val, [NotNull] string name)
        {
            Identifier = val;
            Name = name;
        }

        public GameState(int val, [NotNull] string name)
        {
            Identifier = (uint) val;
            Name = name;
        }

        public GameState(uint val, [NotNull] string name)
        {
            Identifier = val;
            Name = name;
        }

        public GameState(long val, [NotNull] string name)
        {
            Identifier = (ulong) val;
            Name = name;
        }

        public GameState(ulong val, [NotNull] string name)
        {
            Identifier = val;
            Name = name;
        }

        public GameState(byte[] val, [NotNull] string name)
        {
            if (val.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(val),
                    "Passed byte array's length must be in range (0, 8) inclusive.");
            ulong finalValue = 0;
            for (var iii = 0; iii < val.Length; ++iii)
                finalValue += ((ulong) Math.Pow(256, iii))*val[iii];
            Identifier = finalValue;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is GameState) && Equals((GameState) obj);
        }

        public bool Equals(GameState obj)
        {
            return Identifier == obj.Identifier;
        }
    }
}