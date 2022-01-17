using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public bool pressed;
    public GameObject previous;
    public GameObject ramp;
    public Transform target_pos;
    public float speed = 1.0f;
    private Vector3 original_pos;

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        original_pos = ramp.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if(pressed == true)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

            float distance = Vector3.Distance(ramp.transform.position, target_pos.position);

            if (distance == 0.0f)
                return;
            else if (distance < 0.01f)
                ramp.transform.position = target_pos.position;
            else
                ramp.transform.position = Vector3.MoveTowards(ramp.transform.position, target_pos.position, step);

        } else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

            float distance = Vector3.Distance(ramp.transform.position, original_pos);

            if (distance == 0.0f)
                return;
            else if (distance < 0.01f)
                ramp.transform.position = original_pos;
            else
                ramp.transform.position = Vector3.MoveTowards(ramp.transform.position, original_pos, step);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (previous != null && previous.GetComponent<ButtonPuzzle>().pressed == false)
        {
            Reset();
        }

        pressed = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        pressed = false;

        if (previous == null)
        {
            pressed = true;
            return;
        }

        if (previous.GetComponent<ButtonPuzzle>().pressed == true)
        {
            pressed = true;
        }
    }

    public void Reset()
    {
        pressed = false;
        if (previous == null)
            return;
        previous.GetComponent<ButtonPuzzle>().Reset();
    }
}
