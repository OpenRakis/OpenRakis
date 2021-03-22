# PALib Makefile by Scognito and Tom

#-------------------------------------------------------
# Please uncomment only one "ARM7_SELECTED" line.
# If unsure, uncomment "ARM7_SELECTED = ARM7_BASIC"
# If you also use EFS uncomment "USE_EFS = YES" too.
# Enjoy!
#-------------------------------------------------------

#ARM7_SELECTED = ARM7_BASIC
#ARM7_SELECTED = ARM7_MOD_DSWIFI
#ARM7_SELECTED = ARM7_MOD_LIBLOBBY
ARM7_SELECTED = ARM7_MP3_DSWIFI
#ARM7_SELECTED = ARM7_MP3_LIBLOBBY
#ARM7_SELECTED = ARM7_MIKMOD_DSWIFI
#USE_EFS = YES

PROGNAME = PAlib
OFILES   +=
ADD_LIBS +=

PATH 		:= $(DEVKITARM)/bin:$(PATH)

TEXT1 		:= PAlib Project -
TEXT2 		:= Change this text
TEXT3 		:= in your Makefile!
ICON 		:= -b $(CURDIR)/../logo.bmp

#---------------------------------------------------------------------------------
.SUFFIXES:
#---------------------------------------------------------------------------------
ifeq ($(strip $(DEVKITARM)),)
$(error "Please set DEVKITARM in your environment. export DEVKITARM=<path to>devkitARM)
endif

include $(DEVKITARM)/ds_rules


#---------------------------------------------------------------------------------
# TARGET is the name of the output, if this ends with _mb generates a multiboot image
# BUILD is the directory where object files & intermediate files will be placed
# SOURCES is a list of directories containing source code
# INCLUDES is a list of directories containing extra header files
#---------------------------------------------------------------------------------
TARGET	:=	$(shell basename $(CURDIR))
BUILD		:=	build
SOURCES	:=	gfx source data
INCLUDES	:=	include build data

EXPORT_DIR := /c/ndsexamples/
#---------------------------------------------------------------------------------
# ARM7BIN is the path to an arm7 binary other than the default
#	usage: ARM7BIN := -7 binaryName.bin
#
# ICON is the path to an icon to be used int the header plus text
#	usage: ICON := -t iconName.bmp "text line one; text line 2; text line 3"
# 
#---------------------------------------------------------------------------------

ifeq ($(strip $(ARM7_SELECTED)), ARM7_BASIC)
	ARM7BIN		:= -7 $(PAPATH)/lib/arm7/arm7.bin
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MOD_DSWIFI)
	ARM7BIN		:= -7 $(PAPATH)/lib/arm7_mod_dswifi/arm7.bin
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MOD_LIBLOBBY)
	ARM7BIN		:= -7 $(PAPATH)/lib/arm7_mod_liblobby/arm7.bin
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MP3_DSWIFI)
	ARM7BIN		:= -7 $(PAPATH)/lib/arm7_mp3_dswifi/arm7.bin
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MP3_LIBLOBBY)
	ARM7BIN		:= -7 $(PAPATH)/lib/arm7_mp3_liblobby/arm7.bin
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MIKMOD_DSWIFI)
	ARM7BIN		:= -7 $(PAPATH)/lib/arm7_mikmod_dswifi/arm7.bin
endif

#---------------------------------------------------------------------------------
# options for code generation
#---------------------------------------------------------------------------------
ARCH	:=	-mthumb-interwork

# note: arm9tdmi isn't the correct CPU arch, but anything newer and LD
# *insists* it has a FPU or VFP, and it won't take no for an answer!
CFLAGS	:=	-g  -Wformat=2 -Winline -Wall -O2\

 		-mcpu=arm946e-s -mtune=arm946e-s -fomit-frame-pointer\
		-ffast-math \
		$(ARCH)

CFLAGS	+=	$(INCLUDE) -DARM9 -I$(DEVKITPRO)/PAlib/include/nds

ASFLAGS	:=	-g $(ARCH)
LDFLAGS	:=	-g $(ARCH) -mno-fpu -L$(DEVKITPRO)/PAlib/lib -Wl,--gc-sections

#---------------------------------------------------------------------------------
# path to tools - this can be deleted if you set the path in windows
#---------------------------------------------------------------------------------
# export PATH		:=	/d/dev/ds/devkitARM_r11/bin:/bin
 
#---------------------------------------------------------------------------------
# PATH to ndslib - just make a system variable called NDSLIBPATH and be done with it
#---------------------------------------------------------------------------------
# NDSLIBPATH	:=	/d/dev/ds/ndslib/
 
