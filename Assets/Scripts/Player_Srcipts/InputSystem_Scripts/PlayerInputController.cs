using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour 
{
    public Vector2 moveInput { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool runPressed { get; private set; }
    public bool sneakPressed { get; private set; }
    public bool flyPressed { get; private set; }
    public bool rightClick { get; private set; }
    public bool leftClick { get; private set; }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        moveInput = new Vector2(
        Keyboard.current.dKey.isPressed ? 1 :
        Keyboard.current.aKey.isPressed ? -1 : 0,

        Keyboard.current.wKey.isPressed ? 1 :
        Keyboard.current.sKey.isPressed ? -1 : 0);

        jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        runPressed = Keyboard.current.leftShiftKey.isPressed;
        sneakPressed = Keyboard.current.leftCtrlKey.isPressed;
        flyPressed = Keyboard.current.mKey.isPressed;

        rightClick = Mouse.current.rightButton.isPressed;
        leftClick = Mouse.current.leftButton.isPressed;
      
    }
}
