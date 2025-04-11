using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnitRig : MonoBehaviour
{
    [SerializeField] private Rig _spineRig;

    //[SerializeField] private Rig _rightHandHolsterRig;
    //[SerializeField] private Rig _rightHandAimRig;
    [SerializeField] private Rig _rightHandIKRig;

    //[SerializeField] private Rig _leftHandHolsterRig;
    //[SerializeField] private Rig _leftHandAimRig;
    [SerializeField] private Rig _leftHandIKRig;

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

    //public void ToggleRightHolsterRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, _rightHandHolsterRigWeightRoutine, _rightHandHolsterRig);
    //}

    public void ToggleRightIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _rightHandIKRigWeightRoutine, _rightHandIKRig);
    }

    //public void ToggleRightAimRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, _rightHandAimRigWeightRoutine, _rightHandAimRig);
    //}

    //public void ToggleLeftHolsterRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, _leftHandHolsterRigWeightRoutine, _leftHandHolsterRig);
    //}

    public void ToggleLeftIKRig(bool enable)
    {
        float value = enable ? 1 : 0;

        UpdateRig(value, _leftHandIKRigWeightRoutine, _leftHandIKRig);
    }

    //public void ToggleLeftAimRig(bool enable)
    //{
    //    float value = enable ? 1 : 0;

    //    UpdateRig(value, _leftHandAimRigWeightRoutine, _leftHandAimRig);
    //}

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
