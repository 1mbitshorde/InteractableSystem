using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Interactor for <see cref="IInteractable"/> implementations.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Interactor : MonoBehaviour
    {
        [SerializeField, Tooltip("The layers used on the cast collision.")]
        private LayerMask collisions;
        [SerializeField, Min(0.01f), Tooltip("The radius used on the Sphere Cast.")]
        private float radius = 0.1F;
        [SerializeField, Min(0f), Tooltip("The maximum distance used on the Sphere Cast.")]
        private float distance = 10F;

        /// <summary>
        /// The current Interactable. Can be null.
        /// </summary>
        public IInteractable CurrentInteractable { get; private set; }

        public LayerMask Collisions
        {
            get => collisions;
            set => collisions = value;
        }

        public float Radius
        {
            get => radius;
            set => radius = Mathf.Min(0.01F, value);
        }

        public float Distance
        {
            get => distance;
            set => distance = Mathf.Min(0F, value);
        }

        private void Update() => UpdateCast();
        private void OnDisable() => DisposeCurrentInteractable();

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var isExpanded = UnityEditorInternal.InternalEditorUtility.GetIsInspectorExpanded(this);
            if (isExpanded) DrawCast();
        }

        private void DrawCast()
        {
            var hasHit = CurrentInteractable != null;
            var color = hasHit ? Color.red : Color.green;
            var origin = GetOrigin();
            var direction = GetDirection();
            var end = origin + direction * Distance;

            Debug.DrawLine(origin, end, color);
            ActionCode.Shapes.ShapeDebug.DrawSphere(end, diameter: Radius * 2f, color);
        }
#endif

        public void TryInteract() => CurrentInteractable?.TryInteract();

        private void UpdateCast()
        {
            // SphereCast is more performative than BoxCast.
            var hasHit = Physics.SphereCast(
                GetOrigin(),
                Radius,
                GetDirection(),
                out RaycastHit hit,
                Distance,
                Collisions
            );

            if (!hasHit)
            {
                DisposeCurrentInteractable();
                return;
            }

            var hasInteractable = hit.transform.TryGetComponent(out IInteractable interactable);
            if (!hasInteractable)
            {
                DisposeCurrentInteractable();
                return;
            }

            var hasNewInteractable = CurrentInteractable != interactable;
            if (hasNewInteractable)
            {
                DisposeCurrentInteractable();
                CurrentInteractable = interactable;
                CurrentInteractable.ChangeAvailability(
                    CurrentInteractable.CanInteract()
                );
            }
        }

        private void DisposeCurrentInteractable()
        {
            CurrentInteractable?.ChangeAvailability(false);
            CurrentInteractable = null;
        }

        private Vector3 GetOrigin() => transform.position - GetDirection() * 0.01f;
        private Vector3 GetDirection() => transform.forward;
    }
}