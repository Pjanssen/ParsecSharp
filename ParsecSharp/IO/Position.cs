using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public sealed class Position
   {
      internal Position(int offset, int line, int column)
      {
         this.Offset = offset;
         this.Line = line;
         this.Column = column;
      }

      public int Offset
      {
         get;
         private set;
      }

      public int Line
      {
         get;
         private set;
      }

      public int Column
      {
         get;
         private set;
      }

      public override int GetHashCode()
      {
         return Offset.GetHashCode() * 17
              + Line.GetHashCode() * 19
              + Column.GetHashCode() * 23;
      }

      public override bool Equals(object obj)
      {
         Position other = obj as Position;
         if (other == null)
            return false;

         return Offset == other.Offset
             && Line == other.Line
             && Column == other.Column;
      }

      public static bool operator ==(Position positionA, Position positionB)
      {
         if (Object.ReferenceEquals(positionA, positionB))
            return true;

         if ((object)positionA == null || (object)positionB == null)
            return false;

         return positionA.Equals(positionB);
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
