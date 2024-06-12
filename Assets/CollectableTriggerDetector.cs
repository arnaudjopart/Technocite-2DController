using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTriggerDetector : MonoBehaviour
{
    private TheBestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.parent.GetComponent<TheBestCharacterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICollectable>(out var collectable)) { collectable.Collect(controller); }

    }
}
