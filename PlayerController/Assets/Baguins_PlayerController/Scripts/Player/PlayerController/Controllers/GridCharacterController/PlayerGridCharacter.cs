using UnityEngine;
using Cinemachine;

namespace PlayerCore
{
  public class PlayerGridCharacter : GridCharacter
  {
    public CinemachineFreeLook FreeLookCam { get => _freeLookCam; }
    public Rigidbody RigidBody { get => _rigidBody; }

    [SerializeField] private CinemachineFreeLook _freeLookCam;

    private Rigidbody _rigidBody;

    private void Awake()
    {
      _rigidBody = GetComponent<Rigidbody>();
    }
  }
}
