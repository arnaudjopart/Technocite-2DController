using UnityEngine;

public class SilverCoin : MonoBehaviour, ICollectable
{
    public void Collect(TheBestCharacterController theBestCharacterController)
    {
        theBestCharacterController.UnlockDoubleJump();
        Destroy(gameObject);
    }
}
