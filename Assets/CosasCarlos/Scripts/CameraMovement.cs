using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float xRange;
    [SerializeField]
    private float zRange;
    [SerializeField] 
    private float speed;
    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    // Start is called before the first frame update
    void Start()
    {
        xMin = transform.position.x - xRange;
        xMax = transform.position.x + xRange;
        zMin = transform.position.z - zRange;
        zMax = transform.position.z + zRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        float objectiveX = transform.position.x + (speed*Time.fixedDeltaTime*xInput);
        objectiveX = Mathf.Clamp(objectiveX, xMin, xMax);

        float objectiveZ = transform.position.z + (speed * Time.fixedDeltaTime * zInput);
        objectiveZ = Mathf.Clamp(objectiveZ, zMin, zMax);
        transform.position = new(objectiveX, transform.position.y, objectiveZ);

    }
}
