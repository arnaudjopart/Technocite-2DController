using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.ajc.turnbase.manager
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private Character[] m_playerTeam;
        [SerializeField] private Character[] m_enemyTeam;
        private enum STATE
        {
            PLAYER,
            COMPUTER
        }

        [SerializeField] private STATE m_state;
        private Character m_currentSelectedCharacter;
        private Character m_currentTarget;
        [SerializeField] private Transform m_targetReticule;
        [SerializeField] private GameObject m_validateButton;

        void Start()
        {
            foreach (Character character in m_playerTeam)
            {
                character.SetManager(this);
            }
            foreach (Character character in m_enemyTeam)
            {
                character.SetManager(this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            var isPlayerTeamBusted = EvaluateLives(m_playerTeam);
            var isEnemyTeamAnnihilated = EvaluateLives(m_enemyTeam);
            
            if (isPlayerTeamBusted)
            {
                print("Player Team is dead");
            }

            if(isEnemyTeamAnnihilated)
            {
                print("Enemy Team is dead");
            }
        }

        private bool EvaluateLives(Character[] m_playerTeam)
        {
            for (var i = 0; i < m_playerTeam.Length; i++)
            {
                if (m_playerTeam[i].GetHealthPoints()>0) return false;
            }

            return true; 
        }

        public void GoNextTurn()
        {
            if(m_state == STATE.PLAYER) m_state = STATE.COMPUTER;
            else m_state = STATE.PLAYER;

            TellMeWhich();
        }

        private void TellMeWhich()
        {
            switch (m_state)
            {
                case STATE.PLAYER:
                    print("Player");
                    break;
                case STATE.COMPUTER:
                    print("COMPUTER");
                    break;
            }
        }

        public void Select(Character _character)
        {
            if(m_currentSelectedCharacter!=null) m_currentSelectedCharacter.Deselect();
            m_currentSelectedCharacter = _character;
        }

        public void SetTarget(Character _character)
        {
            if(!m_currentSelectedCharacter) return;
            m_currentTarget = _character;
            m_validateButton.SetActive(true);
            m_targetReticule.position = m_currentTarget.transform.position;
            m_targetReticule.gameObject.SetActive(true);

        }

        public void AttackCurrentTarget()
        {
            m_currentSelectedCharacter.Deselect();
            
            m_currentSelectedCharacter.Attack(m_currentTarget);
            m_validateButton.SetActive(false);
            m_targetReticule.gameObject.SetActive(false) ;

            m_currentTarget = null;
            m_currentSelectedCharacter=null;
        }
    }

}

