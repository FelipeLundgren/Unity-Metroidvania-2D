using UnityEngine;

public class isGroundChecker : MonoBehaviour
{
    [SerializeField] private Transform checkerPosition;
    [SerializeField] private Vector2 checkerSize;
    [SerializeField] private LayerMask groundLayer;

    public bool isGrounded()
    {
        return Physics2D.OverlapBox
            (checkerPosition.position, checkerSize, 0, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (checkerPosition == null) return;
        if (isGrounded())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(checkerPosition.position, checkerSize);
        
    }
}
