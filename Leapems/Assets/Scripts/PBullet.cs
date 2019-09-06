using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour {

    Vector2 dir = Vector2.zero;
    public float speed = 0.1f;
    private float bulletTime = 2f;
    private Vector2 startPos;
	
	// Update is called once per frame
	void Update () {
        dir = dir.normalized;
        transform.position += new Vector3(dir.x, dir.y) * speed;
	}

    public void Action(Vector2 pDirection, Vector2 pStartPos)
    {
        Invoke("Recycle", bulletTime);
        dir = pDirection;
        startPos = pStartPos;
    }

    private void Recycle()
    {
        dir = Vector2.zero;
        transform.position = startPos;
    }
}
