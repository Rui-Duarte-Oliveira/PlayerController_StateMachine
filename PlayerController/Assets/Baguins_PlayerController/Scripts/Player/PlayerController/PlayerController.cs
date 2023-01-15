using System.Collections.Generic;
using UnityEngine;

namespace PlayerCore
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] private List<CharacterManager> _currentTargetCharacterList;

    public ComponentsManager CurrentActiveManager { get => _currentActiveManager; set => _currentActiveManager = value; }
    public CharacterManager CurrentTargetCharacter { get => _currentTargetCharacter; set => _currentTargetCharacter = value; }
    public List<ComponentsManager> ComponentsManagers { get => _componentsManagers; set => _componentsManagers = value; }
    public List<CharacterManager> CurrentTargetCharacterList { get => _currentTargetCharacterList; set => _currentTargetCharacterList = value; }


    private List<ComponentsManager> _componentsManagers = new List<ComponentsManager>();
    private ComponentsManager _currentActiveManager;
    private CharacterManager _currentTargetCharacter;

    private void Awake()
    {
      _componentsManagers.AddRange(GetComponentsInChildren<ComponentsManager>());
      _currentActiveManager = GetActiveComponentsManager();
      _currentTargetCharacter = _currentTargetCharacterList[0];
      _currentTargetCharacter.CurrentComponentManager = _currentActiveManager;
    }

    private void Start()
    {
      OnStartFunctions();
    }

    private void Update()
    {
      OnUpdateFunctions();
    }

    private void FixedUpdate()
    {
      OnFixedUpdateFunctions();
    }

    private void OnUpdateFunctions()
    {
      _currentActiveManager.OnUpdateActionPerform();
      _currentActiveManager.OnActiveUpdate();
    }

    private void OnFixedUpdateFunctions()
    {
      _currentActiveManager.OnFixedUpdateActionPerform();
      _currentActiveManager.OnFixedUpdate();
    }

    /// <summary>
    /// Initiliazes the component managers with a target character.
    /// </summary>
    private void OnStartFunctions()
    {
      if (_componentsManagers == null)
      {
        Debug.LogWarning("No Managers Assigned at: " + transform.name);
        return;
      }

      for (int i = 0; i < _componentsManagers.Count; i++)
      {
        _componentsManagers[i].Setup();
        _componentsManagers[i].Initialize(_currentTargetCharacter.CurrentTargetCharacterTypes[i]);
      }

      for (int i = 0; i < _currentTargetCharacterList.Count; i++)
        _currentTargetCharacterList[i].CurrentComponentManager = _currentActiveManager;

      _currentActiveManager.OnActivation();
    }

    /// <summary>
    /// Returns the first active component manager in the ComponentsManagers List
    /// </summary>
    /// <returns></returns>
    private ComponentsManager GetActiveComponentsManager()
    {
      foreach (ComponentsManager componentManager in _componentsManagers)
        if (componentManager.IsActive)
          return componentManager;

      Debug.LogError("No Active manager! " + transform.name);
      return null;
    }
  }
}
