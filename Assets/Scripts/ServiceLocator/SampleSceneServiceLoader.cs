using UnityEngine;

public class SampleSceneServiceLoader : MonoBehaviour
{
    [SerializeField] private CamerasHolder _camerasHolder;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        ServiceLocator.Initialize();

        Register();
        Init();
    }

    private void Register() 
    {
        ServiceLocator.Current.Register(_camerasHolder);
        ServiceLocator.Current.Register(_playerInput);
    }

    private void Init() 
    {
        _player.Init();
    }
}
