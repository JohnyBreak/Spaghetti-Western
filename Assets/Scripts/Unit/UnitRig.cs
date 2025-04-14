using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnitRig : MonoBehaviour
{
    [SerializeField] private Rig _spineRig;
    [SerializeField] private Rig _rightHandIKRig;
    [SerializeField] private Rig _leftHandIKRig;
    [SerializeField] private Rig _twoHandedIdleIKRig;

    private Coroutine _spineRigWeightRoutine;

    private Coroutine _rightHandHolsterRigWeightRoutine;
    private Coroutine _rightHandIKRigWeightRoutine;
    private Coroutine _rightHandAimRigWeightRoutine;

    private Coroutine _leftHandHolsterRigWeightRoutine;
    private Coroutine _leftHandAimRigWeightRoutine;
    private Coroutine _leftHandIKRigWeightRoutine;
    private Coroutine _twoHandedIdleIKRigWeightRoutine;

    public void ToggleSpineRig(bool enable) 
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _spineRigWeightRoutine, _spineRig);
    }

    public void ToggleRightIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _rightHandIKRigWeightRoutine, _rightHandIKRig);
    }

    public void ToggleLeftIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _leftHandIKRigWeightRoutine, _leftHandIKRig);
    }

    public void ToggleTwoHandIdleIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _twoHandedIdleIKRigWeightRoutine, _twoHandedIdleIKRig);
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
        float waitTime = 0.05f;
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
