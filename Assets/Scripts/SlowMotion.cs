using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{

    private bool _slow = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchTimeMode();
        }
    }

    private void SwitchTimeMode() 
    {
        if (_slow)
        {
            Time.timeScale = 1;
            _slow = false;
        }
        else 
        {
            Time.timeScale = 0.3f;
            _slow = true;
        }
    }
}
