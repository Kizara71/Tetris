# 🏆 Final Project — Marking Criteria
### Design Patterns Integration Project (Unity C#)

> **Scope:** This project is the capstone for both the *C# Intermediate Course* (5 days) and the *Design Patterns for Game Development* roadmap (7 days).
> The student must demonstrate mastery of C# language fundamentals **as expressed through** well-structured design patterns in a real, playable Unity scene.

---

## 🎯 Project Brief

Build an **original, playable Unity scene** from scratch that integrates **at least 2 of the 5 Design Patterns** (Strategy, State, Command, Singleton, Factory) while simultaneously demonstrating the **C# intermediate language features** covered in the first roadmap.

The scene must have a functioning core gameplay loop. Visual polish is secondary — **code quality and architectural justification are everything.**

> [!IMPORTANT]
> All **5 SOLID principles** must be identifiable in your codebase. This is a hard requirement, not an optional quality metric.

> [!NOTE]
> 📢 **An end-of-week discussion will be held at the end of the project week.** The date will be announced in advance. Every student will be asked a short set of questions about their own code — be ready to explain your decisions, not just your implementation.

---

## 📊 Marking Overview

| Section | Marks Available |
|---------|----------------|
| A — Design Patterns (Structure & Correctness) | 30 |
| B — C# Language Fundamentals | 25 |
| C — SOLID Principles & Code Quality (all 5 mandatory) | 20 |
| D — Architecture & Justification | 15 |
| E — Playability & Scene Completeness | 10 |
| F — End-of-Week Discussion | 10 |
| **Total** | **110** |

> [!NOTE]
> The total is marked out of **110** to allow bonus marks to be absorbed naturally. Final grades reported out of 100 — scores above 100 are capped.

---

## 🅐 Section A — Design Patterns (30 marks)

**Minimum 2 patterns are required.** Each correctly implemented pattern is worth up to **15 marks** (2 patterns × 15 = 30). Implementing a 3rd or 4th pattern earns bonus marks (see Bonus section). An unused pattern scores 0.

> [!IMPORTANT]
> A pattern is only credited if it has a **proper interface** separating abstraction from implementation, and genuinely solves a design problem — not bolted on artificially.

### Per-Pattern Rubric (15 marks each)

| Mark Band | Criteria |
|-----------|---------|
| **13–15** | Pattern is correctly implemented with a clear interface. The student can articulate *what problem it solves in their specific scene*. Implementation is clean, no redundant coupling. |
| **10–12** | Pattern is correctly implemented and has a working interface. Minor structural issues (e.g., context class slightly over-coupled). The "why" is implied but not clearly explained. |
| **7–9** | Pattern is recognisably present but has notable issues: missing one lifecycle method, context calls concrete type directly, or interface is too thin to be meaningful. |
| **4–6** | Pattern is attempted. Core idea is there (e.g., a strategy class exists) but structure is incomplete or incorrect. No real separation of concerns. |
| **1–3** | Pattern is mentioned in code or comments but is not functionally implemented. |
| **0** | Pattern not present, or pattern is a direct copy-paste from the daily exercises with no adaptation. |

### Pattern-Specific Checkpoints

| Pattern | Must-Have Checkpoint |
|---------|---------------------|
| **Strategy** | `IWeapon` / `IAbility` (or equivalent) interface exists · At least 3 concrete strategies · Context swaps strategy at runtime without `if/switch` on type |
| **State** | `IState` interface with `Enter`, `Update`, `Exit` methods · At least 3 concrete states · State owns its own transition logic · Context calls `TransitionToState()` |
| **Command** | `ICommand` with `Execute()` and `Undo()` · At least 2 concrete commands · `CommandInvoker` holds a `Stack<ICommand>` · Undo works correctly |
| **Singleton** | Generic `Singleton<T>` base class · `DontDestroyOnLoad` used · Duplicate prevention in `Awake` · Used **only** for a true global system (GameManager, AudioManager) |
| **Factory** | `IEnemyFactory` (or equivalent) interface · Client (`EnemySpawner`) depends on the **interface**, not the concrete factory · No `Instantiate` call outside the factory |

---

## 🅑 Section B — C# Language Fundamentals (25 marks)

These are the language features taught in the C# Intermediate roadmap (Days 1–4). They must appear **organically** in the project code — not in isolated demo scripts.

