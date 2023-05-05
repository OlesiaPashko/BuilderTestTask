namespace BuilderGame.Gameplay.Farming
{
    using DG.Tweening;
    using Unit;
    using UnityEngine;
    using Zenject;

    public class Offspring : MonoBehaviour
    {
        [Inject]
        public Player Player { get; set; }

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
    }
}