# PocketDroids

**PocketDroids** is one of the games done on the [Unity 3D location based game development with Mapbox](https://www.udemy.com/mapbox-unity-3d-essentials/) course at Udemy.

It's basically a PokémonGo clone using droids instead of Pokémon and using the awesome [Mapbox Unity SDK](https://www.mapbox.com/unity/) to use their map data instead of Google's.

Some features of the game include:
* Using real maps in Unity stylized via Mapbox Studio
* Using Vector Tiles filtering to position 3d meshes in real locations
* Particle effects
* Changing between scenes
* Data persistence

Things to improve:
* Keep the robots on the same spot everytime the scene changes. Now they are positioned randomly.
* Store the state of each XP so users can't farm them while restarting the app. XP should be reenabled after some predefined time
* The droid on the capture scene should be the one the user has clicked on instead of the example one
* The success screen is shown even when the ball has missed but has touched the droid while rolling on the floor. It should only be shown when the droid is the first thing it touches
* Create multiple styles in Mapbox Studio and swap them dinamically in function of the user time 