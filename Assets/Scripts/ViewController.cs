using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewController : MonoBehaviour
{
    public float speed = 25;
    public float mouseSpeed = 320;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h * speed, mouse * mouseSpeed, v * speed) * Time.deltaTime, Space.World);
    }
}
