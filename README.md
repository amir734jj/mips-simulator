# mips-simulator
MIPS simulator in C#

Features:
- Parser combinator using FParsec

TODO:
- Runtime stack bases simulator

```Assembly
    .text
str:   .asciiz "number is: "
    .code
main:
    li $t0, 3       # put 3 into register $t0
    
    li $v0, 4       ;
    la $a0, str     ;
    syscall         ;
    
    li $v0, 1       ;
    move $a0, $t0   ;
    syscall         ;
    
    li $v0 10       ;
    syscall         ;

```
