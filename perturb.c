#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>

#include "perturb.h"

typedef struct PrivateS
{
	int32_t i;
} PrivateS;

int32_t
perturb_it (accessor_fn f, int32_t amt)
{
	printf ("perturb_it called; f: %p, amt: %d\n", f, amt);
	f ((void*)0, amt);
	return 0;
}

S*
construct_it (constructor_fn ctor)
{
	printf ("construct_it called; ctor: %p\n", ctor);
	S *res = ctor(123, 456);
	printf ("from C: i = %d\n", ((PrivateS*)res)->i);
	return res;
}
