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
    float[] lastThree = new float[3];

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
        float prevMag = -1;
        float curMag;
        float diff;
        while (mouseDown)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 distance = endPos - startPos;
            Vector2 target = (new Vector2(-distance.x, -distance.y) * aimMultiplier)  + (Vector2)transform.position;
            float modx = target.x % 0.032f;
            float mody = target.y % 0.032f;
            target = new Vector2(target.x - modx, target.y - mody);
            crosshair.transform.position = target;
            popSlider.value = distance.magnitude;

            //If prevmag hasn't been assigned yet. start the process
            if(prevMag < 0)
            {
                prevMag = distance.magnitude;
            }
            else//Then get the speed of the mouse.
            {
                curMag = distance.magnitude;
                diff = curMag - prevMag;
                if(diff > 0)
                {
                    AddToLastThree(diff);
                }
                prevMag = curMag;
            }
            
            if(distance.magnitude >= 1)
            {
                Debug.Log("Fire! @ speeds of " + GetLargestDelta());
                crosshair.SetActive(false);
                mouseDown = false;
            }
            yield return null;
        }
    }

    void AddToLastThree(float pDelta)
    {
        //for(int i = 0; i < lastThree.Length - 1; i++)
        //{
        //    lastThree[i] = lastThree[i + 1];
        //}
        lastThree[0] = lastThree[1];
        lastThree[1] = lastThree[2];
        lastThree[2] = pDelta;
    }

    float GetLargestDelta()
    {
        float biggestNum = lastThree[0];
        for(int i = 1; i < lastThree.Length; i++)
        {
            if (lastThree[i] > biggestNum)
                biggestNum = lastThree[i];
        }
        return biggestNum;
    }
}
