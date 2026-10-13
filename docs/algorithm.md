# Raycasting Algorithm

## Overview

The project implements real-time 2D raycasting using computational geometry.

Each ray originates from the player position and is tested against every wall segment in the environment.

The closest valid intersection is selected as the visible point for that ray.

---

## Ray Representation

A ray is represented as:

P + tD

Where:

- `P` = ray origin
- `D` = normalized ray direction
- `t >= 0` = distance along the ray

---

## Wall Representation

Each wall is represented as a line segment:

A + uS

Where:

- `A` = segment start
- `S` = segment direction
- `u` = position along the segment

A valid point on the segment satisfies:

0 <= u <= 1

---

## Ray-Segment Intersection

The system solves:

P + tD = A + uS

The 2D cross product is used:

cross(A, B) = AxBy - AyBx

The intersection parameters are:

t = cross(A - P, S) / cross(D, S)

u = cross(A - P, D) / cross(D, S)

The intersection is valid when:

t >= 0

and:

0 <= u <= 1

---

## Parallel Detection

If:

cross(D, S) ≈ 0

the ray and wall segment are parallel.

A small epsilon value is used to handle floating-point precision:

epsilon = 1e-9

---

## Closest Intersection

A ray may intersect multiple wall segments.

The algorithm keeps only the intersection with the smallest positive `t`.

This represents the first obstacle encountered by the ray.

---

## Ray Generation

The configured field of view is divided into evenly spaced ray directions.

For example:

- Field of View: 120 degrees
- Ray Count: 180

Each ray receives an angular direction between:

-60 degrees

and:

+60 degrees

relative to the player's forward direction.

---

## Visibility Points

Each ray that successfully intersects a wall produces a visibility point.

Each point stores:

- Position
- Angle
- Distance from player

The points are sorted by angle.

---

## Visibility Polygon

The sorted visibility points form the boundary of the visible region.

Connecting the points in angular order produces the visibility polygon.

---

## Complexity

For:

- `R` = number of rays
- `W` = number of walls

The basic raycasting algorithm has:

O(R × W)

intersection tests per frame.

For example:

180 rays × 12 walls

= 2160 intersection tests per frame.

---

## Numerical Stability

The implementation uses an epsilon threshold when checking whether two vectors are parallel.

This prevents unstable results caused by floating-point precision.

---

## Edge Cases

The implementation handles:

- Parallel ray and wall
- Ray pointing away from a wall
- Intersection outside the wall segment
- Intersection at a segment endpoint
- Multiple intersections
- No valid intersection
- Zero direction vectors

---

## Implementation Flow

```text
Player Position
       |
       v
Generate Rays
       |
       v
For Each Ray
       |
       v
Test Against Every Wall
       |
       v
Calculate Intersection
       |
       v
Reject Invalid Intersections
       |
       v
Select Closest Hit
       |
       v
Store Visibility Point
       |
       v
Sort Points By Angle
       |
       v
Build Visibility Boundary
       |
       v
Render To Console