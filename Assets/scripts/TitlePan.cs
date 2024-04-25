using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePan : MonoBehaviour
{
    public float movespeed = 1;
    public int total_frames = 5000;
    public int frames_at = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (frames_at < total_frames) {
            transform.Translate(Vector2.up *movespeed *Time.deltaTime);
        }
        frames_at += 1;
    }
}
