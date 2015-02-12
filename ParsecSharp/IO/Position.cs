using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public struct Position : IEquatable<Position>
   {
      private int _offset;
      private int _line;
      private int _column;

      internal Position(int offset, int line, int column)
      {
         this._offset = offset;
         this._line = line;
         this._column = column;
      }

      public int Offset
      {
         get
         {
            return _offset;
         }
      }

      public int Line
      {
         get
         {
            return _line;
         }
      }

      public int Column
      {
         get
         {
            return _column;
         }
      }

      public override int GetHashCode()
      {
         return Offset.GetHashCode() * 17
              + Line.GetHashCode() * 19
              + Column.GetHashCode() * 23;
      }

      public override bool Equals(object obj)
      {
         return obj is Position && this == (Position)obj;
      }

      public bool Equals(Position other)
      {
         return this == other;
      }

      public static bool operator ==(Position positionA, Position positionB)
      {
         return positionA.Offset == positionB.Offset
             && positionA.Line == positionB.Line
             && positionA.Column == positionB.Column;
      }

      public static bool operator !=(Position positionA, Position positionB)
      {
         return !(positionA == positionB);
      }

      public override string ToString()
      {
         return string.Format("Pos {0}, Ln {1}, Col {2}", Offset, Line, Column);
      }
   }
}
