using com.ajc.turnbase.manager;
using com.ajc.turnbase.scriptableObject;
using System;
using TMPro;
using UnityEngine;

namespace com.ajc.turnbase
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterStatScriptableObject m_stats;
        [SerializeField] private int m_healthPoints =10;
        private GameManager m_manager;
        [SerializeField] private GameObject m_highlightSprite;
        [SerializeField] private TMP_Text m_healthText;

        public enum TYPE
        {
            FRIEND,
            FOE
        }

        public TYPE m_type;
        private bool m_isDone;

        public int GetHealthPoints()
        {
            return m_healthPoints;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_healthPoints = m_stats.m_startHealthPoints;
        }

        // Update is called once per frame
        void Update()
        {
            m_healthText.SetText(m_healthPoints.ToString());
        }

        private void OnMouseDown()
        {
            if (m_isDone) return;
            switch (m_type)
            {
                case TYPE.FRIEND:
                    m_manager.Select(this);
                    m_highlightSprite.SetActive(true);
                    break;
                case TYPE.FOE:
                    m_manager.SetTarget(this);
                    break;
            }
        }

        public void SetManager(GameManager _manager)
        {
            m_manager = _manager;
        }

        public void Deselect()
        {
            m_highlightSprite.SetActive(false);
        }

        public void Attack(Character _target)
        {
            _target.TakeDamage(m_stats.m_damagePoints);
            m_isDone = true;

        }

        private void TakeDamage(int m_damagePoints)
        {
            m_healthPoints -= m_damagePoints;
        }
    }
}

