using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyRotation : MonoBehaviour
{
    [SerializeField] private Transform _aimObject;

    [SerializeField] private float lerpDuration = 0.5f;
    private Coroutine _rotateRoutine;
    private bool _isRotating = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = _aimObject.transform.position;
        targetPos.y = transform.position.y;

        Vector3 targetDir = targetPos - transform.position;

        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
        angle *= -1;
        //Debug.Log(angle);
       // Debug.LogError(angle);
        if (Mathf.Abs(angle) > 90 && !_isRotating)
        {
            // _player.rotation = _player.rotation *  Quaternion.Euler(0, angle, 0);
            if (_rotateRoutine != null)
            {
                StopCoroutine(_rotateRoutine);
                _rotateRoutine = null;
            }
            _rotateRoutine = StartCoroutine(Rotate90(angle));
        }
    }

    private IEnumerator Rotate90(float angle)
    {
        _isRotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        //Debug.Log("Rotate90");
        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            //Debug.Log("timeElapsed " + timeElapsed);
            yield return null;
        }
        transform.rotation = targetRotation;

        _isRotating = false;
    }
}
