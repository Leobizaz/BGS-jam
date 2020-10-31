using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public float speed = 0;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + speed);
    }
}
