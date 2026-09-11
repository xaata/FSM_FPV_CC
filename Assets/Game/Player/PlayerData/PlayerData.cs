using UnityEngine;

/// <summary>
/// ScriptableObject containing all configurable player parameters.
/// Create instances via Assets > Create > Data > Player Data > Base Data
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Look Sensitivity")]
    [Tooltip("Mouse look sensitivity multiplier")]
    public float LookSensitivity = 2f;
    
    [Header("Walk State")]
    [Tooltip("Normal walking speed in units per second")]
    public float WalkSpeed = 5f;
    
    [Header("Run State")]
    [Tooltip("Running speed when holding sprint button")]
    public float RunSpeed = 10f;
    
    [Header("Jump State")]
    [Tooltip("Initial jump velocity")]
    public float JumpVelocity = 5f;
    
    [Tooltip("Maximum height achievable by jumping")]
    public float MaxJumpHeight = 1f;
    
    [Tooltip("Time to reach apex of jump")]
    public float MaxJumpTime = 0.5f;
    
    [Tooltip("Number of jumps allowed before touching ground")]
    [Range(1, 5)]
    public int AmountOfJumps = 1;
    
    [Header("Crouch")]
    [Tooltip("Movement speed while crouching")]
    public float CrouchSpeed = 3f;
    
    [Tooltip("Character controller height when crouched")]
    public float CrouchHeight = 1f;
    
    [Tooltip("Character controller center offset when crouched")]
    public Vector3 CrouchCenter = new Vector3(0f, 0.5f, 0f);
    
    [Tooltip("Duration for crouch transition (reserved for future smoothing)")]
    public readonly float CrouchTransitionDuration = 0.15f;
    
    [Header("Gravity")]
    [Tooltip("Gravity force applied per second (negative value)")]
    public float Gravity = -9.81f;
    
    [Header("Camera")]
    [Tooltip("Minimum vertical camera angle (looking down)")]
    public float MinPitch = -80f;
    
    [Tooltip("Maximum vertical camera angle (looking up)")]
    public float MaxPitch = 80f;
}
