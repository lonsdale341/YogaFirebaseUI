using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateManager
    {
        private Stack<BaseState> stateStack;

        public StateManager()
        {
            stateStack = new Stack<BaseState>();
            stateStack.Push(new BaseState());
        }

        public void PushState(BaseState newState)
        {
            Debug.Log("PushState_Start=" + stateStack.Count);
            newState.manager = this;
            CurrentState().Suspend();
            stateStack.Push(newState);
            Debug.Log("PushState_End=" + stateStack.Count);
            newState.Initialize();
            
        }
        // Ends the currently-running state, and resumes whatever is next
        // down the line.
        public void PopState()
        {
            StateExitValue result = CurrentState().Cleanup();
            stateStack.Pop();
            CurrentState().Resume(result);
        }
        // Clears out all states, leaving just newState as the sole state
        // on the stack.  Since PopState is called, all underlying states
        // still get to respond to Resume() and Cleanup().  Mainly useful
        // for soft resets where we don't want to care about how many levels
        // of menu we have below us.
        public void ClearStack(BaseState newState)
        {
            while (stateStack.Count > 1)
            {
                PopState();
            }
            SwapState(newState);
        }
        // Switches the current state for a new one, without disturbing
        // anything below.  Different from Pop + Push, in that the next
        // state down never gets resumed/suspended.
        // Переключает текущее состояние на новое, не нарушая ничего ниже. В отличие от Pop + Push, в следующем
        public void SwapState(BaseState newState)
        {
            Debug.Log("SwapState_Start=" + stateStack.Count);
            newState.manager = this;
            CurrentState().Cleanup();
            
            stateStack.Pop();
            stateStack.Push(newState);
            Debug.Log("SwapState_End=" + stateStack.Count);
            CurrentState().Initialize();
            
        }
        // Called by the main game every update.
        public void Update()
        {
            CurrentState().Update();
        }
        // Called by the main game every fixed update.
        // Note that during most UI and menus, the update timestep
        // is set to 0, so this function will not fire.
        public void FixedUpdate()
        {
            CurrentState().FixedUpdate();
        }
        public BaseState CurrentState()
        {
            return stateStack.Peek();
        }
        // When GUIButton receives a Unity UI event, it reports it via
        // this function.  (Which then directs it to whichever state is active.)
        public void HandleUIEvent(GameObject source, object eventData)
        {
            CurrentState().HandleUIEvent(source, eventData);
        }

    }
}
