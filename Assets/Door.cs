using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject m_message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter2D (Collider2D other)
    {
        m_message.SetActive(true);
    }*/
    private void OnTriggerExit2D(Collider2D other)
    {
        m_message.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        m_message.SetActive(true);
    }
}
