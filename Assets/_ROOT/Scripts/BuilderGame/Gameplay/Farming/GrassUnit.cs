namespace BuilderGame.Gameplay.Farming
{
    using System;
    using UnityEngine;

    public class GrassUnit : MonoBehaviour
    {
        public float Size => 1f;

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}