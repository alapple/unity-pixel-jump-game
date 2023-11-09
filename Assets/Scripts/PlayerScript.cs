using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.Input;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool jump = Input.GetKey("Jump");
        
        if (jump)
        {
            transform.Translate(new Vector3(0, 0, 0));
        }
    }
}
