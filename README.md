# Platypus
A platformer where you play as a platypus made in Unity.<br>

- [x] Player
- [ ] AI oppentend
- [ ] Enviorment
- [x] Statemachine
- [X] Pick-up system
- [X] Delivery point
- [ ] Waterfall
- [ ] Game loop
- [ ] Alot of art

## Script highlights
### Statemachine
[Base State Machine](Assets/Scripts/Framework/StateMachine/StateMachineManager.cs)<br>
[Base State](Assets/Scripts/Framework/StateMachine/BaseState.cs)<br>
[Player State Machine](Assets/Scripts/Framework/StateMachine/PlayerStateManager.cs)<br>
[Player Base State](Assets/Scripts/Framework/StateMachine/PlayerBaseState.cs)<br>
### Player States
[Attacking State](Assets/Scripts/Player/States/PlayerSmackState.cs)<br>
[Dash(Jump) State](Assets/Scripts/Player/States/PlayerDashState.cs)<br>
[Roll State](Assets/Scripts/Player/States/PlayerRollState.cs)<br>
[Walking State](Assets/Scripts/Player/States/PlayerWalkingState.cs)<br>
### Pick-up System
[Pick-up](Assets/Scripts/Framework/Pick-up/Pickup.cs)<br>
[Pick-up System](Assets/Scripts/Framework/Pick-up/PickupSystem.cs)<br>
[Delivery Point](Assets/Scripts/Framework/Pick-up/DeliveryPoint.cs)<br>
[Delivery Point UI](Assets/Scripts/UI/World/DeliveryPointUI.cs)<br>
### Other
[Ground Cheker](Assets/Scripts/Framework/GroundChecker.cs)<br>
[Singleton](Assets/Scripts/Framework/Singleton.cs)<br>