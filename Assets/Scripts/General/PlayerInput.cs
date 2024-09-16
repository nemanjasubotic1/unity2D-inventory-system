
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    private PlayerInputActions inputActions;

    private InputAction move;

    public FrameInput FrameIn { get; private set; }

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        move = inputActions.Player.Move;
    }

    private void Update()
    {
        FrameIn = GetFrameInput();
    }

    private FrameInput GetFrameInput()
    {
        FrameInput frameInput = new FrameInput()
        {
            move = this.move.ReadValue<Vector2>()
        };
        return frameInput;
    }


    public struct FrameInput
    {
        public Vector2 move;
    }
}
