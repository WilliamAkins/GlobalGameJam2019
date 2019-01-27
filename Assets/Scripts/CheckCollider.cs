using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    private StickyFingers stickyFingers;
    // Start is called before the first frame update
    void Start()
    {
        stickyFingers = GameObject.Find("Player").GetComponent<StickyFingers>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Sticky"))
            stickyFingers.onCollisionOccuring(transform.gameObject, col.gameObject);
    }
}
