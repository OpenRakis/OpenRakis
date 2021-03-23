MODULE := engines/dune

MODULE_OBJS = \
	archive.o \
	detection.o \
	dune.o \
	graphics.o \
	statics.o \
	video.o

MODULE_DIRS += \
	engines/dune

# This module can be built as a plugin
ifeq ($(ENABLE_DUNE), DYNAMIC_PLUGIN)
PLUGIN := 1
endif

# Include common rules
include $(srcdir)/rules.mk
