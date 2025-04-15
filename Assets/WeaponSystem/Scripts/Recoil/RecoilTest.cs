using DG.Tweening;
using UnityEngine;

public class RecoilTest : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _rotation;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            PlayRecoilTween();
        }
    }

    private void PlayRecoilTween() 
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        var seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMove(_position, 0.1f).SetEase(Ease.InOutSine));
        seq.Join(transform.DOLocalRotate(_rotation, 0.1f).SetEase(Ease.InOutSine));

        seq.Append(transform.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.InOutSine));
        seq.Join(transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.InOutSine));

        seq.Play();
    }
}
