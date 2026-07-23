using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tetris
{
    public class GameInputManager : Singleton<GameInputManager>
    {
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnSoftDrop;
        public event Action OnHardDrop;
        public event Action OnRotateLeft;
        public event Action OnRotateRight;
        public event Action OnHold; // For future use
    
        private MyInputActions inputActions;
        
        private bool isSoftDropHeld = false;
        private bool isMoveLeftHeld = false;
        private bool isMoveRightHeld = false;
    
        protected override void Awake()
        {
            base.Awake();
            inputActions = new MyInputActions();
        }
    
        private void OnEnable()
        {
            inputActions?.Enable();
            
            // Manually subscribe to the separated events
            inputActions.Tertromino.MoveLeft.performed += HandleMoveLeft;
            inputActions.Tertromino.MoveLeft.started += HandleMoveLeftState;
            inputActions.Tertromino.MoveLeft.canceled += HandleMoveLeftState;
    
            inputActions.Tertromino.MoveRight.performed += HandleMoveRight;
            inputActions.Tertromino.MoveRight.started += HandleMoveRightState;
            inputActions.Tertromino.MoveRight.canceled += HandleMoveRightState;
    
            inputActions.Tertromino.MoveDown.performed += HandleSoftDrop;
            inputActions.Tertromino.MoveDown.started += HandleSoftDropState;
            inputActions.Tertromino.MoveDown.canceled += HandleSoftDropState;
    
            inputActions.Tertromino.RotateClockwise.performed += HandleRotateRight;
            inputActions.Tertromino.RotateCounterClockwise.performed += HandleRotateLeft;
            
            inputActions.Tertromino.Drop.performed += HandleHardDrop;
            inputActions.Tertromino.Hold.performed += HandleHold;
        }
    
        private void OnDisable()
        {
            inputActions?.Disable();
            
            inputActions.Tertromino.MoveLeft.performed -= HandleMoveLeft;
            inputActions.Tertromino.MoveLeft.started -= HandleMoveLeftState;
            inputActions.Tertromino.MoveLeft.canceled -= HandleMoveLeftState;
    
            inputActions.Tertromino.MoveRight.performed -= HandleMoveRight;
            inputActions.Tertromino.MoveRight.started -= HandleMoveRightState;
            inputActions.Tertromino.MoveRight.canceled -= HandleMoveRightState;
    
            inputActions.Tertromino.MoveDown.performed -= HandleSoftDrop;
            inputActions.Tertromino.MoveDown.started -= HandleSoftDropState;
            inputActions.Tertromino.MoveDown.canceled -= HandleSoftDropState;
    
            inputActions.Tertromino.RotateClockwise.performed -= HandleRotateRight;
            inputActions.Tertromino.RotateCounterClockwise.performed -= HandleRotateLeft;
            
            inputActions.Tertromino.Drop.performed -= HandleHardDrop;
            inputActions.Tertromino.Hold.performed -= HandleHold;
        }
    
        // State Tracking (For continuous movement)
        private void HandleMoveLeftState(InputAction.CallbackContext context) => isMoveLeftHeld = context.ReadValueAsButton();
        private void HandleMoveRightState(InputAction.CallbackContext context) => isMoveRightHeld = context.ReadValueAsButton();
        private void HandleSoftDropState(InputAction.CallbackContext context) => isSoftDropHeld = context.ReadValueAsButton();
    
        // Discrete Actions (For the initial button tap)
        private void HandleMoveLeft(InputAction.CallbackContext context) => OnMoveLeft?.Invoke();
        private void HandleMoveRight(InputAction.CallbackContext context) => OnMoveRight?.Invoke();
        private void HandleSoftDrop(InputAction.CallbackContext context) => OnSoftDrop?.Invoke();
        private void HandleRotateRight(InputAction.CallbackContext context) => OnRotateRight?.Invoke();
        private void HandleRotateLeft(InputAction.CallbackContext context) => OnRotateLeft?.Invoke();
        private void HandleHardDrop(InputAction.CallbackContext context) => OnHardDrop?.Invoke();
        private void HandleHold(InputAction.CallbackContext context) => OnHold?.Invoke();
    
        public bool IsSoftDropHeld() => isSoftDropHeld;
        public bool IsMoveLeftHeld() => isMoveLeftHeld;
        public bool IsMoveRightHeld() => isMoveRightHeld;
    }
    
}

