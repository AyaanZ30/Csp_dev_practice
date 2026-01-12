## Each [.csproj] file inside your project(s) compiles independently (produces one assembly (DLL/exe))

## DO NOT NEST PROJECTS (class libraries inside .NET projects) as:
### a] Source files get compiled 2x
### b] Type conflicts

## Good design rule : App -> CL (NOT App -> (contains) CL) {CL : Class Library}
### (Libraries should be reusable, not owned by one app)


# DIGITAL HANDBOOK (WILL BE UPDATED AS I PROCEED)


# 🧠 Understanding C# & .NET Project Structure (Beginner → Intermediate Guide)

This repository is intentionally structured to **teach how real-world C# / .NET projects are designed, built, and executed**.
If you are learning C# seriously (beyond syntax), this document is for you.

---

## 📦 Core Concept: Projects, Assemblies & Compilation

### 🔹 What is a `.csproj`?

* A **`.csproj`** file defines **one .NET project**
* Each `.csproj` compiles **independently**
* Output = **one assembly**

  * `.dll` → class library
  * `.exe` → application

> ✅ One `.csproj` → One assembly → One logical responsibility

---

## ❌ DO NOT Nest Projects

**Never place one project inside another project’s folder**.

### Why this is bad:

* ❌ Source files may compile **twice**
* ❌ Assembly/type conflicts
* ❌ Broken references
* ❌ Violates separation of concerns

### ❌ Bad

```
AppProject/
 ├─ ClassLibraryProject/
 │   └─ ClassLibrary.csproj
 └─ AppProject.csproj
```

### ✅ Good

```
RepoRoot/
 ├─ AppProject/
 │   └─ AppProject.csproj
 ├─ ClassLibraryProject/
 │   └─ ClassLibrary.csproj
 └─ Solution.sln
```

---

## 🧩 Correct Dependency Direction

### Golden Rule

> **Apps depend on Libraries — Libraries never depend on Apps**

### ✅ Correct

```
App → ClassLibrary
```

### ❌ Incorrect

```
ClassLibrary → App
```

Reason:

* Libraries should be **reusable**
* Apps are **consumers**, not owners

---

## 🏗️ Solution (`.sln`) vs Project (`.csproj`)

### 🔹 `.sln` (Solution)

* Logical grouping of projects
* Used by IDEs and `dotnet` CLI
* Does **not** compile code itself

### 🔹 `.csproj` (Project)

* Actual build unit
* Produces an assembly
* Can be built/run independently

> You can have **many projects**, but usually **one solution**.

---

## ▶️ Running & Building Projects

### 1️⃣ Run using Solution (from root)

```bash
dotnet run
```

* Works **only if** one startup project exists
* Uses the `.sln`

---

### 2️⃣ Run a specific project (recommended)

```bash
dotnet run --project SchoolHRAdministration/SchoolHRAdministration.csproj
```

✔ Most explicit
✔ Best for multi-project repos

---

### 3️⃣ Build first, then run

```bash
dotnet build
```

Then:

```bash
dotnet run --project SchoolHRAdministration/SchoolHRAdministration.csproj
```

✔ Useful for CI / debugging build errors

---

## 🗃️ `bin/` and `obj/` folders (IMPORTANT)

These folders are **auto-generated** during build.

### ❌ Never commit:

* `bin/`
* `obj/`

They contain:

* Compiled binaries
* Temporary build files
* Cache data

✔ Always ignore them using `.gitignore`

---

## 🧠 Interfaces & Abstractions (Key C# Concept)

### Why interfaces matter:

* Enable **polymorphism**
* Allow **multiple implementations**
* Reduce coupling

Example:

* `IEmployee` → basic employee contract
* `IExperiencedEmployee` → optional behavior

> Code should depend on **what an object does**, not **what it is**

---

## 🧬 Inheritance vs Interfaces

### Inheritance (`EmployeeBase`)

* Shared **state + behavior**
* `virtual` / `override`

### Interfaces (`IEmployee`, `IExperiencedEmployee`)

* Shared **capability / contract**
* No implementation

✔ Use inheritance for **"is-a"** relationships
✔ Use interfaces for **"can-do"** behavior

---

## 🔁 Delegates (Functional Core of C#)

### What is a delegate?

> A delegate is a **type-safe function pointer**

Example:

* `EarningsCalculator` represents *any method* that takes:

  * `int years`
  * `decimal salary`
  * returns `decimal`

Why this matters:

* Enables **behavior injection**
* Basis of LINQ, events, async pipelines

---

## 🧮 Yield & Lazy Evaluation

Using `yield return`:

* Values are produced **one at a time**
* Execution is deferred

Benefits:

* Memory efficient
* Clean separation of logic

---

## ⚠️ Common Beginner Mistakes (Avoid These)

* ❌ Accessing subclass properties via base interface
* ❌ Nesting projects
* ❌ Committing `bin/` and `obj/`
* ❌ Mixing responsibilities inside one project

---

## 🧠 Mental Model to Remember

```
Solution (.sln)
 ├─ App (.csproj) → ENTRY POINT
 ├─ Class Library (.csproj) → LOGIC
 └─ Class Library (.csproj) → SHARED MODELS
```

Each project:

* Compiles independently
* Has a single responsibility

---

## ✅ Final Thought

> **Clean structure matters more than clever code**

Once structure is right:

* Testing is easier
* Scaling is easier
* Learning advanced .NET becomes natural

---

🚀 *This repository intentionally follows real-world .NET design principles.*
