using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Look Sensitivity")]
    public float LookSensitivity = 2f;
    [Header("Walk State")]
    public float WalkSpeed = 5f;
    [Header("Run State")]
    public float RunSpeed = 10f;
    [Header("Jump State")]
    public float JumpVelocity = 5f;
    public float MaxJumpHeight = 1f;
    public float MaxJumpTime = 0.5f;
    public int AmountOfJumps = 1;
    [Header("Crouch")]
    public float CrouchSpeed = 5f;
    public float CrouchHeight = 1f;
    public Vector3 CrouchCenter = /*Vector3.up * 0.5f*/new Vector3(0,0.5f,0);
    public readonly float CrouchTransitionDuration = 0.15f;
    [Header("Gravity")]
    public float Gravity = -9.8f;
    [Header("Check Variables")]
    [Header("Camera")]
    public float MinPitch = -80f;
    public float MaxPitch = 80f;

}
