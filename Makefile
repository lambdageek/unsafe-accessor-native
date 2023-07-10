
.PHONY: all clean

LIBRARY=libperturb.dylib

all: $(LIBRARY)

$(LIBRARY): perturb.c perturb.h
	clang -o $@ -Wall -shared -O -g -std=c99 $<

clean:
	-rm -f $(LIBRARY)
