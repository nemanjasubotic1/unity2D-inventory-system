using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteSetSwap : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public event EventHandler OnMovingOnSide;
    public event EventHandler OnMovingOnFront;
    public event EventHandler OnMovingOnBack;
    public event EventHandler OnIdle;

    private Vector2 Move => playerController.Move;

    private Transform visualFront;
    private Transform visualSide;
    private Transform visualBack;

    private bool isIdle;

    private List<Transform> visualTransforms;

    private void Awake()
    {
        visualFront = transform.Find("VisualFront");
        visualSide = transform.Find("VisualSide");
        visualBack = transform.Find("VisualBack");

        visualTransforms = new List<Transform>()
        {
            visualFront,
            visualSide,
            visualBack
        };

        PrepareSprite(visualFront);
    }


    private void Update()
    {
        SpriteSwap();
      
    }

    public Vector2 GetRigidBodyVelocity() => playerController.GetRigidBodyVelocity();

    private void SpriteSwap()
    {

        switch (Move.x)
        {
            case > 0:
                PrepareSprite(visualSide, Move.x);
                OnMovingOnSide?.Invoke(this, EventArgs.Empty);
                break;

            case < 0:
                PrepareSprite(visualSide, Move.x);
                OnMovingOnSide?.Invoke(this, EventArgs.Empty);
                break;
        }

        switch (Move.y)
        {
            case > 0:
                PrepareSprite(visualBack);
                OnMovingOnBack?.Invoke(this, EventArgs.Empty);
                break;

            case < 0:
                PrepareSprite(visualFront);
                OnMovingOnFront?.Invoke(this, EventArgs.Empty);
                break;
        }

        isIdle = Move.x == 0 && Move.y == 0;

        if (isIdle)
        {
            // Idle
            OnIdle?.Invoke(this, EventArgs.Empty);
        }

    }

    private void PrepareSprite(Transform currentActiveSprite, float localScaleX = 1f)
    {
        foreach (Transform transform in visualTransforms)
        {
            transform.gameObject.SetActive(false);
        }

        currentActiveSprite.gameObject.SetActive(true);
        currentActiveSprite.localScale = new Vector3(localScaleX, currentActiveSprite.localScale.y, currentActiveSprite.localScale.z);
    }

    public bool IsIdle()
    {
        return isIdle;
    }
}
