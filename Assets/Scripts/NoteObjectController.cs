using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using System;

public class NoteObjectController : MonoBehaviour {

    const int PreCalcSpawningTick = 200;
    public static int SpawningTick => Mathf.RoundToInt(PreCalcSpawningTick / (Beatmap.CurrentlyLoaded.AR + 1));

    [SerializeField]
    private Material NoteSliderMaterial;

    public static NoteSliderObject[] NoteSlider { get; private set; }

    private Triangle GetTriangleForSlice(int slice, float scale = 1)
    {
        Triangle tri = new Triangle(new Vector2(0, 0), new Vector2(1, 0), new Vector2(.5f, 1)) * scale;
        return tri - tri.C;
    }

    void Start() {
        Beatmap.CurrentlyLoaded = new Beatmap()
        {
            SliceCount = 8,
            Bpm = 120,
            Notes = new Queue<Note>(new Note[] {
                new Note() { Slice = 1, Tick = 50 },
                //new Note() { Slice = 2, Tick = 70 }
            })
        };

        List<NoteSliderObject> noteSliders = new List<NoteSliderObject>();
        for(int i = 0; i < Beatmap.CurrentlyLoaded.SliceCount; i++)
        {
            var noteSlider = new GameObject("NoteSlider" + i, typeof(NoteSliderObject)).GetComponent<NoteSliderObject>();

            noteSlider.Triangle = GetTriangleForSlice(i, 4);
            noteSlider.Material = NoteSliderMaterial;
            noteSlider.Slice = i;
            noteSlider.transform.rotation = Quaternion.Euler(0, 0, -(360 / Beatmap.CurrentlyLoaded.SliceCount) * i + 180);

            noteSliders.Add(noteSlider);
        }
        NoteSlider = noteSliders.ToArray();

        Rhythm.OnTick += (int beat) => {
            Note temp;
            for (int i = 0; !((temp = Beatmap.CurrentlyLoaded.GetNoteAt(i)) == null) && temp.TicksToThis <= SpawningTick; i++)
            {
                temp.isLoaded = true;
                Beatmap.CurrentlyLoaded.RemoteNote(temp);
                NoteObject.Create(temp);
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