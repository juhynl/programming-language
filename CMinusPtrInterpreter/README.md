# CMinusPtrInterpreter
This directory contains a interpreter for C- language with a pointer operations.
## Usage
Run interpreter.py.
```
python3 interpreter.py
```
First, enter the program code into the interpreter. 
* Put a semicolon (;) between each statement.
* Do not put a semicolon (;) at the end of the program.

To exit the type inference program, enter ":exit".
```
============================================================
CMinusPtr Interpreter
============================================================
Building the program. Please wait...
Done!
============================================================
Please enter a target program:
         - Put a semicolon (;) between each statement.
         - Do not put a semicolon (;) at the end of the program.
         - Enter ":exit" to exit the program.
============================================================
x = 1;
y = 2;
p1 = &x;
p2 = &y;
*p1 = *p2 + 10        
------------------------------------------------------------
result: {
  p1 -> x
  p2 -> y
  x -> 12
  y -> 2
}
============================================================
```
interpreter.py saves interpreted programs and results in the 'log' directory.

## Semantics
![image](https://github.com/user-attachments/assets/debd0895-0b7b-4a45-aa3a-b19330ef10c2)
![image](https://github.com/user-attachments/assets/c3851a3d-59f1-4e74-8d05-bdb75eebf024)
![image](https://github.com/user-attachments/assets/d907c6c6-ffbf-4690-b8eb-87192bb2e198)
![image](https://github.com/user-attachments/assets/f863a623-696b-4008-9c02-1e2fdbc5e6e0)



[link]: https://learn.microsoft.com/en-us/dotnet/core/install/linux
