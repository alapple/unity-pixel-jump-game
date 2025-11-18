using UnityEngine;

public class RotateSpinner : MonoBehaviour
{
    public float speed;
    
    void Start()
    { 
        transform.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
    }
    
    void Update()
    {
        
    }
}
