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
        if(GoLeft())
        {
            gameObject.transform.position = new Vector3(x-1f, y, z);
        }
        if (GoRight())
        {
            gameObject.transform.position = new Vector3(x+1f, y, z);
        }

        if (GoUp())
        {
            gameObject.transform.position = new Vector3(x, y+1f, z);
        }

        if (GoDown())
        {
            gameObject.transform.position = new Vector3(x, y-1f, z);
        }

        Camera camera = gameObject.GetComponent<Camera>();
        if (ZoomIn())
        {
            if(camera.orthographicSize> 10)
            camera.orthographicSize -= 1;
        }
        if (ZoomOut())
        {
            camera.orthographicSize += 1;
        }
    }

    bool GoLeft(){
        return Input.GetKey(KeyCode.LeftArrow) ||
                    Input.GetKey(KeyCode.A);
    }

    bool GoRight(){
        return Input.GetKey(KeyCode.RightArrow) ||
                    Input.GetKey(KeyCode.D);
    }
    bool GoUp()
    {
        return Input.GetKey(KeyCode.UpArrow) ||
                    Input.GetKey(KeyCode.W);
    }
    bool GoDown()
    {
        return Input.GetKey(KeyCode.DownArrow) ||
                    Input.GetKey(KeyCode.S);
    }
    bool ZoomOut()
    {
        return Input.GetKey(KeyCode.Minus) ||
                    Input.GetKey(KeyCode.Q);
    }
    bool ZoomIn()
    {
        return Input.GetKey(KeyCode.Equals) ||
                    Input.GetKey(KeyCode.E);
    }
}
