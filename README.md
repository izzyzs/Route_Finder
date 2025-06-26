# Route Suggestion App

# 1) Basic MVP Ideas: _1/2/25_

-   Users are offered multiple ways to get to their destination, starting with shortest travel duration to longest
-   Users can create their own routes & have a spot to save their favorite routes
-   Routs are updated live based on delays & incidents/accidents

# 2) Planning User Process: _1/8/25_

## Frontend or Backend?

| Frontend/backend? | Steps                                                                      | MVP? |
| ----------------- | -------------------------------------------------------------------------- | ---- |
| F                 | User enters start and end point                                            | Y    |
| Data transfer:API | start and end points; front → back                                         | Y    |
| B                 | routes are found                                                           | Y    |
| B                 | Routes are ordered                                                         | Y    |
| Data transfer:API | ordered routes; back → front                                               | N    |
| F                 | Routes presented to user                                                   | Y    |
| F                 | User selects their route and uses it                                       | Y    |
| F/B               | guided through the route entire trip; alternatives offered if delays found | N    |

## Extras (Not necessary for MVP)

-   **In-house graph traversal** ⇒ start with a module that handles that for now
-   **Map Visual** ⇒ First implement route finding and simple presenting with route description; visualization is secondary

# 3) Simplification: _1/8/25_

1. **Receive and process** start and destination
2. **Find** routes
3. **Compare** routes **in terms of time**
4. **Present** routes in said order
5. **Display chosen route** to user

# 4) Extremely Abstract Graph: 1/8/25

**_Note: RouteFinder and RouteSort will have to be further detailed…_**

```mermaid
classDiagram
note "1. Find routes process"
note "2. Favorite route process (this design is still under work)"
RoutesFinder --|> RouteSort : (1) data -> found_routes
User --|> RoutesFinder : (1) FindRoute(start_point, endpoint)
RouteSort --|> User : (1) data -> sorted_routes
User --|> Storage : (1 / 2a) data -> fav_route
User <|-- Storage : (2b) data -> fav_route
Storage --|> RTRouteFinder : (2b) data -> fav_route
RTRouteFinder <|--|> User : REALTIME -> route_details
```

# 5) Data…

# 6) Talk About The Basics... [6/12/2025 10:55 PM EST]

-   **What is a Route?**
    -> A route is simply a journey with multiple _legs_ and _transfers_.
    A _leg_ is a single ride that takes place on one train which starts
    with a boarding and ends with a descending. When a person descends
    off a train and boards a second at the same or a close station that's called a _transfer._
-   **What is the best journey?**
    -> The shortest one in terms of time. The Weight of a leg is determined by the amount of time spent on said leg.
-   **Solution for finding path**
    -> 2 Options:
    1. Use a path finding API (with google probably)
    2. Write out a solution myself, modifying a pathfinding algorithm.

---

A graph needs to be made

## Sources

MTA’s GTFS (schedule and real-time)

## Project Details

---

-   ASP.NET Core 8 [Backend]
