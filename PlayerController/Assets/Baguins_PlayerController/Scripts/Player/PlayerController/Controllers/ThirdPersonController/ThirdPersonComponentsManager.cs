using UnityEngine;

namespace PlayerCore
{
  public class ThirdPersonComponentsManager : ComponentsManagerMultiple
  {
    public override void OnActivation()
    {
      base.OnActivation();

      _objectCoreComponents[0].IsActive = true;
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }

    public override int CanBeSetActive()
    {
      return 1;
    }

    public override int CanBeSetInactive()
    {
      return 1;
    }
  }
}
