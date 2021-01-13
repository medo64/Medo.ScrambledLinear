.PHONY: all clean distclean dist debug release test

ifeq ($(PREFIX),)
    PREFIX := /usr/local/
endif


DIST_NAME := xoshiro
DIST_VERSION := 0.0.1

DEB_BUILD_ARCH := all


SOURCE_LIST := Makefile CONTRIBUTING.md LICENSE.md README.md docs/ src/


EXITCODE_HAS_DOTNET := $(shell which dotnet >/dev/null ; echo $$?)
EXITCODE_HAS_UNCOMMITTED := $(shell git status -s 2>/dev/null | wc -l)


all: release


clean:
	-@$(RM) -r bin/
	-@$(RM) -r build/
	-@$(RM) -r src/**/bin
	-@$(RM) -r src/**/obj

distclean: clean
	-@$(RM) -r dist/
	-@$(RM) -r target/


dist:
	$(if $(findstring 0,$(EXITCODE_HAS_UNCOMMITTED)),,$(warning Uncommitted changes present))
	@$(RM) -r build/dist/
	@mkdir -p build/dist/$(DIST_NAME)-$(DIST_VERSION)/
	@cp -r $(SOURCE_LIST) build/dist/$(DIST_NAME)-$(DIST_VERSION)/
	@cd build/dist/$(DIST_NAME)-$(DIST_VERSION)/
	-@find build/dist/$(DIST_NAME)-$(DIST_VERSION)/src/ -name ".vs" -type d -exec rm -rf {} \; 2>/dev/null
	-@find build/dist/$(DIST_NAME)-$(DIST_VERSION)/src/ -name "bin" -type d -exec rm -rf {} \; 2>/dev/null
	-@find build/dist/$(DIST_NAME)-$(DIST_VERSION)/src/ -name "obj" -type d -exec rm -rf {} \; 2>/dev/null
	@tar -cz -C build/dist/ --owner=0 --group=0 -f build/dist/$(DIST_NAME)-$(DIST_VERSION).tar.gz $(DIST_NAME)-$(DIST_VERSION)/
	@mkdir -p dist/
	@mv build/dist/$(DIST_NAME)-$(DIST_VERSION).tar.gz dist/
	@echo Output at dist/$(DIST_NAME)-$(DIST_VERSION).tar.gz


debug: src/Xoshiro.sln
	$(if $(findstring 0,$(EXITCODE_HAS_DOTNET)),,$(error No 'dotnet' in path, consider installing .NET 5.0 SDK))
	@mkdir -p bin/
	@mkdir -p build/debug/
	@dotnet build --configuration Debug --output build/debug --verbosity Normal src/Xoshiro.sln
	@cp build/release/Xoshiro.dll bin/
	@cp build/release/Xoshiro.pdb bin/

release: src/Xoshiro.sln
	$(if $(findstring 0,$(EXITCODE_HAS_DOTNET)),,$(error No 'dotnet' in path, consider installing .NET 5.0 SDK))
	$(if $(findstring 0,$(EXITCODE_HAS_UNCOMMITTED)),,$(warning Uncommitted changes present))
	@mkdir -p bin/
	@mkdir -p build/release/
	@dotnet build --configuration Release --output build/release --verbosity Normal src/Xoshiro.sln
	@cp build/release/Xoshiro.dll bin/
	@cp build/release/Xoshiro.pdb bin/


test: src/Xoshiro.sln
	$(if $(findstring 0,$(EXITCODE_HAS_DOTNET)),,$(error No 'dotnet' in path, consider installing .NET 5.0 SDK))
	@mkdir -p build/test/
	@dotnet test --configuration Debug --output build/test --verbosity normal src/Xoshiro.sln
