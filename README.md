# Unity2DEntitySpawn
This is a repository for my Unity 2D script, which spawns entities on a map.

**Script Settings**
**Object Prefab** (GameObject)
  The prefab of the object that will be spawned.
**Respawn Time** (float)
  The time interval between each spawn of a new object.
**Inner Radius** (float)
  The minimum distance from the camera at which an object can spawn.
**Outer Radius** (float)
  The maximum distance from the camera at which an object can spawn.
**Keep Momentum** (bool)
  Determines whether the object keeps its velocity when spawned (if set to true, the object retains its momentum; otherwise, it resets).
**Pool Size** (float)
  The number of objects to be pre-created in the object pool for spawning.
**Delay Seconds** (float)
  The delay in seconds before the first object spawn occurs.
**Enable Logs** (bool)
  Whether to log messages about object spawns to the console.
**Enable Gizmos** (bool)
  Whether to show visual indicators (Gizmos) of the spawn radii in the editor.
