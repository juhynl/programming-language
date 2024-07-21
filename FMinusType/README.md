# FMinusType
This directory contains a program that provides a type system for F- language.
## Prerequisites
* .NET is required
* Python >= 3.8
  
If you want to install .NET on Linux, please refer to the following [link]. 

## Usage
Run type_system.py.
```
python3 type_system.py
```
First, input the program code which you want to perform type inference. 

Then enter ":start" to execute the type inference. 

To exit the type inference program, enter ":exit".
```
============================================================
FMinus Type Inference
============================================================
Building the program. Please wait...
Done!
============================================================
Please enter a target program:
         - Enter ":start" to perform type inference.
         - Enter ":exit" to exit the program.
============================================================
fun x -> (fun y -> x + y)
:start
------------------------------------------------------------
result: (int) -> ((int) -> (int))
============================================================
Please enter a target program:
         - Enter ":start" to perform type inference.
         - Enter ":exit" to exit the program.
============================================================
:exit
```
FMinusType.py saves inferred programs and results in the 'log' directory.

## Typing Rules
![image](https://github.com/juhynl/programming-language/assets/101332606/44b97b7c-6e4e-4b44-a1a3-bd3c204648ab)
![image](https://github.com/juhynl/programming-language/assets/101332606/9f3428cd-760b-4919-aefa-d173fdd22be7)


[link]: https://learn.microsoft.com/en-us/dotnet/core/install/linux
