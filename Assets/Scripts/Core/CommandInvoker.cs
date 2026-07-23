using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class CommandInvoker : MonoBehaviour
    {
        private Stack<ICommand> commandHistory = new Stack<ICommand>();
        public Board board;
    
        public float moveDelay = 0.1f;
        public float softDropDelay = 0.1f;
        private float moveTimer;
        private float softDropTimer;
    
        private void Awake()
        {
            if (board == null)
            {
                board = GetComponent<Board>();
            }
        }
    
        private void OnEnable()
        {
            if (GameInputManager.Instance != null)
            {
                GameInputManager.Instance.OnMoveLeft += OnMoveLeft;
                GameInputManager.Instance.OnMoveRight += OnMoveRight;
                GameInputManager.Instance.OnRotateLeft += OnRotateLeft;
                GameInputManager.Instance.OnRotateRight += OnRotateRight;
                GameInputManager.Instance.OnHardDrop += OnHardDrop;
                GameInputManager.Instance.OnSoftDrop += OnSoftDrop;
                GameInputManager.Instance.OnHold += OnHold;
            }
        }
    
        private void OnDisable()
        {
            if (GameInputManager.Instance != null)
            {
                GameInputManager.Instance.OnMoveLeft -= OnMoveLeft;
                GameInputManager.Instance.OnMoveRight -= OnMoveRight;
                GameInputManager.Instance.OnRotateLeft -= OnRotateLeft;
                GameInputManager.Instance.OnRotateRight -= OnRotateRight;
                GameInputManager.Instance.OnHardDrop -= OnHardDrop;
                GameInputManager.Instance.OnSoftDrop -= OnSoftDrop;
                GameInputManager.Instance.OnHold -= OnHold;
            }
        }
    
        private void OnMoveLeft() 
        {
            ExecuteCommand(new MoveLeftCommand(board.activePiece)); moveTimer = Time.time + moveDelay;
        }
        private void OnMoveRight() 
        { 
            ExecuteCommand(new MoveRightCommand(board.activePiece)); moveTimer = Time.time + moveDelay; 
        }
        private void OnRotateLeft() => ExecuteCommand(new RotateLeftCommand(board.activePiece));
        private void OnRotateRight() => ExecuteCommand(new RotateRightCommand(board.activePiece));
        private void OnHardDrop() => ExecuteCommand(new HardDropCommand(board.activePiece));
        private void OnSoftDrop() 
        { 
            ExecuteCommand(new SoftDropCommand(board.activePiece)); softDropTimer = Time.time + softDropDelay; 
        }
        private void OnHold() => ExecuteCommand(new HoldCommand(board));
    
        private void Update()
        {
            if (board == null || board.activePiece == null) return;
    
            if (GameInputManager.Instance != null)
            {
                if (GameInputManager.Instance.IsSoftDropHeld() && Time.time >= softDropTimer)
                {
                    ExecuteCommand(new SoftDropCommand(board.activePiece));
                    softDropTimer = Time.time + softDropDelay;
                }
    
                if (GameInputManager.Instance.IsMoveLeftHeld() && Time.time >= moveTimer)
                {
                    ExecuteCommand(new MoveLeftCommand(board.activePiece));
                    moveTimer = Time.time + moveDelay;
                }
                else if (GameInputManager.Instance.IsMoveRightHeld() && Time.time >= moveTimer)
                {
                    ExecuteCommand(new MoveRightCommand(board.activePiece));
                    moveTimer = Time.time + moveDelay;
                }
            }
        }
    
        public void ExecuteCommand(ICommand command)
        {
            if (GameStateManager.Instance != null)
            {
                IGameState currentState = GameStateManager.Instance.CurrentState;
                if (!(currentState is PlayingState) && !(currentState is GarbageState))
                {
                    return; // Do not execute commands if not playing
                }
            }
    
            try
            {
                command.Execute();
                commandHistory.Push(command);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Command failed to execute: " + ex.Message);
            }
        }
    }
    
}
