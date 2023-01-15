using GridCore;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCore
{
  public class GridComponentsManager : ComponentsManagerSingle
  {

    [SerializeField] private List<GridBehaviour> _grids;

    private PlayerGridCharacter _targetCharacter;
    private Vector2Int _gridPosition;

    public override void Initialize(CharacterBase targetCharacter)
    {
      base.Initialize(targetCharacter);
      _targetCharacter = (PlayerGridCharacter)targetCharacter;
    }

    public override void OnActivation()
    {
      base.OnActivation();

      CursorStateChange(true);
      _targetCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void OnDeactivation()
    {
      base.OnDeactivation();

      CursorStateChange(false);
    }

    public override void OnTargetSwitch()
    {
      base.OnTargetSwitch();
      IsInsideGridBoundaries();
    }

    public override int CanBeSetActive()
    {
      return IsInsideGridBoundaries();
    }

    public override int CanBeSetInactive()
    {
      return 1;
    }


    private int IsInsideGridBoundaries()
    {
      for (int i = 0; i < _grids.Count; i++)
      {
        _gridPosition = _grids[i].GetGridPosition(_targetCharacter.transform.position);

        if (_grids[i].IsInsideGridBoundry(_gridPosition.x, _gridPosition.y))
        {
          _targetCharacter.CurrentGrid = _grids[i];
          _targetCharacter.CurrentGridPosition = _gridPosition;
          return 1;
        }
      }

      return 0;
    }

    private void CursorStateChange(bool isActive)
    {
      Cursor.visible = isActive;

      if (isActive)
        Cursor.lockState = CursorLockMode.None;
      else
        Cursor.lockState = CursorLockMode.Locked;
    }
  }
}
