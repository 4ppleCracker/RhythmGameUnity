using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class Note
    {
        public long Tick;
        public int Slice;
        public long TicksToThis {
            get {
                return Tick - Rhythm.Tick;
            }
        }
        public bool isLoaded;

        public override bool Equals(object obj)
        {
            var note = obj as Note;
            return note != null &&
                   Tick == note.Tick &&
                   Slice == note.Slice &&
                   TicksToThis == note.TicksToThis;
        }

        public override int GetHashCode()
        {
            var hashCode = -17724969;
            hashCode += Tick.GetHashCode();
            hashCode += Slice.GetHashCode();
            hashCode += TicksToThis.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Note me, object other) => me?.Equals(other) ?? true;
        public static bool operator !=(Note me, object other) => !(me == other);
    }
}
