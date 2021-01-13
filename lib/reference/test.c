#include <stdint.h>
#include <stdio.h>

#include "xoshiro256starstar.c"


void main() {
    s[0] = 2; s[1] = 3; s[2] = 5; s[3] = 7;

    for (int i = 0; i < 20; i++) {
        printf("0x%016lx\n", next());
    }

    for (int i = 0; i < 1000000; i++) {
        next();
    }
    printf("0x%016lx\n", next());
}
