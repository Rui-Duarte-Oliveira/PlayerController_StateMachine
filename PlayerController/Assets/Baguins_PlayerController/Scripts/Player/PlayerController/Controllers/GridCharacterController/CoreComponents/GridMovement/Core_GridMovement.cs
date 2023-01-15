using UnityEngine;
using System.Collections.Generic;
using GridCore;

namespace PlayerCore
{
  public class Core_GridMovement : CoreComponent
  {
    [Space(20)]
    [SerializeField] private LayerMask _rayCastMask;
    [SerializeField] private float _movementModifier;

    public PlayerGridCharacter TargetGridCharacter { get => _targetGridCharacter; }

    private PlayerGridCharacter _targetGridCharacter;

    private PathFinding _pathFinding = new();
    private List<Node> _pathNodes = new();
    private List<Vector3> _pathWorldPositions = new();

    public override void OnInitialization(CharacterBase targetCharacter)
    {
      base.OnInitialization(targetCharacter);
      _targetGridCharacter = (PlayerGridCharacter)targetCharacter;
    }

    public override void OnActivation()
    {
      base.OnActivation();
      _targetGridCharacter.CurrentGrid.SetGridCharacterPosition(_targetGridCharacter);
    }

    public override void OnActiveUpdate()
    {
      base.OnActiveUpdate();

      if (Input.GetMouseButtonDown(0))
        ExecuteAction();
    }

    public override void OnDeactivation()
    {
      base.OnDeactivation();
      _targetGridCharacter.CurrentGrid.RemoveGridCharacter(_targetGridCharacter);
      _pathNodes.Clear();
      _pathWorldPositions.Clear();
    }

    public override void OnUpdateActionPerform()
    {
      base.OnUpdateActionPerform();
      MoveTowardsDestination();
    }

    public override void OnActionPerformed()
    {
      base.OnActionPerformed();
      _targetGridCharacter.CurrentGrid.SetGridCharacterPosition(_targetGridCharacter);
      _pathNodes.Clear();
      IsActive = false;
      IsActive = true;
    }

    /// <summary>
    /// Checks for player Left Mouse Click input.
    /// After player input, shoots a raycast towards the mouse direction and, if conditions are met, 
    /// calculates a path towards the desired destination
    /// </summary>
    public override void ExecuteAction()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, float.MaxValue, _rayCastMask))
      {
        Vector2Int gridPosition = _targetGridCharacter.CurrentGrid.GetGridPosition(hit.point);

        if (_targetGridCharacter.CurrentGrid.Grid[gridPosition.x, gridPosition.y].GridCharacter != null)
          return;

        _pathNodes = _pathFinding.FindPath(_targetGridCharacter, gridPosition.x, gridPosition.y);

        if (_pathNodes != null)
          base.ExecuteAction();
        else
        {
          Debug.Log("No Possible Path " + _targetGridCharacter.name);
          return;
        }
      }
    }

    /// <summary>
    /// Moves target character along the calculated path.
    /// </summary>
    private void MoveTowardsDestination()
    {
      SetPathWorldPositions();

      _targetGridCharacter.transform.position = Vector3.MoveTowards(_targetGridCharacter.transform.position, _pathWorldPositions[0], _movementModifier);

      if (Vector3.Distance(_targetGridCharacter.transform.position, _pathWorldPositions[0]) < 0.1f)
        _pathWorldPositions.RemoveAt(0);

      if (_pathWorldPositions.Count == 0)
        OnActionPerformed();

      ObjectLookDirection(_targetGridCharacter);                                                                        //Temporary
    }

    private void SetPathWorldPositions()
    {
      if (_pathWorldPositions.Count != 0)
        return;

      _targetGridCharacter.CurrentGrid.RemoveGridCharacter(_targetGridCharacter);
      _pathWorldPositions = _targetGridCharacter.CurrentGrid.GetNodesWorldPositions(_pathNodes);
      ObjectLookDirection(_targetGridCharacter);                                                                                //Temporary
    }

    /// <summary>
    /// Temporary.
    /// Sets the target character direction towards the path it is following.
    /// </summary>
    /// <param name="targetCharacter"></param>
    private void ObjectLookDirection(PlayerGridCharacter targetCharacter)
    {
      if (_pathWorldPositions.Count == 0)
        return;

      Vector3 direction = (_pathWorldPositions[0] - targetCharacter.transform.position).normalized;
      direction.y = 0;
      targetCharacter.transform.rotation = Quaternion.LookRotation(direction);
    }
  }
}
