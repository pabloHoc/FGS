# TODO

[x] Building Queue -> Generic Queue
[x] Agent Actions
Queue action to current action
[x] Conquest Region
[] Check if we create a with turns object (and on finished callback), or we
handle them separately
[x] Agent Action: Bless Region
[x] Effects (Increment / Decrement)
[x] Conditions (potential / allow)
[] District and capital buildings
[] One building queue per region
[] Undo / Redo
[] Command Pattern Refactor?
[] Save system

---

[X] Armies
[X] Combat
[x] AI
[x] Connected cities & Map
[x] Pathfinding

---

[] Building requirements
[] Events
[] Exploration
[] Quests
[] Combat reports

---

# AI

[x] HTN
[x] Utility AI
[x] HTN + Utility
[] Tag System? Tags vs Traits? -> Tags is for AI, Traits is for game
[] Need based AI

---

[] Are armies like agents (regarding actions) ? Should agents actions be interchangeable with armies'?

---

[] i11n

---

# MIGRATION

[x] Entities
[x] Core
[x] Definitions
[x] Commands
[x] Conditions
[] Helpers
[x] Scopes
[x] Utility AI
[] Tests
[x] UI -> Debug commands and empires (add logger to DI)
[] Undo in handler and save current state in command
[] Save / Load
[] Commands History

[] Review resources and modifiers -> check if we can change them from procedural to OOP
[] Stats for characters and dynamic cost of spells (mp)
[x] Remove entities thar are not entities
[] Review names: Service, Actions
[] Review name: Definition -> Data
[] Rename core to services, and move out game from there
[] Review refactoring type maps to string/enum maps (if possible)
[] Loggers (AI actions, etc) -> write in file

[] World entity

[] UI -> input manager, primitive drawing, camera, fix lot of DI