# FPV Character Controller - Documentation

## Overview
A professional, extensible First-Person View character controller built with Unity's State Machine pattern. Designed for easy integration into any FPS or first-person adventure project.

## Features
- **State Machine Architecture**: Clean FSM with super-states (Ground/Air) and sub-states (Idle/Walk/Run/Jump/Crouch)
- **New Input System**: Full support for Unity's Input System package
- **ScriptableObject Configuration**: Easy-to-tune player parameters without code changes
- **Event System**: Subscribe to player events (jump, crouch, grounded changes)
- **Bug-Free Physics**: Proper deltaTime usage, fixed crouch detection, no camera hacks

## Requirements
- Unity 6000.0.60f1 or later
- Unity Input System package
- URP (Universal Render Pipeline) - optional

## Quick Start

### Installation
1. Copy the `Player` folder into your project's `Assets` directory
2. Ensure you have the Input System package installed (Window > Package Manager)
3. Create a new PlayerData asset: Right-click in Project > Create > Data > Player Data > Base Data

### Setup Steps
1. **Create Player GameObject**:
   - Add `CharacterController` component
   - Add `PlayerMovement` component
   - Add `PlayerInputHandler` component
   - Add `PlayerStateMachineInit` component

2. **Setup Camera**:
   - Create a child GameObject named "CameraPivot" at head height
   - Place your Main Camera as a child of CameraPivot
   - Assign CameraPivot transform to `PlayerMovement._cameraTransform` field

3. **Configure Player Data**:
   - Select the `PlayerStateMachineInit` component
   - Assign your PlayerData ScriptableObject to the `_playerData` field

4. **Configure Input**:
   - The controller uses default input actions: Move (WASD), Look (Mouse), Jump (Space), Run (Left Shift), Crouch (Left Ctrl)
   - Customize in InputSystem_Actions.inputactions if needed

## Architecture

### Core Components

#### PlayerMovement
Handles all physics and movement:
- Movement relative to camera direction
- Gravity application (grounded and air)
- Crouch height adjustment
- Camera rotation
- Events: `OnCrouchChanged`, `OnJumped`, `OnGroundedChanged`

#### PlayerInputHandler
Wraps Unity's Input System:
- Provides clean properties: `MoveInput`, `LookInput`, `JumpPressed`, `RunPressed`, `CrouchPressed`
- Events: `OnJumpStarted`, `OnCrouchToggled`

#### PlayerData (ScriptableObject)
Configurable parameters:
- Speeds: WalkSpeed, RunSpeed, CrouchSpeed
- Jump: JumpVelocity, MaxJumpHeight, MaxJumpTime, AmountOfJumps
- Gravity: Gravity force
- Camera: LookSensitivity, MinPitch, MaxPitch
- Crouch: CrouchHeight, CrouchCenter

### State Machine

```
PlayerStateMachineInit (MonoBehaviour)
└── StateMachine
    ├── InGroundState (Super-state)
    │   ├── IdleState
    │   ├── WalkState
    │   ├── RunState
    │   └── CrouchState
    └── InAirState (Super-state)
        └── JumpState
```

#### State Lifecycle
Each state has four methods:
- `Enter()`: Called once when entering the state
- `Exit()`: Called once when leaving the state
- `LogicUpdate()`: Called every frame in Update - handle input and transitions
- `PhysicsUpdate()`: Called every frame in FixedUpdate - handle physics checks

## Extending the Controller

### Adding New States
1. Create a new class inheriting from `PlayerState`, `PlayerInGroundState`, or `PlayerInAirState`
2. Override the lifecycle methods as needed
3. Add the state to `PlayerStateMachineInit`
4. Create transition logic in existing states

### Example: Adding Double Jump
```csharp
public class PlayerDoubleJumpState : PlayerInAirState
{
    private int _jumpsRemaining;
    
    public override void Enter()
    {
        base.Enter();
        _jumpsRemaining = PlayerData.AmountOfJumps - 1;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (PlayerInputHandler.JumpPressed && _jumpsRemaining > 0)
        {
            PerformDoubleJump();
            _jumpsRemaining--;
        }
    }
    
    private void PerformDoubleJump()
    {
        PlayerMovement.SetJumpVelocity(PlayerData.JumpVelocity);
    }
}
```

### Subscribing to Events
```csharp
public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    private void OnEnable()
    {
        playerMovement.OnJumped += PlayJumpSound;
        playerMovement.OnGroundedChanged += OnGroundedChanged;
    }
    
    private void OnDisable()
    {
        playerMovement.OnJumped -= PlayJumpSound;
        playerMovement.OnGroundedChanged -= OnGroundedChanged;
    }
    
    private void PlayJumpSound() { /* Play audio */ }
    private void OnGroundedChanged(bool isGrounded) { /* Play land sound */ }
}
```

## Troubleshooting

### Player doesn't move
- Check that Input System is enabled
- Verify PlayerData is assigned
- Ensure CharacterController has proper dimensions

### Camera rotates incorrectly
- Make sure camera is assigned to PlayerMovement._cameraTransform
- Check LookSensitivity value in PlayerData

### Crouch gets stuck
- Verify CanStandUp() check is working (Physics.CheckCapsule)
- Ensure there's enough space above the player

## Asset Store Preparation Checklist
- [x] All bugs fixed (crouch detection, deltaTime usage, camera positioning)
- [x] Code documented with XML comments
- [x] Clean architecture with interfaces and events
- [ ] Demo scene included
- [ ] Prefab with pre-configured player
- [ ] Multiple PlayerData presets (Realistic, Arcade, Low Gravity)
- [ ] Video tutorial
- [ ] PDF documentation

## Support
For issues and feature requests, please contact the developer.
