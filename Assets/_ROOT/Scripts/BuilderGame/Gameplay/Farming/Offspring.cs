namespace BuilderGame.Gameplay.Farming
{
    using Unit;
    using UnityEngine;
    using Zenject;

    public class Offspring : MonoBehaviour
    {
        [Inject]
        public Player Player { get; set; }

        [Header("Settings")] 
        [SerializeField] private float speed;
        [SerializeField] private float acceleration = 0.1f;

        private bool shouldMove;

        public void Raise()
        {
            shouldMove = true;
        }
        
        private void Update()
        {
            if (shouldMove)
            {
                MoveToPlayer();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PumpingAnimator>();
            if (player == null)
            {
                return;
            }
            player.Pump();
            Destroy(gameObject);
        }

        private void MoveToPlayer()
        {
            speed += acceleration;
            transform.position = Vector3.Lerp(transform.position, Player.centerBone.position, speed * Time.deltaTime);
        }
    }
}