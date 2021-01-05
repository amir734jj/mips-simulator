    .text
amir: .asciiz "Hello world"
    .code
move $t0, $t0   ;
li $t0 555      ;
la $v0, amir    ;
syscall         ;
