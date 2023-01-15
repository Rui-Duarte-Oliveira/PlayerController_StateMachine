
namespace PlayerCore
{
  public class ComponentsManagerMultiple : ComponentsManager
  {
    private int _currentDeactivationValue;

    public override void OnUpdateActionPerform()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
        if (_objectCoreComponents[i].IsActive && _objectCoreComponents[i].IsPerforming)
          _objectCoreComponents[i].OnUpdateActionPerform();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnUpdateActionPerform();
    }

    public override void OnActiveUpdate()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
      {
        if (_objectCoreComponents[i].IsActive)
        {
          _currentDeactivationValue = _objectCoreComponents[i].CanBeSetInactive();

          if (_currentDeactivationValue > 0)
          {
            if (_currentDeactivationValue == 2)
              _objectCoreComponents[i].IsPerforming = false;

            if (!_objectCoreComponents[i].IsPerforming)
              _objectCoreComponents[i].IsActive = false;
          }
        }
        else
          if (_objectCoreComponents[i].CanBeSetActive() > 0)
          _objectCoreComponents[i].IsActive = true;

        _objectCoreComponents[i].OnActiveUpdate();
      }

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnActiveUpdate();
    }

    public override void OnFixedUpdateActionPerform()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
        if (_objectCoreComponents[i].IsActive && _objectCoreComponents[i].IsPerforming)
          _objectCoreComponents[i].OnFixedUpdateActionPerform();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnFixedUpdateActionPerform();
    }

    public override void OnFixedUpdate()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
        if (_objectCoreComponents[i].IsActive)
          _objectCoreComponents[i].OnFixedUpdate();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnFixedUpdate();
    }

    public override void OnActivation()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
        if (_objectCoreComponents[i].IsActive)
          _objectCoreComponents[i].OnActivation();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnActivation();
    }

    public override void OnDeactivation()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
        if (_objectCoreComponents[i].IsActive)
          _objectCoreComponents[i].IsActive = false;

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnDeactivation();
    }

    public override void OnTargetSwitch()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
        if (_objectCoreComponents[i].IsActive)
          _objectCoreComponents[i].OnTargetSwitch();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnTargetSwitch();
    }
  }
}
