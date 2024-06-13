using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace com.ajc.turnbase.manager
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_text;
        [SerializeField] private Button m_button;
        private Ability m_ability;
        private GameManager m_gameManager;

        internal void Initialize(Ability ability, GameManager gameManager)
        {
            m_text.SetText(ability.Name);
            m_ability = ability;
            m_gameManager = gameManager;
            m_button.onClick.AddListener(ClickOnButton);

        }

        public void ClickOnButton()
        {
            m_gameManager.ProcessClick(m_ability);
        }
    }
}

