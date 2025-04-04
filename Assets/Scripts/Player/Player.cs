using UnityEngine;

public class Player : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerCameraHandler _handler;

    public void Init()
    {
        _handler.Init();
    }
}
