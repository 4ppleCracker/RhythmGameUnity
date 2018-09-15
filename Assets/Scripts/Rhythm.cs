using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

static class Rhythm
{
    public const int TicksPerBeat = 16;
    public static long Tick { get; private set; }
    public static int BeatInTick => (int)Tick % TicksPerBeat;

    public static int Bpm {
        get {
            return Beatmap.CurrentlyLoaded.Bpm;
        }
        set {
            Beatmap.CurrentlyLoaded.Bpm = value;
        }
    }
    public static double Interval {
        get {
            var bpms = (60f / (Bpm * TicksPerBeat));
            return bpms * 1000;
        }
    }

    public static bool Running { get; set; } = false;

    public static event Action<int> OnTick;

    public static void Reset() => Tick = 0;

    private static Timer timer;

    static Rhythm()
    {
        timer = new Timer();
        timer.Interval = Interval;
        timer.Elapsed += (object sender, ElapsedEventArgs args) => { if (Running) { Tick++; OnTick.Invoke(BeatInTick); } };
        timer.Start();
    }

    public static void ChangeBpm(Map.BpmChange bpmChange)
    {
        if (bpmChange.Bpm > 0)
            Bpm = bpmChange.Bpm;
        else
            throw new Exception("Can't have 0 bpm");
        timer.Interval = Interval;
    }

    // % 16 == 1/1
    // % 8  == 1/2
    // % 4  == 1/4
    /// <summary>
    /// If current note playing is a 1/nth note
    /// </summary>
    /// <param name="n">Denominator in the calculation</param>
    /// <returns></returns>
    public static bool IsNote(int n)
    {
        return BeatInTick % (TicksPerBeat / n) == 0;
    }
}