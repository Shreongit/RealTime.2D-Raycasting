# Real-Time 2D Raycasting & Field-of-Sight Visualizer

A standalone real-time 2D computational-geometry system built in C# that demonstrates raycasting, ray-to-line-segment intersection, and dynamic field-of-sight generation entirely through the console.

The project implements the underlying geometry algorithms from first principles instead of relying on a physics engine.

---

## Features

- Real-time 2D raycasting
- Ray-to-line-segment intersection
- Cross-product based geometry calculations
- Closest intersection detection
- Dynamic field-of-sight generation
- Visibility point calculation
- Angular visibility sorting
- ASCII console visualization
- Interactive player movement
- Runtime intersection statistics
- Unit testing with xUnit
- Clean object-oriented architecture

---

## Technology Stack

| Component | Technology |
|---|---|
| Language | C# |
| Runtime | .NET 10 |
| Interface | Console / Terminal |
| Mathematics | `System.Numerics.Vector2` |
| Geometry | 2D Computational Geometry |
| Algorithm | Ray-Line Segment Intersection |
| Testing | xUnit |
| Version Control | Git + GitHub |
| Platform | Windows / Cross-platform .NET |

No graphical engine or rendering framework is required.

---

## Project Architecture

```text
RealTime2D-RayCasting/
│
├── src/
│   └── RaycastingVisualizer/
│       ├── Core/
│       ├── Geometry/
│       ├── Raycasting/
│       ├── World/
│       ├── Rendering/
│       └── Utils/
│
├── tests/
│   └── Raycasting.Tests/
│
├── assets/
│   └── levels/
│
├── docs/
│   ├── architecture.md
│   └── algorithm.md
│
├── README.md
├── LICENSE
└── .gitignore
```

---

## How It Works

The system places an observer inside a 2D environment containing wall segments.

Rays are generated from the observer across a configurable field of view.

Each ray is tested against every wall.

```text
Player
   |
   +---- Ray
   |
   +---- Ray
   |
   +---- Ray
   |
   v
Wall Segments
   |
   v
Intersection Tests
   |
   v
Closest Valid Hit
   |
   v
Visibility Points
   |
   v
Console Visualization
```

---

## Ray Representation

A ray is represented parametrically as:

```text
P + tD
```

Where:

- `P` is the ray origin
- `D` is the normalized direction
- `t` represents distance along the ray

---

## Ray-Segment Intersection

Walls are represented as line segments.

The system solves the intersection between:

```text
Ray:

P + tD
```

and:

```text
Segment:

A + uS
```

The 2D cross product is used:

```text
cross(A, B) = AxBy - AyBx
```

The resulting parameters are:

```text
t = cross(A - P, S) / cross(D, S)

u = cross(A - P, D) / cross(D, S)
```

A valid intersection satisfies:

```text
t >= 0
```

and:

```text
0 <= u <= 1
```

If multiple walls intersect a ray, the intersection with the smallest positive `t` is selected.

---

## Field of Sight

After calculating the closest intersection for each ray, the resulting visibility points are sorted by angle.

The ordered points form the boundary of the visible region.

```text
              WALL
        ───────────────

             \ | /
              \|/
           --- @ ---
              /|             / | 
        FIELD OF SIGHT
```

---

## Controls

```text
W       Move player up
A       Move player left
S       Move player down
D       Move player right

R       Reset player position

ESC     Exit application
```

---

## Build

Clone the repository and enter the project directory.

Then:

```powershell
dotnet restore
```

Build the complete solution:

```powershell
dotnet build
```

---

## Run

Run the console application:

```powershell
dotnet run --project src/RaycastingVisualizer
```

---

## Run Tests

Execute the complete test suite:

```powershell
dotnet test
```

The current test suite contains:

```text
14 tests
14 passed
0 failed
```

---

## Complexity

For:

```text
R = number of rays
W = number of walls
```

the basic raycasting algorithm performs:

```text
O(R × W)
```

intersection tests per frame.

For example:

```text
180 rays × 12 walls
= 2160 intersection tests
```

per raycasting update.

This implementation intentionally uses the straightforward approach so that the computational geometry remains easy to understand, test, and profile.

---

## Testing

The test suite covers:

- Cross-product calculations
- Ray/segment intersection
- Parallel geometry
- Intersections behind the ray
- Intersections outside segments
- Segment endpoint intersections
- Ray generation
- Wall intersection detection
- Closest-hit selection
- Visibility polygon construction
- Empty visibility data
- Visibility point preservation

---

## Documentation

Detailed technical documentation is available in:

```text
docs/algorithm.md
```

and:

```text
docs/architecture.md
```

---

## Future Improvements

Possible future extensions include:

- Configurable field of view
- Configurable ray count
- Interactive level loading
- Multiple observers
- Enemy field-of-sight simulation
- Dynamic obstacles
- 2D lighting simulation
- Spatial partitioning
- Quadtree acceleration
- Performance benchmarking
- Parallel ray processing
- More advanced terminal rendering

---

## Engineering Focus

The primary goal of this project is to demonstrate understanding of:

- Vector mathematics
- Parametric equations
- Cross products
- Computational geometry
- Raycasting
- Visibility algorithms
- Collision/intersection queries
- Algorithmic complexity
- Object-oriented design
- Unit testing
- Real-time simulation

---

## Project Status

**Development**

The core raycasting and computational-geometry implementation is functional and tested.

Current verification:

```text
Build:  PASS
Tests:  14 / 14 PASS
```

---

## License

MIT License
