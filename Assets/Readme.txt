Hello,

I have implemented the following components:
1) Player ship
2) Opponent’s ship
3) Bullets
4) HUD displaying score and remaining lives.
5) Pause game functionality
6) Game Over dialog box with replay functionality.
7) Infinite scrolling background, with temporary texture.


I have referred to one tutorial, to implement object pooling. 
http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/object-pooling

The Enemy ship uses a simple Finite State Machine, where it idles for some time, then goes to shoot state, and back to Idle. if it gets hit, it goes to hit state. Currently, the enemy has 1 Health point, so it goes to die state. If we increase the number of Health points, it will Die, after all HP is reduced to 0.

The EnemyManager is a singleton class, which spawns enemies at random intervals of time within an exposed range. It uses an object pool to spawn enemies.

Controller.cs is a base class for input controllers. KeyboardController and MouseController and derived from Controller. Similarly we can derive JoystickController, TouchController, etc.

Player bullets, enemies and enemy bullets use object pooling. We could also use object pool for the infinite terrain.

The player ship is a child of a VerticalScroller game object. The VerticalScroller moves upwards continuously, and the camera follows it. The Ship moves within this object.

Thank you.
Chetan
