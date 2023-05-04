namespace BuilderGame.Gameplay.Farming
{
    using Unit;
    using UnityEngine;

    public class Ground : MonoBehaviour
    {
        [SerializeField] private PumpingAnimator pumpingAnimator;

        private void OnTriggerEnter(Collider other)
        {
            pumpingAnimator.Pump();
        }
    }
}