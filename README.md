# Unity2DEntitySpawn
Welcome to **Unity2DEntitySpawn**, a repository featuring a robust script for dynamically spawning entities in a 2D Unity game. Designed with versatility, efficiency, and ease of use in mind, this tool is essential for Unity 2D developers seeking streamlined spawning functionality.

# How To Use
1. Create an empty object in your scene and add a new component named `ObjectSpawn`.
2. Add the prefab object you want to spawn to the `Object Prefab` field in the component and configure the other settings (e.g., respawn time, radius, pool size).
3. Run the game, and objects will start spawning according to the configured settings.

# Key Features
- **Customizable Spawn Settings**: Tailor spawn behavior with adjustable parameters like spawn radius, timing, and object pooling.
- **Efficient Object Pooling**: Pre-instantiate objects to minimize runtime performance costs by reusing objects instead of instantiating new ones each time.
- **Debug Visualization**: Leverage Gizmos in the Unity Editor to visualize spawn areas and radii, making it easier to adjust spawn logic.
- **Detailed Logging**: Track spawning events with optional console logs to monitor the flow of object spawns and any errors or warnings.

# Script Settings

| **Parameter**           | **Type**       | **Description**                                                                                             |  
|-------------------------|----------------|-------------------------------------------------------------------------------------------------------------|  
| **Object Prefab**        | GameObject     | The prefab of the object that will be spawned.                                                              |  
| **Respawn Time**         | float          | The time interval between each spawn of a new object.                                                       |  
| **Inner Radius**         | float          | The minimum distance from the camera at which an object can spawn.                                          |  
| **Outer Radius**         | float          | The maximum distance from the camera at which an object can spawn.                                          |  
| **Keep Momentum**        | bool           | Determines whether the object keeps its velocity when spawned (true retains momentum; false resets it).      |  
| **Pool Size**            | int            | The number of objects to be pre-created in the object pool for spawning.                                    |  
| **Delay Seconds**        | float          | The delay in seconds before the first object spawn occurs.                                                  |  
| **Enable Logs**          | bool           | Whether to log messages about object spawns to the console for debugging purposes.                           |  
| **Enable Gizmos**        | bool           | Whether to show visual indicators (Gizmos) of the spawn radii in the editor for easier debugging.           |  
| **Random Size**          | bool           | Whether to spawn objects with random sizes (can be useful for adding variation).                             |  
| **Size Range**           | Vector2        | Defines the range of possible sizes for spawned objects (minimum and maximum scale).                        |  
| **Spawn Duration**       | float          | The maximum time (in seconds) that objects will spawn for before stopping.                                  |  
