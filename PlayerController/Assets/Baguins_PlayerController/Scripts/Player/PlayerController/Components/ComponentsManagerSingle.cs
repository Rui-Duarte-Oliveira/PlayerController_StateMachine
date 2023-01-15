using UnityEngine;

namespace PlayerCore
{
  public class ComponentsManagerSingle : ComponentsManager
  {
    [SerializeField] private CoreComponent _currentActiveComponent;
    private int _currentActivationValue;

    public virtual void ChangeCurrentActiveCoreComponentObject(GameObject coreComponentObject) => ChangeCurrentActiveCoreComponent(coreComponentObject.GetComponent<CoreComponent>());

    public void ChangeCurrentActiveCoreComponent(CoreComponent coreComponent)
    {
      _currentActiveComponent.IsActive = false;
      _currentActiveComponent = coreComponent;
      _currentActiveComponent.IsActive = true;
    }

    public override void OnUpdateActionPerform()
    {
      if (!_currentActiveComponent.IsPerforming)
        return;

      _currentActiveComponent.OnUpdateActionPerform();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnUpdateActionPerform();
    }

    public override void OnActiveUpdate()
    {
      for (int i = 0; i < _objectCoreComponents.Count; i++)
      {
        _currentActivationValue = _objectCoreComponents[i].CanBeSetActive();

        if (_currentActivationValue > 0 && !_character.IsPerformingAction)
          if (_currentActivationValue == 2)
          {
            ChangeCurrentActiveCoreComponent(_objectCoreComponents[i]);
            break;
          }
          else
          {
            if (_currentActiveComponent.CanBeSetInactive() == 1)
            {
              ChangeCurrentActiveCoreComponent(_objectCoreComponents[i]);
              break;
            }
          }
      }

      _currentActiveComponent.OnActiveUpdate();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnActiveUpdate();
    }

    public override void OnFixedUpdateActionPerform()
    {
      if (!_currentActiveComponent.IsPerforming)
        return;

      _currentActiveComponent.OnFixedUpdateActionPerform();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnFixedUpdateActionPerform();
    }

    public override void OnFixedUpdate()
    {
      _currentActiveComponent.OnFixedUpdate();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnFixedUpdate();
    }

    public override void OnActivation()
    {
      _currentActiveComponent.OnActivation();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnActivation();
    }

    public override void OnDeactivation()
    {
      _currentActiveComponent.OnDeactivation();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnDeactivation();
    }

    public override void OnTargetSwitch()
    {
      _currentActiveComponent.OnTargetSwitch();

      for (int i = 0; i < _supplementaryComponents.Count; i++)
        _supplementaryComponents[i].OnTargetSwitch();
    }
  }
}
