using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigIndexes 
{
    public const int Spine = 0;
    public const int RightHand = 1;
    public const int LeftHand = 2;
    public const int TwoHandedIdle = 3;
    public const int TwoHandedAim = 4;
    public const int SingleHandedWeaponIdle = 5;
}

public class UnitRig : MonoBehaviour
{
    [SerializeField] private Rig _spineRig;
    [SerializeField] private Rig _rightHandIKRig;
    [SerializeField] private Rig _leftHandIKRig;
    [SerializeField] private Rig _twoHandedIdleIKRig;
    [SerializeField] private Rig _twoHandedAimIKRig;
    [SerializeField] private Rig _singleHandedWeaponIdleIKRig;

    private float _duration;
    private Dictionary<int, Coroutine> _routinesMap = new();
    private Dictionary<int, Rig> _rigsMap = new();

    public void Awake()
    {
        _rigsMap.Add(RigIndexes.Spine, _spineRig);
        _rigsMap.Add(RigIndexes.RightHand, _rightHandIKRig);
        _rigsMap.Add(RigIndexes.LeftHand, _leftHandIKRig);
        _rigsMap.Add(RigIndexes.TwoHandedIdle, _twoHandedIdleIKRig);
        _rigsMap.Add(RigIndexes.TwoHandedAim, _twoHandedAimIKRig);
        _rigsMap.Add(RigIndexes.SingleHandedWeaponIdle, _singleHandedWeaponIdleIKRig);
    }

    //public void ToggleSpineRig(bool enable) 
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, RigIndexes.Spine, _spineRig);
    //}

    //public void ToggleRightIKRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, RigIndexes.RightHand, _rightHandIKRig);
    //}

    //public void ToggleLeftIKRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, RigIndexes.LeftHand, _leftHandIKRig);
    //}

    //public void ToggleTwoHandIdleIKRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, RigIndexes.TwoHandedIdle, _twoHandedIdleIKRig);
    //}

    //public void ToggleTwoHandAimIKRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, RigIndexes.TwoHandedAim, _twoHandedAimIKRig);
    //}

    public void ToggleRig(int index, bool enable, float duration = 0.05f) 
    {
        float value = enable ? 1 : 0;

        if (_rigsMap.TryGetValue(index, out var rig) == false) 
        {
            return;
        }
        //test
        duration = 0;
        UpdateRig(value, index, rig, duration);
    }

    private void UpdateRig(float end, int index, Rig rig, float duration)
    {
        if (_routinesMap.ContainsKey(index))
        {
            if(_routinesMap[index] != null)
            {
                StopCoroutine(_routinesMap[index]);
                _routinesMap.Remove(index);
            }
        }
        _routinesMap[index] = StartCoroutine(SmoothRig(end, rig, duration));
    }

    private IEnumerator SmoothRig(float end, Rig rig, float duration)
    {
        if (rig.weight == end)
        {

            yield break;
        }

        if (duration == 0) 
        {
            rig.weight = end;
            yield break;
        }

        float elapsedTime = 0;
        float waitTime = duration;
        while (elapsedTime <= waitTime)
        {
            float start = rig.weight;
            rig.weight = Mathf.Lerp(start, end, elapsedTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        rig.weight = end;
    }
}
