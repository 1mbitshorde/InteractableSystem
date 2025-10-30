# Interactable System

System for generic Gameplay Interactions.

## How To Use

Create [IInteractable](/Runtime/IInteractable.cs) implementations and use [Interactor](/Runtime/Interactor.cs) component to interact with.

### Using Interactor

Attach [Interactor](/Runtime/Interactor.cs) inside your Player or any other object able to interact with [IInteractable](/Runtime/IInteractable.cs) implementations:

![Interactor](/Docs~/Interactor.png)

### Using IInteractable implementations

Create a component implementing `IInteractable`. 

Alternatively, if you want to quickly prototype, use the [UnityEventInteractable](/Runtime/UnityEventInteractable.cs) component and set its Unity Events:

![Unity Event Interactable](/Docs~/InterUnityEventInteractableactor.png).

You'll receive callbacks when `Interactor` gets close.

## Installation

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set and the correct git credentials to 1M Bits Horde.

- In this repo, go to Code button, select SSH and copy the URL.
- In Unity, use the **Package Manager** "Add package from git URL..." feature and paste the URL.
- Set the version adding the suffix `#[x.y.z]` at URL

---

**1 Million Bits Horde**

[Website](https://www.1mbitshorde.com) -
[GitHub](https://github.com/1mbitshorde) -
[LinkedIn](https://www.linkedin.com/company/1m-bits-horde)
