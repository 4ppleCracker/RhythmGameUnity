using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    class Beatmap
    {
        public static Beatmap CurrentlyLoaded { get; set; } = new Beatmap() { SliceCount = 8 };

        public Queue<Note> Notes { get; set; }
        public Queue<BpmChange> BpmChanges { get; set; } = new Queue<BpmChange>();

        public float Acc = 5f;
        public float AR = 2f;

        public int SliceCount = 8;

        public int Bpm = 120;

        private long m_offset = 0;
        public long Offset {
            get {
                return m_offset;
            }
            set {
                List<Note> notes = Notes.ToList();
                List<Note> temp = new List<Note>();
                notes.ForEach(n => temp.Add(new Note { Tick = n.Tick - m_offset, Slice = n.Slice }));
                notes = new List<Note>(temp);
                temp.Clear();
                m_offset = value;
                notes.ForEach(n => temp.Add(new Note { Tick = n.Tick + m_offset, Slice = n.Slice }));
                notes = temp;
                Notes = new Queue<Note>(notes);
            }
        }

        public bool PendingBpmChange {
            get {
                if (BpmChanges.Count <= 0)
                    return false;
                return Rhythm.Tick >= BpmChanges.First().Tick;
            }
        }
        public bool PendingNotes {
            get {
                if (Notes.Count <= 0)
                    return false;
                return true;
            }
        }
        public long TicksToCurrent {
            get {
                if (Notes.Count > 0)
                    return Notes.Peek().TicksToThis;
                else
                    return long.MaxValue;
            }
        }
        public Note GetNoteAt(int i) {
            if (Notes.Count <= i)
                return null;
            return Notes.ToArray()[i];
        }
        public bool NoteExistsAtTick(int tick) {
            return Notes.Any(n => n.Tick == tick);
        }
        public void RemoveNote(Note n)
        {
            List<Note> temp = Notes.ToList();
            temp.Remove(n);
            Notes = new Queue<Note>(temp);
        }
    }
}
