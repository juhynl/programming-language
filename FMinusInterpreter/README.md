# CMinusPtrInterpreter
This directory contains a interpreter for C- language.
## Usage
Run interpreter.py.
```
python3 interpreter.py
```
Enter ":start" to run the program.

Enter ":exit" to exit the program.
```
============================================================
FMinus Interpreter
============================================================
Building the program. Please wait...
Done!
============================================================
Please enter a target program:
         - Enter ":start" to run the program.
         - Enter ":exit" to exit the program.
============================================================
let rec sum n =
  if n < 0 then 0 else sum (n - 1) + n
in
sum 10
:start
------------------------------------------------------------
result: 55
============================================================
Please enter a target program:
         - Enter ":start" to run the program.
         - Enter ":exit" to exit the program.
============================================================
:exit
```
interpreter.py saves interpreted programs and results in the 'log' directory.

## Semantics
![image](https://github.com/user-attachments/assets/5996fc5f-c074-480b-babb-f8488454724c)
![image](https://github.com/user-attachments/assets/83c15e09-40c0-439e-8dfd-5ac776ebb728)
![image](https://github.com/user-attachments/assets/cdf9c6ea-3025-45cd-92bf-3bf99b3deb79)
![image](https://github.com/user-attachments/assets/c15b27d5-28a8-4167-9e99-b27f0ddcf4a5)



[link]: https://learn.microsoft.com/en-us/dotnet/core/install/linux
