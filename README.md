# Unity2DEntitySpawn
Welcome to **Unity2DEntitySpawn**, a repository featuring a robust script for dynamically spawning entities in a 2D Unity game. Designed with versatility, efficiency, and ease of use in mind, this tool is essential for Unity 2D developers seeking streamlined spawning functionality.

# Key Features
Customizable Spawn Settings: Tailor spawn behavior with adjustable parameters like spawn radius, timing, and object pooling.<br>
Efficient Object Pooling: Pre-instantiate objects to minimize runtime performance costs.<br>
Debug Visualization: Leverage Gizmos in the Unity Editor to visualize spawn areas effectively.<br>
Detailed Logging: Track spawning events with optional console logs.<br>

**Script Settings** <br>
**Object Prefab** (GameObject) <br>
  The prefab of the object that will be spawned. <br>
**Respawn Time** (float) <br>
  The time interval between each spawn of a new object. <br>
**Inner Radius** (float) <br>
  The minimum distance from the camera at which an object can spawn. <br>
**Outer Radius** (float) <br>
  The maximum distance from the camera at which an object can spawn. <br>
**Keep Momentum** (bool) <br>
  Determines whether the object keeps its velocity when spawned (if set to true, the object retains its momentum; otherwise, it resets). <br>
**Pool Size** (float) <br>
  The number of objects to be pre-created in the object pool for spawning. <br>
**Delay Seconds** (float) <br>
  The delay in seconds before the first object spawn occurs. <br>
**Enable Logs** (bool) <br>
  Whether to log messages about object spawns to the console. <br>
**Enable Gizmos** (bool) <br>
  Whether to show visual indicators (Gizmos) of the spawn radii in the editor. <br>

# How to use
How to Use<br>
Attach the script to a GameObject in your Unity project.<br>
Assign the required prefab and configure settings via the Inspector.<br>
Press Play to see objects spawn dynamically based on your configuration.<br>
