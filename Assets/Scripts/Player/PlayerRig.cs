using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditor.Progress;

public class PlayerRig : MonoBehaviour
{
    [SerializeField] private bool _twoHandRigs;

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

    [SerializeField] private PlayerBattleStateController _battleStateController;

    private Coroutine _spineRigWeightRoutine;

    private Coroutine _rightHandHolsterRigWeightRoutine;
    private Coroutine _rightHandIKRigWeightRoutine;
    private Coroutine _rightHandAimRigWeightRoutine;

    private Coroutine _leftHandHolsterRigWeightRoutine;
    private Coroutine _leftHandAimRigWeightRoutine;
    private Coroutine _leftHandIKRigWeightRoutine;

    private void Awake()
    {
        _battleStateController.BattleStateChangedEvent += OnBattleStateChanged;
    }

    private void OnDestroy()
    {
        _battleStateController.BattleStateChangedEvent -= OnBattleStateChanged;
    }

    private void OnBattleStateChanged(BattleState state)
    {
        switch (state)
        {
            case BattleState.Regular:

                UpdateRig(0, _spineRigWeightRoutine, _spineRig);
                
                UpdateRig(1, _rightHandHolsterRigWeightRoutine, _rightHandHolsterRig);
                UpdateRig(0, _rightHandIKRigWeightRoutine, _rightHandIKRig);
                UpdateRig(0, _rightHandAimRigWeightRoutine, _rightHandAimRig);
                if (_twoHandRigs)
                {
                    UpdateRig(1, _leftHandHolsterRigWeightRoutine, _leftHandHolsterRig);
                    UpdateRig(0, _leftHandIKRigWeightRoutine, _leftHandIKRig);
                    UpdateRig(0, _leftHandAimRigWeightRoutine, _leftHandAimRig);
                }
                //SpineRig(0);
                //RHandRig(0);
                //if (_twoHandRigs)LHandRig(0);

                break;
            case BattleState.Ready:

                UpdateRig(0, _spineRigWeightRoutine, _spineRig);
                UpdateRig(1, _rightHandHolsterRigWeightRoutine, _rightHandHolsterRig);
                UpdateRig(0, _rightHandIKRigWeightRoutine, _rightHandIKRig);
                UpdateRig(0, _rightHandAimRigWeightRoutine, _rightHandAimRig);
                if (_twoHandRigs)
                {
                    UpdateRig(1, _leftHandHolsterRigWeightRoutine, _leftHandHolsterRig);
                    UpdateRig(0, _leftHandIKRigWeightRoutine, _leftHandIKRig);
                    UpdateRig(0, _leftHandAimRigWeightRoutine, _leftHandAimRig);
                }
                //SpineRig(1);
                //RHandRig(1);
                //if (_twoHandRigs)LHandRig(1);

                break;
            case BattleState.Aim:

                UpdateRig(1, _spineRigWeightRoutine, _spineRig);

                UpdateRig(0, _rightHandHolsterRigWeightRoutine, _rightHandHolsterRig);
                UpdateRig(1, _rightHandIKRigWeightRoutine, _rightHandIKRig);
                UpdateRig(1, _rightHandAimRigWeightRoutine, _rightHandAimRig);

                if (_twoHandRigs)
                {
                    UpdateRig(0, _leftHandHolsterRigWeightRoutine, _leftHandHolsterRig);
                    UpdateRig(1, _leftHandIKRigWeightRoutine, _leftHandIKRig);
                    UpdateRig(1, _leftHandAimRigWeightRoutine, _leftHandAimRig);
                }
                //SpineRig(1);
                //RHandRig(1);
                //if (_twoHandRigs)LHandRig(1);

                break;
        }
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

    private IEnumerator SmoothRig(float end, Rig rig)//List<MultiAimConstraint> constraints)
    {
        //if (!_twoHandRigs) yield break;
        float elapsedTime = 0;
        float waitTime = 0.2f;
        while (elapsedTime <= waitTime)
        {
            float start = rig.weight;
            rig.weight = Mathf.Lerp(start, end, elapsedTime);
            //foreach (var item in constraints)
            //{
            //    float start = item.weight;
            //    item.weight = Mathf.Lerp(start, end, elapsedTime);
            //}
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //foreach (var item in constraints)
        //{
        //    float start = item.weight;
        //    item.weight = end;
        //}
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
