using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace PlayerCore
{
  public class CoreComponent : ComponentBase
  {
    /// <summary>
    /// If set true, will call OnActivation function.
    /// If set false, will call OnDeactivation function.
    /// </summary>
    public bool IsActive
    {
      get => _isActive;
      set
      {
        _isActive = value;

        if (_isActive)
        {
          OnActivation();
          return;
        }

        OnDeactivation();
      }
    }

    /// <summary>
    /// If set true, will call ExecuteAction function.
    /// If set false, will call OnActionPerformed function.
    /// </summary>
    public bool IsPerforming
    {
      get => _isPerforming;
      set
      {
        if (value)
          ExecuteAction();
        else
          OnActionPerformed();
      }
    }

    [SerializeField] private bool _isActive;
    [SerializeField] private bool _isPerforming;

    private List<SupplementaryComponent> _supplementaryComponents;
    private CharacterBase _targetCharacter;

    /// <summary>
    /// Always call base!
    /// Function is called OnStart and sets up dependancies
    /// </summary>
    public virtual void Setup()
    {
      _supplementaryComponents = GetComponents<SupplementaryComponent>().ToList();

      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].Setup(this);
    }

    /// <summary>
    /// Always call base!
    /// Function is called everytime the target character changes.
    /// </summary>
    public virtual void OnInitialization(CharacterBase targetCharacter)
    {
      _targetCharacter = targetCharacter;

      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnInitialization();
    }

    public override void OnFixedUpdate()
    {
      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnFixedUpdate();
    }

    public override void OnActiveUpdate()
    {
      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnActiveUpdate();
    }

    public override void OnActivation()
    {
      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnActivation();
    }

    public override void OnDeactivation()
    {
      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnDeactivation();
    }

    public override void ExecuteAction()
    {
      if (IsPerforming)
        return;

      _targetCharacter.IsPerformingAction = true;
      _isPerforming = true;

      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].ExecuteAction();
    }

    public override void OnUpdateActionPerform()
    {
      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnUpdateActionPerform();
    }

    public override void OnActionPerformed()
    {
      _targetCharacter.IsPerformingAction = false;
      _isPerforming = false;

      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnActionPerformed();
    }

    public override void OnTargetSwitch()
    {
      if (_supplementaryComponents != null)
        for (int i = 0; i < _supplementaryComponents.Count; i++)
          _supplementaryComponents[i].OnTargetSwitch();

      IsPerforming = false;
    }

    /// <summary>
    /// Is called on Update if the component is not Active.
    /// Returns false (0) by default if only base is called.
    /// Return true (1).
    /// Return priority true (2).
    /// </summary>
    /// <returns></returns>
    public virtual int CanBeSetActive(){ return 0; }

    /// <summary>
    /// Is called Update if the component is Active.
    /// Returns false (0) by default if only base is called.
    /// Return true (1).
    /// Return priority true (2).
    /// </summary>
    /// <returns></returns>
    public virtual int CanBeSetInactive(){ return 0; }
  }
}
