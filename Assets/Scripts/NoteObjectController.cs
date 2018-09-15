using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using System;

public class NoteObjectController : MonoBehaviour {

    const int PreCalcSpawningTick = 200;
    public static int SpawningTick => Mathf.RoundToInt(PreCalcSpawningTick / (Beatmap.CurrentlyLoaded.AR + 1));

	// Use this for initialization
	void Start () {
        Beatmap.CurrentlyLoaded = new Beatmap() {
            Notes = new Queue<Note>(new Note[] {
                new Note() { Slice = 1, Tick = 50 },
                new Note() { Slice = 3, Tick = 70 }
            })
        };

        Rhythm.OnTick += (int beat) => {
            Note temp;
            for (int i = 0; !((temp = Beatmap.CurrentlyLoaded.GetNoteAt(i)) == null) && temp.TicksToThis <= SpawningTick; i++)
            {
                temp.isLoaded = true;
                Beatmap.CurrentlyLoaded.RemoteNote(temp);
                NoteObject.Create(temp);
                UnityMainThreadDispatcher.Instance().Enqueue(() => new GameObject("NoteSlider0", typeof(NoteSliderObject)).GetComponent<NoteSliderObject>().SetTriangle(0, 0, 100, 100));
            }
        };
        Rhythm.Running = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), Rhythm.Tick.ToString());
    }
}