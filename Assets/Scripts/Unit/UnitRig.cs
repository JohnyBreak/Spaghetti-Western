using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditor.Progress;

public class UnitRig : MonoBehaviour
{
    [SerializeField] private Rig _spineRig;

    [SerializeField] private Rig _rightHandHolsterRig;
    [SerializeField] private Rig _rightHandAimRig;
    [SerializeField] private Rig _rightHandIKRig;

    [SerializeField] private Rig _leftHandHolsterRig;
    [SerializeField] private Rig _leftHandAimRig;
    [SerializeField] private Rig _leftHandIKRig;


    //[SerializeField] private List<MultiAimConstraint> _spineConstraints;
    //[SerializeField] private List<MultiAimConstraint> _rightHandConstraints;
    //[SerializeField] private List<MultiAimConstraint> _leftHandConstraints;

    private Coroutine _spineRigWeightRoutine;

    private Coroutine _rightHandHolsterRigWeightRoutine;
    private Coroutine _rightHandIKRigWeightRoutine;
    private Coroutine _rightHandAimRigWeightRoutine;

    private Coroutine _leftHandHolsterRigWeightRoutine;
    private Coroutine _leftHandAimRigWeightRoutine;
    private Coroutine _leftHandIKRigWeightRoutine;

    public void ToggleSpineRig(bool enable) 
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _spineRigWeightRoutine, _spineRig);
    }

    public void ToggleRightHolsterRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _rightHandHolsterRigWeightRoutine, _rightHandHolsterRig);
    }

    public void ToggleRightIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _rightHandIKRigWeightRoutine, _rightHandIKRig);
    }

    public void ToggleRightAimRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _rightHandAimRigWeightRoutine, _rightHandAimRig);
    }

    public void ToggleLeftHolsterRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _leftHandHolsterRigWeightRoutine, _leftHandHolsterRig);
    }

    public void ToggleLeftIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _leftHandIKRigWeightRoutine, _leftHandIKRig);
    }

    public void ToggleLeftAimRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _leftHandAimRigWeightRoutine, _leftHandAimRig);
    }

    private void UpdateRig(float end, Coroutine routine, Rig rig)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        routine = StartCoroutine(SmoothRig(end, rig));
    }

    private IEnumerator SmoothRig(float end, Rig rig)
    {
        float elapsedTime = 0;
        float waitTime = 0.2f;
        while (elapsedTime <= waitTime)
        {
            float start = rig.weight;
            rig.weight = Mathf.Lerp(start, end, elapsedTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        rig.weight = end;
    }

    //private void SpineRig(float end)
    //{
    //    if (_spineRigWeightRoutine != null)
    //    {
    //        StopCoroutine(_spineRigWeightRoutine);
    //        _spineRigWeightRoutine = null;
    //    }
    //    _spineRigWeightRoutine = StartCoroutine(SmoothRig(end, _spineConstraints));
    //}

    //private void RHandRig(float end)
    //{
    //    if (_rightHandRigWeightRoutine != null)
    //    {
    //        StopCoroutine(_rightHandRigWeightRoutine);
    //        _rightHandRigWeightRoutine = null;
    //    }
    //    _rightHandRigWeightRoutine = StartCoroutine(SmoothRig(end, _rightHandConstraints));
    //}

    //private void LHandRig(float end)
    //{
    //    if (_leftHandRigWeightRoutine != null)
    //    {
    //        StopCoroutine(_leftHandRigWeightRoutine);
    //        _leftHandRigWeightRoutine = null;
    //    }
    //    _leftHandRigWeightRoutine = StartCoroutine(SmoothRig(end, _leftHandConstraints));
    //}

    //private IEnumerator SmoothRig(float end, List<MultiAimConstraint> constraints)
    //{
    //    //if (!_twoHandRigs) yield break;
    //    float elapsedTime = 0;
    //    float waitTime = 0.2f;
    //    while (elapsedTime <= waitTime)
    //    {
    //        foreach (var item in constraints)
    //        {
    //            float start = item.weight;
    //            item.weight = Mathf.Lerp(start, end, elapsedTime);
    //        }
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    foreach (var item in constraints)
    //    {
    //        float start = item.weight;
    //        item.weight = end;
    //    }
    //}
}
