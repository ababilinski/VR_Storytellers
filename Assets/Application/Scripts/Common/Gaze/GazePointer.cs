using UnityEngine;
using System.Collections;

public class GazePointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetPointerPosition(Vector3 position) {

        position.x = 0;
        position.y = 0;

        transform.localPosition = position;

        //.10 scales the pointer to a reasonable size
        transform.localScale = Vector3.one * Vector3.Distance(Camera.main.transform.position, transform.position) * .10f;
    }
}
