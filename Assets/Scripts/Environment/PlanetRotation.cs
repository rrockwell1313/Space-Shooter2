using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
  private float rotationSpeed = 0.1f;
  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void FixedUpdate()
  {
    RotatePlanet();
  }

  public void RotatePlanet()
  {
    transform.Rotate(Vector3.left * rotationSpeed);
  }
}
