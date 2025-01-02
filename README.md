# Unity2DEntitySpawn
This is a repository for my Unity 2D script, which spawns entities on a map.

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
