using UnityEngine;

public class Player : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerCameraHandler _handler;

    public void Init()
    {
        var components = GetComponentsInChildren<IInitializable>();

        foreach (var component in components) 
        {
            if (component == (IInitializable)this)
            {
                continue;
            }
            component.Init();
        }

        //_handler.Init();
    }
}
