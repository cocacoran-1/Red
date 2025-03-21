using UnityEngine;
using DG.Tweening;

public class BounceInAnimation : MonoBehaviour
{
    [SerializeField] private float bounceDuration = 1f;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;

    private void Start()
    {
        startPoint =new Vector3(transform.position.x - 900f, transform.position.y, transform.position.z);
        endPoint = transform.position;

        // 애니메이션 시작
        AnimateBounce();
    }

    private void AnimateBounce()
    {
        transform.position = startPoint;

        transform.DOJump(endPoint, 150f, 6, bounceDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                // 애니메이션이 완료되면 수행할 작업을 여기에 추가하세요.
            });
    }
}
