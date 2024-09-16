using UnityEngine;

public class SelectingUnits : MonoBehaviour
{
    private Vector3 startPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = MouseWorld.GetMousePosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log($"Starting position: {startPos}; Ending position: {MouseWorld.GetMousePosition()}");

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPos, MouseWorld.GetMousePosition());

            foreach (Collider2D collider2D in collider2DArray)
            {
                if (collider2D.transform.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    Debug.Log(player.name);
                }
            }
        }
    }

    
}
