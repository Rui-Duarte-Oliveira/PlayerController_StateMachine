using UnityEngine;

namespace PlayerCore
{
  public class Supp_ThirdPersonAnimation : SupplementaryComponent
  {
    private Core_ThirdPersonMovement _coreComponent;

    public override void Setup(CoreComponent parentCoreComponent)
    {
      _coreComponent = (Core_ThirdPersonMovement)parentCoreComponent;
    }

    public override void OnActiveUpdate()
    {
      if (_coreComponent.TargetThirdPersonCharacter.RigidBody.velocity.magnitude > 0.2f)
        _coreComponent.TargetThirdPersonCharacter.Animator.SetBool("IsMoving", true);
      else
        _coreComponent.TargetThirdPersonCharacter.Animator.SetBool("IsMoving", false);
    }
  }
}