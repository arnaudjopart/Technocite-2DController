using Arnaudtest;
using UnityEngine;

public class SwordGarb : MonoBehaviour, ICollectable
{
    public void Collect(TheBestCharacterController theBestCharacterController)
    {
        theBestCharacterController.GetSword();
        Destroy(gameObject);
    }

}