#---------------------------------------------------------------------------------
# the prefix on the compiler executables
#---------------------------------------------------------------------------------
PREFIX			:=	arm-eabi-
#---------------------------------------------------------------------------------
# any extra libraries we wish to link with the project
#---------------------------------------------------------------------------------	

ifeq ($(strip $(ARM7_SELECTED)), ARM7_BASIC)
	LIBS	:= -lfat -lnds9 -ldswifi9
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MOD_DSWIFI)
	LIBS	:= -lfat -lnds9 -ldswifi9
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MOD_LIBLOBBY)
	LIBS	:= -lfat -lnds9 -llobby9d
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MP3_DSWIFI)
	LIBS	:= -lfat -lnds9 -ldswifi9
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MP3_LIBLOBBY)
	LIBS	:= -lfat -lnds9 -llobby9d
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MIKMOD_DSWIFI)
	LIBS	:= -lfat -lnds9 -ldswifi9 -lmikmod9
endif

LIBSPA	:= -lpa9
 
#---------------------------------------------------------------------------------
# list of directories containing libraries, this must be the top level containing
# include and lib
#---------------------------------------------------------------------------------
ifeq ($(strip $(ARM7_SELECTED)), ARM7_BASIC)
	LIBDIRS	:=	$(DEVKITPRO)/libnds
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MOD_DSWIFI)
	LIBDIRS	:=	$(DEVKITPRO)/libnds
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MOD_LIBLOBBY)
	LIBDIRS	:=	$(DEVKITPRO)/libnds $(DEVKITPRO)/liblobby
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MP3_DSWIFI)
	LIBDIRS	:=	$(DEVKITPRO)/libnds
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MP3_LIBLOBBY)
	LIBDIRS	:=	$(DEVKITPRO)/libnds $(DEVKITPRO)/liblobby
endif
ifeq ($(strip $(ARM7_SELECTED)), ARM7_MIKMOD_DSWIFI)
	LIBDIRS	:=	$(DEVKITPRO)/libnds $(DEVKITPRO)/mikmod
endif

LIBDIRPA	:=	$(PAPATH)
 
#---------------------------------------------------------------------------------
# no real need to edit anything past this point unless you need to add additional
# rules for different file extensions
#---------------------------------------------------------------------------------
ifneq ($(BUILD),$(notdir $(CURDIR)))
#---------------------------------------------------------------------------------
 
export OUTPUT	:=	$(CURDIR)/$(TARGET)
 
export VPATH	:=	$(foreach dir,$(SOURCES),$(CURDIR)/$(dir))
 
export CC		:=	$(PREFIX)gcc
export CXX		:=	$(PREFIX)g++
export AR		:=	$(PREFIX)ar
export OBJCOPY	:=	$(PREFIX)objcopy
#---------------------------------------------------------------------------------
# use CXX for linking C++ projects, CC for standard C
#---------------------------------------------------------------------------------
export LD		:=	$(CXX)
#export LD		:=	$(CC)
 
