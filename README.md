# Programming Language
This repository contains the lab assignments for CSE4050 (Programming Languages) at Sogang University, 2024.

## Prerequisite
* .NET is required
* Python >= 3.8

## Structure
The README.md file in each directory contains instructions on how to use each program.

The repository structure is as follows:
* **CMinusPtrInterpreter/**: a interpreter for C- language with a pointer operations.
  * **interpreter.py**
* **FMinusPtrInterpreter/**: a interpreter for F- language.
  * **interpreter.py**
* **FMinusType/**: a type system for F- language.
  * **type_system.py**

## Notations for Semantics
* 𝐗→𝒀: the set of partial functions from 𝑿 to 𝒀.
* 𝑴⊢𝒆⇓𝒗: given memory 𝑴, expression 𝒆 is evaluated into 𝒗.
* <𝑴,𝒔>⇒𝑴′: statement 𝒔 changes memory state from 𝑴 to 𝑴′.
* 𝑴⊢𝒍𝒗↓𝒍: given memory 𝑴, l-value 𝒍𝒗 is evaluated into a location 𝒍.
* 𝝆⊢𝒆⇓𝒗: given environment 𝝆, expression 𝒆 is evaluated into 𝒗.
* 𝐟=<𝒙,𝒆,𝝆>: representation of a function value. It is tuple of argument name(𝒙), expression(𝒆) for function body, and environment(𝝆) at the point of definition.
* 𝚪⊢𝒆∶𝒕: given type environment 𝜞, type of 𝒆 must be 𝒕.

## Languages
The programs in this repository implements a simplified version of existing programming language systems.
* F- is a simplified version of the F# programming language.
![image](https://github.com/user-attachments/assets/8080f6ad-c976-4d88-89ad-41520a514c6c)
* C- is a simplified version of the C programming language.
![image](https://github.com/user-attachments/assets/4f11e879-0f30-41a7-a2e9-e8d411b6ff1f)
