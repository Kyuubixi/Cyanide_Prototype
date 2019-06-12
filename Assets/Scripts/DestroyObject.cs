using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timer = 2f;

    private void Start()
    {
        StartCoroutine(destroyObject());
    }


    private IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }
}
