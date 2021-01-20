# mips-simulator
MIPS simulator in C#

Features:
- Parser combinator using FParsec
- Basic instruction set including **integer** arithmetic, branch, jump
- Stack support

![factorial MIPS](demo.gif)


Specifications: 
- supported system calls: 1, 4, 5, 10
- supported directives:
  - .ascii
  - .asciiz
  - .text
  - .data
  - .word
- supported instructions:
  - add
  - addi
  - sub
  - mul
  - div
  - b
  - beq
  - bne
  - blt
  - ble
  - bgt
  - bge
  - beqz
  - bneq
  - blez
  - bgez
  - sll
  - srl
  - j
  - jr
  - jal
  - lw
  - sw
  - move
  - li
  - la

TODO:
- static memory support
- semantic analysis
- pretty printer

Notes:
- I included a factorial MIPS example so demonstrate the recursion support thanks to stack