| # | C# Feature | Marks | Evidence Required |
|---|------------|-------|------------------|
| B1 | **Enums** | 3 | At least one meaningful enum used in game logic (e.g., `EnemyType`, `GameState`, `WeaponType`) |
| B2 | **Properties** | 3 | At least one C# property with `get`/`set` (not just public fields). Bonus if setter has validation logic. |
| B3 | **Interfaces** | 4 | Minimum 2 interfaces defined and implemented (pattern interfaces count, plus at least one non-pattern interface e.g., `IDamageable`) |
| B4 | **Events & Delegates** | 4 | At least 2 C# events wired up with subscribers (e.g., `OnScoreChanged`, `OnGameOver`). Events must actually fire during gameplay. |
| B5 | **Generics** | 4 | At least one generic class or method used (e.g., generic `Singleton<T>`, generic `Pool<T>`, or a generic data container) |
| B6 | **Collections (Dictionary / Stack / List)** | 3 | At least one non-array collection used meaningfully (the `Stack<ICommand>` in the Command pattern counts) |
| B7 | **Try/Catch** | 2 | At least one `try/catch` block wrapping a genuinely risky operation (file I/O, null access, data parsing) |
| B8 | **Namespaces** | 2 | All scripts organised into namespaces matching their folder structure (e.g., `MyGame.Combat`, `MyGame.Core`) |

**Partial credit applies.** Feature present but misused = half marks. Feature not present = 0.

---

## 🅒 Section C — SOLID Principles & Code Quality (20 marks)

> [!IMPORTANT]
> All **5 SOLID principles** are **mandatory**. Each principle must be clearly identifiable in the codebase. A principle that cannot be pointed to in the code scores **0** for that row — partial credit is only awarded if the principle is present but imperfectly applied.

| # | Principle / Quality | Marks | What to Look For |
|---|---------------------|-------|-----------------|
| C1 | **Single Responsibility** *(mandatory)* | 4 | Each class does one job. No "god scripts" that manage input, UI, AI, and audio all at once. Student must be able to point to at least one class that was deliberately split to satisfy SRP. |
| C2 | **Open/Closed** *(mandatory)* | 3 | New behaviours (weapons, states, enemies) can be added by creating a new class, not by editing existing ones. The interface/abstract layer is the evidence. |
| C3 | **Liskov Substitution** *(mandatory)* | 3 | Any concrete strategy/state/command can be swapped in without breaking the context class. Demonstrated by the fact that interfaces are upheld exactly. |
| C4 | **Interface Segregation** *(mandatory)* | 2 | No bloated interface that forces implementors to leave methods empty. Each interface is focused and small. |
| C5 | **Dependency Inversion** *(mandatory)* | 4 | High-level classes depend on interfaces, not on concrete types. Singleton is **not** passed everywhere as a shortcut. |
| C6 | **Naming Conventions** | 2 | `PascalCase` for classes/methods/properties · `_camelCase` for private fields · no abbreviations |
| C7 | **Comments & Summaries** | 2 | Each interface and each pattern-critical class has a summary comment explaining its role. |

---

## 🅓 Section D — Architecture & Justification (15 marks)

This section is assessed through the student's **written `Day7_Summary.md`** (or equivalent design doc).

| # | Criterion | Marks |
|---|-----------|-------|
| D1 | **Architecture sketch** is included — a diagram or list of classes, their pattern roles, and how they connect | 3 |
| D2 | **For each pattern used**, the student explains *what specific problem it solved in their scene* (not a generic definition) | 5 |
| D3 | **SOLID evidence** — the student points to a specific class or interface in their project for each of the 5 SOLID principles and explains how it satisfies that principle | 4 |
| D4 | **Honest reflection** — identifies which pattern felt the most natural and which felt the most forced, and explains why | 2 |
| D5 | **C# concepts identified** — the student lists which C# intermediate features appear in the final project and where | 1 |

> [!NOTE]
> D2 is the most important sub-criterion. A technically perfect project with no written justification scores at most 6/10 in this section.

---

## 🅔 Section E — Playability & Scene Completeness (10 marks)

| # | Criterion | Marks |
|---|-----------|-------|
| E1 | Scene opens in Play Mode **without errors** in the Console | 3 |
| E2 | At least one **core gameplay loop** runs (e.g., enemies spawn, player can interact, score updates) | 4 |
| E3 | At least one **UI element** is present and updates dynamically (score, state display, command log, etc.) | 2 |
| E4 | The scene can be demoed for **60 seconds** without crashing or throwing null-reference exceptions | 1 |

---

## 🅕 Section F — End-of-Week Discussion (10 marks)

> [!NOTE]
> 📢 The discussion date will be **announced before the end of the project week**. It is a short, informal verbal session — not a formal presentation. Students will be asked 3–5 questions directly about their own submitted code.

The purpose of this section is to confirm the student genuinely understands their own project. Submitted code alone cannot demonstrate understanding — this conversation can.

### What to Expect
- The assessor will open the student's submitted Unity project
- Questions are drawn from their **actual code** — not hypothetical scenarios
- There are no trick questions; the goal is to hear the student's own reasoning

