using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(mouseRay, out hit))
        {
            Debug.Log("Hit " + hit.transform.name);
        }
	}
}
