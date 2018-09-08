using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class NoteObject : MonoBehaviour {

    public Map.Note MapNote;
    public int Slice {
        get {
            return MapNote.Slice;
        }
        set {
            MapNote.Slice = value % Map.Beatmap.CurrentlyLoaded.SliceCount;
            CalculateRotationForSlice();
        }
    }
    private void CalculateRotationForSlice()
    {
        transform.rotation = Quaternion.Euler(0, 0, MapNote.Slice * -(360 / Map.Beatmap.CurrentlyLoaded.SliceCount));
    }
    public static Vector3 SpawningPoint => GameObject.Find("Spawning Point").transform.position;
    public int Distance;
    public Vector2 LocalTapLocation => transform.rotation * new Vector3(0, Distance);
    public Vector2 StartingLocation => SpawningPoint + (Vector3)LocalTapLocation;

    public void Load()
    {
        CalculateRotationForSlice();
        transform.position = StartingLocation;
    }

    // Update is called once per frame
    void Update () {
        var distance = NoteObjectController.SpawningTick - MapNote.TicksToThis;
        transform.position = SpawningPoint + (
            ((transform.rotation * new Vector3(0, distance)) / NoteObjectController.SpawningTick) * Distance
        );
        Debug.Log(distance);

        if (MapNote.TicksToThis == 0)
            GetComponent<MeshRenderer>().material.color = Color.red;
        if (MapNote.TicksToThis <= -10)
            Destroy(gameObject);
    }
    public static GameObject Prefab => Resources.Load<GameObject>("Prefabs/Note");
    public static void Create(Map.Note mapNote)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            GameObject gameObject = Instantiate(Prefab);
            NoteObject noteObjComp = gameObject.GetComponent<NoteObject>();
            noteObjComp.MapNote = mapNote;
            noteObjComp.Load();
        });
    }
}