### Discussion Rubric (10 marks)

| Mark Band | Criteria |
|-----------|---------|
| **9–10** | Student clearly explains *why* each pattern was chosen, can point to where each SOLID principle is applied in their code, and describes at least one C# language feature they used and why. Answers are confident and specific. |
| **7–8** | Student explains their patterns with minor gaps. Can identify most SOLID principles in their code. Answers are mostly accurate but occasionally vague. |
| **5–6** | Student understands general concepts but struggles to connect them to their own code. Some answers are generic ("I used Strategy because the book said so"). |
| **3–4** | Student has surface-level understanding. Answers suggest the code was written without full comprehension. |
| **1–2** | Student cannot meaningfully explain the code they submitted. |
| **0** | Student does not attend without prior notice, or it is evident the work is not their own. |

### Sample Discussion Questions

| # | Question |
|---|----------|
| 1 | *"Point to one place in your code where the Open/Closed principle is applied. Why did you structure it that way?"* |
| 2 | *"If I wanted to add a new weapon/enemy type, which files would you need to create or change, and why?"* |
| 3 | *"Why did you choose [Pattern X] for this part of your game? What problem was it solving?"* |
| 4 | *"Where in your project did you use a C# event? Walk me through how it fires and who reacts to it."* |
| 5 | *"Is there any pattern you considered using but decided against? Why?"* |

---

## 🚫 Automatic Deductions

| Infringement | Deduction |
|-------------|-----------|
| Direct copy-paste from Days 2–6 exercises without redesign | −10 per file identified |
| Pattern counted but has no interface | −5 per pattern |
| `Singleton` used for a non-global system (e.g., singleton enemy) | −5 |
| `Instantiate` / `Destroy` called outside a Factory class | −3 per occurrence |
| No namespace on any script | −5 |
| Project does not open / scene does not load | Section E capped at 0 |

---

## 🌟 Bonus Marks (up to +15, does not exceed 100)

| Bonus | Marks |
|-------|-------|
| **3rd pattern** implemented correctly and justified | +8 |
| **4th or 5th pattern** implemented correctly and justified (per extra pattern) | +4 each |
| **Object Pool** implemented inside the Factory (`Get()` / `Release()` replacing `Instantiate` / `Destroy`) | +3 |
| **Replay system** — press a key to replay recorded commands on a ghost object | +3 |
| **ScriptableObject event channel** used instead of a plain C# event (Dependency Inversion bonus) | +2 |
| `IEnumerable` / `yield return` used in a meaningful way in the project | +2 |

---

## ✅ Submission Checklist

Before submitting, the student must verify:

- [ ] At least **2 patterns** are implemented, each with a proper interface
- [ ] All pattern-specific checkpoints in Section A are met for each chosen pattern
- [ ] All **5 SOLID principles** are identifiable in the codebase — point to a specific class/interface for each
- [ ] All 8 C# features in Section B are present in the project code
- [ ] All scripts are in a **namespace**
- [ ] **Naming conventions** are consistent throughout
- [ ] `Day7_Summary.md` (or equivalent) contains architecture sketch + pattern justifications + SOLID evidence
- [ ] Scene runs in **Play Mode without Console errors**
- [ ] No direct copy-paste from daily exercises — code is adapted and redesigned

---

## 📋 Marking Summary Sheet

> For the assessor — fill in per student.

| Section | Max | Score | Notes |
|---------|-----|-------|-------|
| A1 — Pattern: _____________ *(required)* | 15 | /15 | |
| A2 — Pattern: _____________ *(required)* | 15 | /15 | |
| B — C# Language Fundamentals | 25 | /25 | |
| C1 — Single Responsibility *(mandatory)* | 4 | /4 | |
| C2 — Open/Closed *(mandatory)* | 3 | /3 | |
| C3 — Liskov Substitution *(mandatory)* | 3 | /3 | |
| C4 — Interface Segregation *(mandatory)* | 2 | /2 | |
| C5 — Dependency Inversion *(mandatory)* | 4 | /4 | |
| C6 — Naming Conventions | 2 | /2 | |
| C7 — Comments & Summaries | 2 | /2 | |
| D — Architecture & Justification | 15 | /15 | |
| E — Playability | 10 | /10 | |
| F — End-of-Week Discussion | 10 | /10 | |
| **Deductions** | — | − | |
| **Bonus (3rd+ patterns, pool, replay, etc.)** | +15 | + | |
| **TOTAL** | **110** | **/110 (capped at 100)** | |

---

> [!TIP]
> **For students:** The single best question to ask yourself before submitting is:
> *"Can I swap out one concrete class — one weapon, one state, one enemy type — without touching any other file?"*
> If yes, you've understood what design patterns are for.
