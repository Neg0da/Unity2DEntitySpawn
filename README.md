# Unity2DEntitySpawn
Welcome to **Unity2DEntitySpawn**, a repository featuring a robust script for dynamically spawning entities in a 2D Unity game. Designed with versatility, efficiency, and ease of use in mind, this tool is essential for Unity 2D developers seeking streamlined spawning functionality.

# How To Use
1. Create an empty object and add a new component named ObjectSpawn.
2. Add the prefab object to be spawned and set up other settings.
3. Run the game.

# Key Features
Customizable Spawn Settings: Tailor spawn behavior with adjustable parameters like spawn radius, timing, and object pooling.<br>
Efficient Object Pooling: Pre-instantiate objects to minimize runtime performance costs.<br>
Debug Visualization: Leverage Gizmos in the Unity Editor to visualize spawn areas effectively.<br>
Detailed Logging: Track spawning events with optional console logs.<br>

# Script Settings
| **Parameter**           | **Type**       | **Description**                                                                                             |  
|--------------------------|----------------|-------------------------------------------------------------------------------------------------------------|  
| **Object Prefab**        | GameObject     | The prefab of the object that will be spawned.                                                             |  
| **Respawn Time**         | float          | The time interval between each spawn of a new object.                                                      |  
| **Inner Radius**         | float          | The minimum distance from the camera at which an object can spawn.                                         |  
| **Outer Radius**         | float          | The maximum distance from the camera at which an object can spawn.                                         |  
| **Keep Momentum**        | bool           | Determines whether the object keeps its velocity when spawned (true retains momentum; false resets it).     |  
| **Pool Size**            | float          | The number of objects to be pre-created in the object pool for spawning.                                   |  
| **Delay Seconds**        | float          | The delay in seconds before the first object spawn occurs.                                                 |  
| **Enable Logs**          | bool           | Whether to log messages about object spawns to the console.                                                |  
| **Enable Gizmos**        | bool           | Whether to show visual indicators (Gizmos) of the spawn radii in the editor.                               |  
| **Random Size**          | bool           | Whether to spawn objects with random sizes.                                                                |  
| **Size Range**           | Vector2        | Defines the range of possible sizes for spawned objects (minimum and maximum scale).                       |  

