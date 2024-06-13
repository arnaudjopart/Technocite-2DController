using UnityEngine;


namespace Arnaudtest
{
    public class SilverCoin : MonoBehaviour, ICollectable
    {
        public void Collect(TheBestCharacterController theBestCharacterController)
        {
            theBestCharacterController.UnlockDoubleJump();
            Destroy(gameObject);
        }

    }
}

