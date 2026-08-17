using UnityEngine;
using UnityEngine.Events;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Triggers <see cref="OnEntered"/> and <see cref="OnExited"/> Unity Events 
    /// when a GameObject with the given tag enters/exits the area.
    /// </summary>
    [SelectionBase]
    [DisallowMultipleComponent]
    public class AreaTriggerUnityEvent : AreaTrigger
    {
        [Space]
        [Tooltip("Event fired when entering in the trigger area.")]
        public UnityEvent<GameObject> OnInstanceEntered;
        [Tooltip("Event fired when exiting from the trigger area.")]
        public UnityEvent<GameObject> OnInstanceExited;

        protected override void Enter(GameObject interactor)
        {
            base.Enter(interactor);
            OnInstanceEntered?.Invoke(interactor);
        }

        protected override void Exit()
        {
            OnInstanceExited?.Invoke(Interactor);
            base.Exit();
        }
    }
}