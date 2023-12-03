# HEADING 1
## Heading 2
**bold**
*italics*

## Prereqs

Steamworks .NET unitypackage
https://github.com/rlabrecque/Steamworks.NET/releases
FizzySteamworks untiypackage
https://github.com/Chykary/FizzySteamworks/releases
Mirror
Install from Package Manager


## Modifications ##

1. fps.Gameplay.asmdef 
   fps.ui.asmdef
   fps.Game
   fps.AI
	- Add Mirror to assembly
	
2. Change Projectile speed to 30	

3. PlayerWeaponsManager.cs
   Health.cs
   Damageable.cs
   GameFlowManager.cs
	- changes
	
4. Disable in Scene
	- GameHUD
	- InGameMenu
	- Enemy Turret
	- Enemy_HoverBot
	- Mesh_Sun
	
5. Add Network Tranform to Bots
	- Sync Direction Server to Client

6. steam_appid.txt *MUST* ship with compiled game or it will not work!!! 