CFILES		:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.c)))
CPPFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.cpp)))
SFILES		:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.s)))
PCXFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.pcx)))
BINFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.bin)))
PNGFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.png)))
PALFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.pal)))
RAWFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.raw)))
MAPFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.map)))
JPEGFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.jpg)))
MODFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.mod)))
GIFFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.gif)))
BMPFILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.bmp)))
MP3FILES	:=	$(foreach dir,$(SOURCES),$(notdir $(wildcard $(dir)/*.mp3)))
 
export OFILES	:=	$(MAPFILES:.map=.o) $(RAWFILES:.raw=.o) $(PALFILES:.pal=.o) $(BINFILES:.bin=.o) $(PNGFILES:.png=.o) $(PCXFILES:.pcx=.o) $(JPEGFILES:.jpg=.o) $(MODFILES:.mod=.o) $(GIFFILES:.gif=.o) $(BMPFILES:.bmp=.o) $(MP3FILES:.mp3=.o)\
					$(CPPFILES:.cpp=.o) $(CFILES:.c=.o) $(SFILES:.s=.o)
 
export INCLUDE	:=	$(foreach dir,$(INCLUDES),-I$(CURDIR)/$(dir)) \
					$(foreach dir,$(LIBDIRS),-I$(dir)/include) \
					$(foreach dir,$(LIBDIRS),-I$(dir)/include/nds) \
					-I$(PAPATH)/include/nds\
					-I$(CURDIR)/$(BUILD)
 
export LIBPATHS	:=	$(foreach dir,$(LIBDIRS),-L$(dir)/lib)
export LIBPATHPA	:=	$(foreach dir,$(LIBDIRPA),-L$(dir)/lib)
 
.PHONY: $(BUILD) clean export
 
#---------------------------------------------------------------------------------
$(BUILD):	
	@[ -d $@ ] || mkdir -p $@
	@make --no-print-directory -C $(BUILD) -f $(CURDIR)/Makefile
 
#---------------------------------------------------------------------------------
clean:
	@echo clean ...$(TARGET)
	@rm -fr $(BUILD) *.elf *.*ds*
 
export:
	@echo exporting ...$(TARGET)
	@cp *.nds $(EXPORT_DIR)/$(TARGET).nds

#---------------------------------------------------------------------------------
else
 
DEPENDS	:=	$(OFILES:.o=.d)
 
#---------------------------------------------------------------------------------
# main targets
#---------------------------------------------------------------------------------
$(OUTPUT).ds.gba	: 	$(OUTPUT).nds

$(OUTPUT).nds	: 	$(OUTPUT).bin

$(OUTPUT).bin	:	$(OUTPUT).elf
 
$(OUTPUT).elf	:	$(OFILES)
 
#---------------------------------------------------------------------------------
%.ds.gba: %.nds
	@echo built ... $(notdir $@)
	@dsbuild $< 
ifeq ($(strip $(USE_EFS)), YES)
	@$(CURDIR)/../efs $(OUTPUT).ds.gba
endif

#---------------------------------------------------------------------------------
%.nds: %.bin
ifeq ($(strip $(USE_EFS)), YES)
	@ndstool -c $@ -9 $(TARGET).bin $(ARM7BIN) $(LOGO) $(ICON) "$(TEXT1);$(TEXT2);$(TEXT3)" -d ../efsroot
	@$(CURDIR)/../efs $(OUTPUT).nds
else
	@ndstool -c $@ -9 $(TARGET).bin $(ARM7BIN) $(LOGO) $(ICON) "$(TEXT1);$(TEXT2);$(TEXT3)"
endif

#---------------------------------------------------------------------------------
%.bin: %.elf
	
	@$(OBJCOPY) -O binary $(TARGET).elf $(TARGET).bin
 
#---------------------------------------------------------------------------------
%.elf:
	@echo $(LD)  $(LDFLAGS) -specs=ds_arm9.specs $(OFILES) $(LIBPATHPA) $(LIBSPA) $(LIBPATHS) $(LIBS) -o $(TARGET).elf
	@$(LD)  $(LDFLAGS) -specs=ds_arm9.specs $(OFILES) $(LIBPATHPA) $(LIBSPA) $(LIBPATHS) $(LIBS) -o $(TARGET).elf
 
 
 
#---------------------------------------------------------------------------------
# Compile Targets for C/C++
#---------------------------------------------------------------------------------
 
#---------------------------------------------------------------------------------
%.o : %.cpp
	@echo $(notdir $<)
	@$(CXX) -MM $(CFLAGS) -o $*.d $<
	@$(CXX) $(CFLAGS) -c $< -o$@
 
#---------------------------------------------------------------------------------
%.o : %.c
	@echo $(notdir $<)
	@$(CC) -MM $(CFLAGS) -o $*.d $<
	@$(CC)  $(CFLAGS) -c $< -o$@
 
#---------------------------------------------------------------------------------
%.o : %.s
	@echo $(notdir $<)
	@$(CC) -MM $(CFLAGS) -o $*.d $<
	@$(CC)  $(ASFLAGS) -c $< -o$@


# bin2o macro like that from libnds, slightly changed for the PALib naming conventions of resources which doesn't append the file extension to the resource name
define bin2o
	cp $(<) $(*)
	bin2s $(*) | $(AS) $(ARCH) -o $(@)
	rm $(*)
	
	echo "extern const u8" $(*)"[];" > $(*).h
	echo "extern const u32" $(*)_size";" >> $(*).h
endef
 
#---------------------------------------------------------------------------------
%.o	:	%.mp3
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)
 
#---------------------------------------------------------------------------------
%.o	:	%.pcx
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)
 
#---------------------------------------------------------------------------------
%.o	:	%.bin
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

#---------------------------------------------------------------------------------
%.o	:	%.png
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)
	
#---------------------------------------------------------------------------------
%.o	:	%.raw
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)
 
#---------------------------------------------------------------------------------
%.o	:	%.pal
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)
 
#---------------------------------------------------------------------------------
%.o	:	%.map
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

#---------------------------------------------------------------------------------
%.o	:	%.mdl
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

#---------------------------------------------------------------------------------
%.o	:	%.jpg
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

#---------------------------------------------------------------------------------
%.o	:	%.mod
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

#---------------------------------------------------------------------------------
%.o	:	%.gif
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

#---------------------------------------------------------------------------------
%.o	:	%.bmp
#---------------------------------------------------------------------------------
	@echo $(notdir $<)
	@$(bin2o)

 
-include $(DEPENDS) 
#---------------------------------------------------------------------------------------
endif
#---------------------------------------------------------------------------------------
