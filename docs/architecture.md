# System Architecture

## Overview

Real-Time 2D Raycasting & Field-of-Sight Visualizer is a standalone C# console application that demonstrates real-time 2D computational geometry.

The system is divided into independent components responsible for application control, geometry calculations, raycasting, world representation, console rendering, and utilities.

## Architecture

```text
                    Program
                       |
                       v
                     Game
                       |
          +------------+------------+
          |            |            |
          v            v            v
       Input        World       Raycaster
          |            |            |
          |            |            v
          |            |       GeometryMath
          |            |            |
          |            |            v
          |            |      VisibilityPoints
          |            |            |
          +------------+------------+
                       |
                       v
                 ConsoleRenderer
                       |
                       v
                    Terminal
```

## Project Layers

### 1. Core

Location:

```text
src/RaycastingVisualizer/Core/
```

Responsibilities:

- Application lifecycle
- Main game loop
- Configuration
- Keyboard input

Components:

- `Game.cs`
- `GameConfig.cs`
- `InputManager.cs`

`Game` coordinates the update and rendering cycle.

---

### 2. Geometry

Location:

```text
src/RaycastingVisualizer/Geometry/
```

Responsibilities:

- Mathematical representation of rays
- Mathematical representation of line segments
- Intersection results
- Computational geometry algorithms

Components:

- `Ray2D.cs`
- `LineSegment.cs`
- `IntersectionResult.cs`
- `GeometryMath.cs`

This layer contains the core ray-to-segment intersection implementation.

---

### 3. Raycasting

Location:

```text
src/RaycastingVisualizer/Raycasting/
```

Responsibilities:

- Ray generation
- Wall intersection testing
- Closest-hit selection
- Visibility point generation
- Visibility polygon construction

Components:

- `Raycaster.cs`
- `VisibilityPoint.cs`
- `VisibilityPolygon.cs`

The `Raycaster` uses the geometry layer rather than implementing mathematical calculations directly.

---

### 4. World

Location:

```text
src/RaycastingVisualizer/World/
```

Responsibilities:

- Player position
- Wall storage
- World construction
- Level creation

Components:

- `World.cs`
- `Wall.cs`
- `LevelBuilder.cs`

The world layer contains environment data but does not perform raycasting calculations.

---

### 5. Rendering

Location:

```text
src/RaycastingVisualizer/Rendering/
```

Responsibilities:

- Convert calculated geometry into terminal output
- Draw walls
- Draw rays
- Draw intersection points
- Draw the player
- Display runtime statistics
- Display debugging information

Components:

- `ConsoleRenderer.cs`
- `DebugRenderer.cs`

The renderer does not determine visibility. It only visualizes the results produced by the simulation.

---

### 6. Utilities

Location:

```text
src/RaycastingVisualizer/Utils/
```

Responsibilities:

- Reusable mathematical helper functions
- Angle conversion
- Direction generation
- Angle normalization
- Distance calculations

Component:

- `MathUtils.cs`

---

## Runtime Data Flow

Each frame follows this sequence:

```text
1. Read keyboard input
          |
          v
2. Update player position
          |
          v
3. Generate ray directions
          |
          v
4. Test each ray against every wall
          |
          v
5. Select closest valid intersection
          |
          v
6. Store visibility points
          |
          v
7. Sort visibility points by angle
          |
          v
8. Render world and raycasting data
          |
          v
9. Wait for next frame
```

---

## Game Loop

The main loop is controlled by `Game.cs`.

Conceptually:

```csharp
while (running)
{
    ProcessInput();
    CastRays();
    Render();
}
```

This provides continuous recalculation as the player moves.

---

## Geometry Dependency

The dependency direction is intentionally one-way:

```text
Core
 |
 +----> World
 |
 +----> Raycasting
            |
            v
         Geometry

Rendering
 |
 +----> World
 |
 +----> Raycasting
```

The geometry layer remains independent from rendering and game-specific logic.

This makes the mathematical components easier to test and reuse.

---

## Object-Oriented Design

The project uses small focused classes rather than placing all logic inside `Program.cs`.

Examples:

### `Ray2D`

Represents a mathematical ray.

### `LineSegment`

Represents a wall segment.

### `GeometryMath`

Contains computational geometry operations.

### `Raycaster`

Coordinates ray generation and intersection testing.

### `World`

Maintains the simulation environment.

### `ConsoleRenderer`

Converts simulation state into terminal output.

This separation keeps responsibilities clear and makes future extensions easier.

---

## Testing Architecture

Tests are located in:

```text
tests/Raycasting.Tests/
```

The tests verify:

- Cross-product calculations
- Valid ray intersections
- Parallel rays
- Intersections behind the ray
- Intersections outside segments
- Endpoint intersections
- Ray generation
- Closest intersection selection
- Visibility polygon construction

The project currently has:

```text
14 tests
14 passed
0 failed
```

---

## Performance Model

The basic raycasting implementation performs:

```text
Ray Count × Wall Count
```

intersection checks per frame.

Therefore:

```text
Time Complexity = O(R × W)
```

where:

- `R` = number of rays
- `W` = number of walls

The current implementation intentionally prioritizes clarity and algorithmic transparency over advanced spatial acceleration.

---

## Future Optimization

Potential future improvements include:

- Spatial partitioning
- Uniform grids
- Quadtrees
- Bounding volume checks
- Reduced ray counts in low-detail regions
- Parallel ray processing
- Incremental visibility updates

These optimizations can be evaluated after profiling the baseline implementation.

---

## Console Rendering

The application does not use a graphical framework.

The visual output is generated entirely through the terminal using characters.

Example:

```text
############################################################

                 . . . . .
              .             .
            .       @         .
              .             .
                 . . . . .

############################################################

Rays: 180
Walls: 12
Intersections: 97
```

This keeps the project focused on the underlying computational geometry while still providing real-time visual feedback.

---

## Design Goals

The architecture prioritizes:

1. Mathematical correctness
2. Separation of concerns
3. Testability
4. Readability
5. Real-time execution
6. Console-based visualization
7. Easy future extension

The project is intentionally standalone and does not depend on the SA2DGE engine.
