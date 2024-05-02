# FMinus_interpreter

## Directory Structure of FMinus

Skeleton code structure under src is same as before

- AST.fs Syntax definition of the F language
- FMinus.fs You have to implement the semantics here
- Types.fs Type definitions needed for semantics
- Main.fs Main driver code of the interpreter
- Lexer.fsl , Parser.fsy : Parser (you don’t have to care)

## F- Language Syntax

![Untitled](https://github.com/juhynl/programming-language/blob/feat-fminus/Lab3/assets/Untitled.png)

## F- Language Semantics

| **true** | **false** 

| x 

| -E 

| E + E | E - E 

| E < E | E > E

![Untitled](https://github.com/juhynl/programming-language/blob/feat-fminus/Lab3/assets/Untitled%201.png)

| E == E

| E != E

![Untitled](https://github.com/juhynl/programming-language/blob/feat-fminus/Lab3/assets/Untitled%202.png)

| **if** E **then** E **else** E

| **let** x = E **in** E

| **let** f x = E **in** E

| **let** **rec** f x = E **in** E

| **fun** x → E

![Untitled](https://github.com/juhynl/programming-language/blob/feat-fminus/Lab3/assets/Untitled%203.png)

| E E

![Untitled](https://github.com/juhynl/programming-language/blob/feat-fminus/Lab3/assets/Untitled%204.png)
