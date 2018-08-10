using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Andrew Seba
/// Description: Drag away from the player to make him jump towards his goal!
/// </summary>
public class PlayerController : MonoBehaviour {

    Vector2 startPos;
    Vector2 endPos;
    bool mouseDown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDown = true;
            StartCoroutine("MouseDownLoop");
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        
	}

    IEnumerator MouseDownLoop()
    {
        while (mouseDown)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 distance = endPos - startPos;
            Debug.Log(distance.magnitude);
            if(distance.magnitude >= 1)
            {
                mouseDown = false;
            }
            yield return null;
        }
    }
}
