using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController CharacterController { get; private set; }
    //---------------------Camera----------------------------------
    [SerializeField] private Transform _cameraTransform;

    private float _cameraVerticalRotation = 0;
    //----------------------Movement-------------------------------
    private Vector3 _currentVelocity;
    private float _verticalVelocity = 0f;
    public float CurrentSpeed { get; set; }
    public float SpeedModifier = 1f;
    //-------------------------------------------------------------

    //-------------------Crouch------------------------
    private float _standingHeight;
    private Vector3 _standingCenter;
    private RaycastHit[] _hitResults;
    //---------------------------------------------------
    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Debug.Assert(_cameraTransform != null, "Camera reference is missing!");
        Debug.Assert(CharacterController != null, "CharacterController is missing!");      
    }
    private void Start()
    {
        _hitResults = new RaycastHit[10];
    }

    public float Timer()
    {
        return Time.deltaTime;
    }
    //---------------Crouch------------------------
    public bool CanStandUp() // баг при приседании - застревает в текстурах, чекни!
    {    
        int hitcount = Physics.CapsuleCastNonAlloc(
            transform.position + new Vector3(0, CharacterController.height, 0) * 2f,
            transform.position + new Vector3(0, CharacterController.height, 0) * 3f,
            CharacterController.radius,
            transform.up,
            _hitResults,
            CharacterController.height
        );
        return hitcount > 0 ? false : true;
    }
    public void Crouch(PlayerData playerData)
    {
        _standingHeight = CharacterController.height;
        _standingCenter = CharacterController.center;
        CharacterController.height = playerData.CrouchHeight;
        CharacterController.center = new Vector3 (0, 0.5f, 0);
        _cameraTransform.localPosition = new Vector3(0, _cameraTransform.localPosition.y * 0.5f, 0);// costyl
    }
    public void Uncrouch()
    {
        CharacterController.height = _standingHeight;
        CharacterController.center = _standingCenter;
        _cameraTransform.localPosition = new Vector3(0, _cameraTransform.localPosition.y * 2f, 0);// costyl
    }
    //-----------------Movement----------------------------
    public void Move(PlayerInputHandler inputHandler)
    {
        Vector3 camForward = _cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = _cameraTransform.right;
        camRight.y = 0f; 
        camRight.Normalize();

        float playerMoveSpeed = CurrentSpeed * SpeedModifier;
        Vector3 desiredMoveDirection = camForward * playerMoveSpeed * inputHandler.MoveInput.y
                                     + camRight * playerMoveSpeed * inputHandler.MoveInput.x;

        _currentVelocity = new Vector3(desiredMoveDirection.x, _verticalVelocity, desiredMoveDirection.z) * Time.deltaTime;
        CharacterController.Move(_currentVelocity);
    }
    public void SetJumpVelocity(float velocityY)
    {
        _verticalVelocity = velocityY;
    }
    //----------Gravity-----------------------
    public void SetInAirGravity(float velocityY)
    {
        _verticalVelocity += velocityY * Time.deltaTime;
    }
    public void SetInGroundGravity(float velocityY)
    {
        _verticalVelocity += velocityY;// does it need Time.deltaTime? * Time.deltaTime
    }
    public bool CheckIfGrounded()
    {
        return CharacterController.isGrounded;
    }
    //-----------Camera-----------------------
    public void HandleCameraRotation(PlayerInputHandler inputHandler, PlayerData playerData)   
    {
        Vector2 lookInput = inputHandler.LookInput;
        float horizontalInput = lookInput.x * playerData.LookSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, horizontalInput);
        _cameraVerticalRotation -= lookInput.y * playerData.LookSensitivity * Time.deltaTime;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, playerData.MinPitch, playerData.MaxPitch);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraVerticalRotation, 0f, 0f);
    }
}
