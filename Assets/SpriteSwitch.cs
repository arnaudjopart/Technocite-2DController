using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitch : MonoBehaviour
{

    [SerializeField] private Sprite[] m_sprites;
    private WaitForSeconds m_waitCoroutine;
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]private int m_currentSpriteIndex;

    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_waitCoroutine = new WaitForSeconds(.1f);
        while (true)
        {
            yield return m_waitCoroutine;
            m_currentSpriteIndex += 1;
            m_currentSpriteIndex %= m_sprites.Length;

            m_spriteRenderer.sprite = m_sprites[m_currentSpriteIndex];
        }

        
    }

    // Update is called once per fram
}
