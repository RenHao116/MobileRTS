using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Transform transform = gameObject.transform;
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if(Input.GetKey(KeyCode.LeftArrow)){
            gameObject.transform.position = new Vector3(x-1f, y, z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position = new Vector3(x+1f, y, z);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position = new Vector3(x, y+1f, z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position = new Vector3(x, y-1f, z);
        }

        Camera camera = gameObject.GetComponent<Camera>();
        if (Input.GetKey(KeyCode.Equals))
        {
            camera.orthographicSize -= 1;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            camera.orthographicSize += 1;
        }
    }
}
