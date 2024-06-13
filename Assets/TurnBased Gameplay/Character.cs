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
        private CharacterAbilitiesManagerBase m_abilityManager;
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

        public Ability[] GetListOfAbilities()
        {
            return m_abilityManager.abilities.ToArray();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_abilityManager = GetComponent<CharacterAbilitiesManagerBase>();
            m_healthPoints = m_stats.m_startHealthPoints;
            
        }

        // Update is called once per frame
        void Update()
        {
            m_healthText.SetText(m_healthPoints.ToString());
        }

        private void OnMouseDown()
        {
            switch (m_type)
            {
                case TYPE.FRIEND:
                    m_manager.Select(this);
                    
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

        public void Attack(Character _target, int _damage)
        {
            _target.TakeDamage(_damage);
            m_isDone = true;

        }

        public void Attack(Character _target)
        {
            _target.TakeDamage(m_stats.m_damagePoints);
            m_isDone = true;

        }

        public void TakeDamage(int m_damagePoints)
        {
            m_healthPoints -= m_damagePoints;
        }

        internal int GetDamage()
        {
            return m_stats.m_damagePoints;
        }

        internal void DecreaseAttackPoint(int m_cost)
        {
            throw new NotImplementedException();
        }

        internal void Heal(Character m_currentTarget)
        {
            m_currentTarget.AddHealth(2);
        }

        private void AddHealth(int v)
        {
            m_healthPoints += v;
        }

        internal void BigAttack(Character m_currentTarget)
        {
            throw new NotImplementedException();
        }

        internal void HealAll(Character m_currentTarget)
        {
            throw new NotImplementedException();
        }

        internal void HealAll()
        {
            throw new NotImplementedException();
        }

        internal void Select()
        {
            m_highlightSprite.SetActive(true);
        }
    }
}

