using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour, IInitializable
{
    [SerializeField] private UnitBattleStateController _battleStateController;

    private GameObject _regularCamera;
    private GameObject _readyCamera;
    private GameObject _aimCamera;
    
    public void Init()
    {
        var holder = ServiceLocator.Current.Get<CamerasHolder>();

        _regularCamera = holder.RegularCamera;
        _readyCamera = holder.ReadyCamera;
        _aimCamera = holder.AimCamera;
    }

    private void Awake()
    {
        _battleStateController.BattleStateChangedEvent += OnBattleStateChanged;
    }

    private void OnDestroy()
    {
        _battleStateController.BattleStateChangedEvent -= OnBattleStateChanged;
    }

    private void OnBattleStateChanged(int state) 
    {
        _regularCamera.SetActive(false);
        _readyCamera.SetActive(false);
        _aimCamera.SetActive(false);

        switch (state)
        {
            case BattleState.Regular:
                _regularCamera.SetActive(true);
                break;
            case BattleState.Ready:
                _readyCamera.SetActive(true);
                break;
            case BattleState.Aim:
                _aimCamera.SetActive(true);
                break;
        }
    }
}
