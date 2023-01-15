using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerCore
{
  public class ComponentsManager : MonoBehaviour
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
          OnActivation();
        else
          OnDeactivation();
      }
    }

    public CharacterBase TargetCharacter { get => _character; }

    [SerializeField] protected bool _isActive;

    protected List<SupplementaryComponent> _supplementaryComponents;
    protected List<CoreComponent> _objectCoreComponents;
    protected List<CoreComponent> _objectCoreComponentsToActivate;
    protected CharacterBase _character;

    /// <summary>
    /// Is used by the State Change functions to determine if the current component manager can be activated.
    /// Returns false (0) by default if only base is called.
    /// Return true (1).
    /// Return priority true (2).
    /// </summary>
    /// <returns></returns>
    public virtual int CanBeSetActive() { return 0; }

    /// <summary>
    /// Is used by the State Change functions to determine if the current component manager can be deactivated.
    /// Returns false (0) by default if only base is called.
    /// Return true (1).
    /// Return priority true (2).
    /// </summary>
    /// <returns></returns>
    public virtual int CanBeSetInactive() { return 0; }

    /// <summary>
    /// Always call base!
    /// When a Core Component executes an action, this function will be called on Update.
    /// </summary>
    public virtual void OnUpdateActionPerform() { }

    /// <summary>
    /// Always call base!
    /// When a Core Component executes an action, this function will be called on FixedUpdate.
    /// </summary>
    public virtual void OnFixedUpdateActionPerform() { }

    /// <summary>
    /// Always call base!
    /// Is called on Update if the manager is active.
    /// Base function will run through all the designated core components and call their respective ActiveUpdate and InactiveUpdate functions.
    /// </summary>
    public virtual void OnActiveUpdate() { }

    /// <summary>
    /// Always call base!
    /// Is called on FixedUpdate if the manager is active.
    /// Base function will run through all the designated core components and call their respective FixedUpdate functions. 
    /// </summary>
    public virtual void OnFixedUpdate() { }

    /// <summary>
    /// Always call base!
    /// Is called when the manager is activated.
    /// Base function will run through all the designated core components and call their respective OnActivation functions.
    /// </summary>
    public virtual void OnActivation() { }

    /// <summary>
    /// Always call base!
    /// Is called when the manager is deactivated.
    /// Base function will run through all the designated core components and call their respective OnDeactivation functions.
    /// </summary>
    public virtual void OnDeactivation() { }

    /// <summary>
    /// Always call base!
    /// Is called when target character is switched and if the manager is active.
    /// Base function will run through all the designated core components and call their respective OnTargetSwitch functions. 
    /// </summary>
    public virtual void OnTargetSwitch() { }

    /// <summary>
    /// Always call base!
    /// Is called on Start when the managers before the Initialize function.
    /// </summary>
    public virtual void Setup()
    {
      _objectCoreComponents = GetComponentsInChildren<CoreComponent>().ToList();
      _supplementaryComponents = GetComponents<SupplementaryComponent>().ToList();

      for (int i = 0; i < _objectCoreComponents.Count; i++)
        _objectCoreComponents[i].Setup();
    }

    /// <summary>
    /// Always call base!
    /// Base function will run through all the designated core components and call their respective SetTargetCharacter functions. 
    /// </summary>
    /// <param name="targetCharacter"></param>
    public virtual void Initialize(CharacterBase targetCharacter)
    {
      _character = targetCharacter;

      for (int i = 0; i < _objectCoreComponents.Count; i++)
        _objectCoreComponents[i].OnInitialization(targetCharacter);
    }
  }
}
