using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Andrew Seba
/// Description: Drag away from the player to make him jump towards his goal!
/// </summary>
public class PlayerController : MonoBehaviour {

    public float aimMultiplier = 1f;
    public GameObject crosshair;
    //Slider that shows when my action (or gun or jump "TBD") will happen.
    public Slider popSlider;
    //Limit on the slider value. (Trigger value)
    public float actionSliderLimit = 1f;

    Vector2 startPos;
    Vector2 endPos;
    bool mouseDown = false;

	// Use this for initialization
	void Start () {
        popSlider.maxValue = actionSliderLimit;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            crosshair.SetActive(true);
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDown = true;
            StartCoroutine("MouseDownLoop");
        }

        if (Input.GetMouseButtonUp(0))
        {
            crosshair.SetActive(false);
            mouseDown = false;
        }

        
	}

    IEnumerator MouseDownLoop()
    {
        while (mouseDown)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 distance = endPos - startPos;
            Vector2 target = (new Vector2(-distance.x, -distance.y) * aimMultiplier)  + (Vector2)transform.position;
            crosshair.transform.position = target;
            popSlider.value = distance.magnitude;
            if(distance.magnitude >= 1)
            {
                Debug.Log("Fire!");
                crosshair.SetActive(false);
                mouseDown = false;
            }
            yield return null;
        }
    }
}
