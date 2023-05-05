namespace BuilderGame.Gameplay.Farming
{
    using DG.Tweening;
    using Unit;
    using Unity.VisualScripting;
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
            var startParent = transform.parent;
            transform.parent = Player.transform;
            transform.localScale = Vector3.zero;
            transform.DOScale(new Vector3(2f,2f,2f), 0.7f).SetEase(Ease.OutQuad);

            transform.DOLocalJump(Vector3.up, 0.8f, 1, 0.9f).OnComplete(
                () =>
                {
                    transform.DOScale(Vector3.zero, 0.7f)
                        .OnComplete(() =>
                        {
                            Player.GetComponent<PumpingAnimator>().Pump();
                            transform.parent = startParent;
                            Destroy(gameObject);
                        });
                });
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