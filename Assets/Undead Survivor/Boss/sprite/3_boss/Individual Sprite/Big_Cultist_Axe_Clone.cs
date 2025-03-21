using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Cultist_Axe_Clone : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.back * 360f * Time.deltaTime);
    }
}
