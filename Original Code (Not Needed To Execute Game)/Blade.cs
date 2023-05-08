using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    private Rigidbody2D rb;

    public float minVelo = 0.1f;
    private Vector3 lastMousePos;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = IsMouseMoving();
        SetBladeToMouse();
    }
    
    private void SetBladeToMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z * -1;
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseMoving()
    {
        Vector3 curMousePos = Input.mousePosition;
        curMousePos.z = Camera.main.transform.position.z * -1;
        float traveled = (lastMousePos - curMousePos).magnitude;

        //Debug.Log(traveled.ToString());
        lastMousePos = curMousePos;

        if (traveled > minVelo)
        {
            return true;
        }
        else 
        {
            return false;
        }
        
    }
    
}
