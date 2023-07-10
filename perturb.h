#ifndef _PERTURB_H_
#define _PERTURB_H_

typedef struct S S;

typedef S* (*constructor_fn) (int32_t i, int32_t f);

typedef void (*accessor_fn)(void *self, int32_t amt);

int32_t
perturb_it (accessor_fn f, int32_t amt);

S*
construct_it (constructor_fn ctor);

#endif /*_PERTURB_H_*/
