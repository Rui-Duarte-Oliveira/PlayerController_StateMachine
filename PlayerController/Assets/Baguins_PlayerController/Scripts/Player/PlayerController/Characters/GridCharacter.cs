using UnityEngine;
using GridCore;

namespace PlayerCore
{
  public class GridCharacter : CharacterBase
  {
    [SerializeField] private int _movementRange = 50;

    public Vector2Int CurrentGridPosition { get => _currentGridPosition; set => _currentGridPosition = value; }
    public GridBehaviour CurrentGrid { get => _currentGrid; set => _currentGrid = value; }
    public int MovementRange { get => _movementRange; set => _movementRange = value; }


    private GridBehaviour _currentGrid;
    private Vector2Int _currentGridPosition;
  }
}
