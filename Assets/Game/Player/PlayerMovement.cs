using UnityEngine;
using System;

/// <summary>
/// Handles all character movement physics and camera rotation.
/// Designed for easy extension and integration into any FPS project.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    #region Events
    /// <summary> Fired when crouch state changes </summary>
    public event Action<bool> OnCrouchChanged;
    
    /// <summary> Fired when jump occurs </summary>
    public event Action OnJumped;
    
    /// <summary> Fired when grounded state changes </summary>
    public event Action<bool> OnGroundedChanged;
    #endregion

    #region Properties
    public CharacterController CharacterController { get; private set; }
    
    [Header("Camera Reference")]
    [Tooltip("Reference to the camera transform for look rotation")]
    [SerializeField] private Transform _cameraTransform;
    public Transform CameraTransform => _cameraTransform;
    
    private float _cameraVerticalRotation = 0f;
    
    [Header("Movement")]
    private Vector3 _currentVelocity;
    private float _verticalVelocity = 0f;
    public float CurrentSpeed { get; set; }
    public float SpeedModifier = 1f;
    
    [Header("Crouch")]
    private float _standingHeight;
    private Vector3 _standingCenter;
    private bool _isCrouching = false;
    public bool IsCrouching => _isCrouching;
    
    [Header("Ground Detection")]
    private bool _wasGrounded = false;
    #endregion

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        
        if (_cameraTransform == null)
        {
            Debug.LogError("[PlayerMovement] Camera transform is not assigned!");
        }
        
        if (CharacterController == null)
        {
            Debug.LogError("[PlayerMovement] CharacterController component is missing!");
        }
    }

    #region Crouch System
    
    /// <summary>
    /// Checks if there's enough space above to stand up.
    /// Uses capsule cast to detect obstacles in standing height.
    /// </summary>
    /// <returns>True if player can stand up without collision</returns>
    public bool CanStandUp()
    {
        Vector3 start = transform.position + Vector3.up * CharacterController.radius;
        float castDistance = _standingHeight - CharacterController.radius * 2f;
        
        if (castDistance <= 0f) return true;
        
        bool hasObstacle = Physics.CheckCapsule(
            start,
            start + Vector3.up * castDistance,
            CharacterController.radius,
            QueryTriggerInteraction.Ignore
        );
        
        return !hasObstacle;
    }
    
    /// <summary>
    /// Transitions player to crouch state with smooth height reduction
    /// </summary>
    public void Crouch(PlayerData playerData)
    {
        if (_isCrouching) return;
        
        _standingHeight = CharacterController.height;
        _standingCenter = CharacterController.center;
        
        CharacterController.height = playerData.CrouchHeight;
        CharacterController.center = playerData.CrouchCenter;
        
        _isCrouching = true;
        OnCrouchChanged?.Invoke(true);
    }
    
    /// <summary>
    /// Returns player to standing state
    /// </summary>
    public void Uncrouch()
    {
        if (!_isCrouching) return;
        
        CharacterController.height = _standingHeight;
        CharacterController.center = _standingCenter;
        
        _isCrouching = false;
        OnCrouchChanged?.Invoke(false);
    }
    
    #endregion

    #region Movement System
    
    /// <summary>
    /// Moves the character based on input direction relative to camera
    /// </summary>
    public void Move(Vector2 moveInput, Transform cameraTransform)
    {
        if (moveInput == Vector2.zero)
        {
            _currentVelocity = new Vector3(0f, _verticalVelocity, 0f);
            return;
        }
        
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f; 
        camRight.Normalize();

        float playerMoveSpeed = CurrentSpeed * SpeedModifier;
        Vector3 desiredMoveDirection = camForward * playerMoveSpeed * moveInput.y
                                     + camRight * playerMoveSpeed * moveInput.x;

        _currentVelocity = new Vector3(desiredMoveDirection.x, _verticalVelocity, desiredMoveDirection.z);
        CharacterController.Move(_currentVelocity * Time.deltaTime);
    }
    
    /// <summary>
    /// Sets vertical velocity for jumping
    /// </summary>
    public void SetJumpVelocity(float velocityY)
    {
        _verticalVelocity = velocityY;
        OnJumped?.Invoke();
    }
    
    #endregion

    #region Gravity System
    
    /// <summary>
    /// Applies gravity while in air (uses deltaTime for consistent physics)
    /// </summary>
    public void ApplyAirGravity(float gravity)
    {
        _verticalVelocity += gravity * Time.deltaTime;
    }
    
    /// <summary>
    /// Applies minimal gravity when grounded to keep player on surface
    /// Already includes proper deltaTime handling
    /// </summary>
    public void ApplyGroundGravity(float gravity)
    {
        _verticalVelocity = gravity * Time.deltaTime;
    }
    
    /// <summary>
    /// Checks if character is currently grounded
    /// Triggers event when grounded state changes
    /// </summary>
    public bool CheckIfGrounded()
    {
        bool isGrounded = CharacterController.isGrounded;
        
        if (isGrounded != _wasGrounded)
        {
            _wasGrounded = isGrounded;
            OnGroundedChanged?.Invoke(isGrounded);
        }
        
        return isGrounded;
    }
    
    #endregion

    #region Camera System
    
    /// <summary>
    /// Handles camera rotation based on mouse input
    /// Separated from movement for better modularity
    /// </summary>
    public void HandleCameraRotation(Vector2 lookInput, PlayerData playerData)
    {
        if (_cameraTransform == null) return;
        
        float horizontalInput = lookInput.x * playerData.LookSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, horizontalInput);
        
        _cameraVerticalRotation -= lookInput.y * playerData.LookSensitivity * Time.deltaTime;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, playerData.MinPitch, playerData.MaxPitch);
        
        _cameraTransform.localRotation = Quaternion.Euler(_cameraVerticalRotation, 0f, 0f);
    }
    
    #endregion
}
