using System;
using System.IO.Pipes;

namespace Data_Struct
{
    public struct Coord
    {
        public int X;
        public int Y;
    }

    public enum Direction
    {
        ClockWise,
        CounterClockWise
    }

    public enum Corner
    {
        Left,
        Right,
        NotCorner
    }

    public struct Choice
    {
        public Coord coord;
        public Direction direction;
        public Corner corner;
    }

    public enum step
    {
        UMUNIA,
        UTEZA_NA_NDRAZI
    }

}

