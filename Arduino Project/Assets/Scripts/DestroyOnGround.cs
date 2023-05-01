using UnityEngine;

public class DestroyOnGround : MonoBehaviour
{
    public float groundY;       
    private Collider2D objectCollider;  

    private void Start()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!objectCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return; 
        }

        Bounds bounds = objectCollider.bounds;
        float objectBottomY = bounds.min.y; 

        if (objectBottomY <= groundY)
        {
            Destroy(gameObject); 
        }
    }
}
