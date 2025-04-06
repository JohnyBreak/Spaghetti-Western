using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour, IInitializable
{
    private PlayerInput _input;
    [SerializeField] private UnitBattleStateController _battleState;
    //[SerializeField] private bool _twoHandRigs;
    [SerializeField] private GameObject _holder1;
    [SerializeField] private GameObject _holder2;

    //[SerializeField] 
    private BaseGun _pistol1;
    //[SerializeField] 
    private BaseGun _pistol2;

    private BaseGun[] _guns;

    private List<BaseGun> _activeGuns = new List<BaseGun>();

    public bool TwoHands => _activeGuns.Count > 1;
    public bool HasActiveGun => _activeGuns.Count > 0;

    public void Init()
    {
        _input = ServiceLocator.Current.Get<PlayerInput>();
        _input.LMBPressEvent += OnLMBPress;
        _input.LMBReleaseEvent += OnLMBRelease;
        _input.ReloadEvent += OnReload;
    }

    private void Awake()
    {
        _pistol1 = _holder1.GetComponentInChildren<BaseGun>();
        _pistol2 = _holder2.GetComponentInChildren<BaseGun>();
        //_guns = GetComponentsInChildren<BaseGun>(true);
        
    }

    private void OnReload(bool obj)
    {
        foreach (var gun in _activeGuns)
        {
            gun.Reload();
        }
    }

    private void OnLMBPress()
    {
        if(!HasActiveGun) return;
        Debug.LogError("pre shoot");
        if (_battleState.CurrentState == BattleState.Regular) return;

        Debug.LogError("try shoot");
        foreach (var gun in _activeGuns)
        {
            gun.TryShoot();
        }

    }

    private void OnLMBRelease()
    {
        if (!HasActiveGun) return;

        //if (_battleState.CurrentState != BattleState.Aim) return;

        foreach (var gun in _activeGuns)
        {
            if (gun is Submachine submachine) 
            {
                submachine.StopShoot();
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(new BaseGun[] { _pistol1 });
            // 1 pistol
            Debug.Log("1");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(new BaseGun[] { _pistol1, _pistol2 });
            // 2 pistol
            Debug.Log("2");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(new BaseGun[] { });
            // 1 pistol
            Debug.Log("3");
            return;
        }
    }

    private void SetActiveWeapon(BaseGun[] _guns) 
    {
        _activeGuns.Clear();
        _activeGuns.Capacity = 2;

        for (int i = 0; i < _guns.Length; i++)
        {
            if (_guns[i] == null) continue;
                
            _activeGuns.Add(_guns[i]);
        }
    }
}
