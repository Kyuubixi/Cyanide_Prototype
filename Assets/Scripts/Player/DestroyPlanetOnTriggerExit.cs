using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanetOnTriggerExit : MonoBehaviour
{
    public int amountOfObjects = 0;
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

    public void GetAmountOfObjects()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, 200f);
        amountOfObjects = objects.Length;
    }

    private void Update()
    {
        GetAmountOfObjects();
    }
}
