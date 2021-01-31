.PHONY: all clean distclean dist debug release package test

all: release

clean:
	@./Make.sh clean

distclean: clean
	@./Make.sh distclean

dist:
	@./Make.sh dist

debug:
	@./Make.sh debug

release:
	@./Make.sh release

package: release
	@./Make.sh package

test:
	@./Make.sh test
