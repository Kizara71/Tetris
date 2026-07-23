using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private Piece piece;
    public MoveLeftCommand(Piece piece) { this.piece = piece; }
    public void Execute() { piece.HandleMoveLeft(); }
}

public class MoveRightCommand : ICommand
{
    private Piece piece;
    public MoveRightCommand(Piece piece) { this.piece = piece; }
    public void Execute() { piece.HandleMoveRight(); }
}

public class RotateLeftCommand : ICommand
{
    private Piece piece;
    public RotateLeftCommand(Piece piece) { this.piece = piece; }
    public void Execute() { piece.HandleRotateLeft(); }
}

public class RotateRightCommand : ICommand
{
    private Piece piece;
    public RotateRightCommand(Piece piece) { this.piece = piece; }
    public void Execute() { piece.HandleRotateRight(); }
}

public class HardDropCommand : ICommand
{
    private Piece piece;
    public HardDropCommand(Piece piece) { this.piece = piece; }
    public void Execute() { piece.HandleHardDrop(); }
}

public class SoftDropCommand : ICommand
{
    private Piece piece;
    public SoftDropCommand(Piece piece) { this.piece = piece; }
    public void Execute() { piece.HandleSoftDrop(); }
}

public class HoldCommand : ICommand
{
    private Board board;
    public HoldCommand(Board board) { this.board = board; }
    public void Execute() { board.HoldPiece(); }
}
