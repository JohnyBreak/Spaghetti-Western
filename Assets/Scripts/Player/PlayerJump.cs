using UnityEngine;

public class PlayerJump : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerAnimation _locomotion;
    private PlayerInput _playerInput;

    public void Init()
    {
        _playerInput = ServiceLocator.Current.Get<PlayerInput>();

        _playerInput.SpacePressEvent += Jump;
    }

    private void OnDestroy()
    {
        _playerInput.SpacePressEvent -= Jump;
    }

    private void Jump()
    {
        _locomotion.CrossFade(_locomotion.ForwardJumpHash);
    }
}
