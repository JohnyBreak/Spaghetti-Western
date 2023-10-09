using System;
using System.Collections.Generic;
using UnityEngine;
namespace TestRSM
{
    public abstract class BaseStateMachine<Estate> : MonoBehaviour where Estate : Enum
    {
        protected Dictionary<Estate, BaseState<Estate>> States = new Dictionary<Estate, BaseState<Estate>>();

        protected BaseState<Estate> CurrentState;
        protected bool _isTransitioningState = false;

        private void Start()
        {
            StartMachine();
        }

        public void StartMachine()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            Estate nextState = CurrentState.GetNextState();

            if (_isTransitioningState) return;

            if (nextState.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else
                TransitionToState(nextState);
        }

        private void TransitionToState(Estate nextState)
        {
            _isTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[nextState];
            CurrentState.EnterState();
            _isTransitioningState = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            CurrentState.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            CurrentState.OnTriggerExit(other);
        }
    }
}