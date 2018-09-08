using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmLightup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Rhythm.OnTick += (int beat) =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                if(Rhythm.IsNote(1))
                    renderer.material.color = renderer.material.color == Color.red ? Color.blue : Color.red;
            });
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
