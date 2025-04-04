using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    protected int _damageHash = Animator.StringToHash("TakeDamage");

    public void TriggerDamage()
    {
        _animator.SetTrigger(_damageHash);
    }
}
