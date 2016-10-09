using UnityEngine;
using System.Collections;

public class JsonChanger : MonoBehaviour {
    [SerializeField]
    string[] jsonFiles;
    public int currentJsonIndex = 0;
    string currentJson;
    public Client client;
	// Use this for initialization
	void Start () {
        currentJson = jsonFiles[currentJsonIndex];
        client.testJson = currentJson;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentJsonIndex = currentJsonIndex<jsonFiles.Length-1 ? currentJsonIndex+ 1 :0;
            currentJson = jsonFiles[currentJsonIndex];
            client.testJson = currentJson;
        }
	}
}
