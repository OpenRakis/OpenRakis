![Linux](https://img.shields.io/badge/-Linux-grey?logo=linux)
![OSX](https://img.shields.io/badge/-OSX-black?logo=apple)
![Windows](https://img.shields.io/badge/-Windows-red?logo=windows)

<div style="height: 25px; width: 160px; border: 1px solid red; white-space: nowrap; text-align: center; margin: 1em 0;">
    <span style="display: inline-block; height: 100%; vertical-align: middle;"></span><img src="worm.gif" style="vertical-align: middle; max-height: 400px; max-width: 640px;" height="400" />
</div>

# OpenRakis

This is a WIP project aiming at reversing Cryo's DUNE game.

## Project structure

* *bin/floppy* :  PC DOS floppy version files (LOGO.HNM, BAGDAD.HSQ, DUNE.EXE, LOGO.EXE, ...)
* *bin/cd* : PC DOS CD version files (DUNE.DAT, DNCDPRG.EXE, DUNE.EXE)
* tools : a set of tools from various sources
* [bluegr/scummvm](https://github.com/bluegr/scummvm/tree/dune/engines/dune) : the resulting ScummVM engine (which is the work of multiple people)
* asm : the original x86 DOS assembly

the bin folder is not part of this repo, as it is copyrighted material.

# Tools

[IDA Freeware](https://www.scummvm.org/news/20180331/)

[Ghidra](https://github.com/NationalSecurityAgency/ghidra)

[UNLZEXE](https://keenwiki.shikadi.net/wiki/UNLZEXE) (to unpack LOGO.EXE, if it is packed)

[DOSBox Debugger with AUTOEXEC support](https://www.vogons.org/viewtopic.php?p=860536#p860536)

[Chani: Simulfow Debugger](https://github.com/madmoose/chani)

[Spice86: Reverse Engineering Emulator](https://github.com/OpenRakis/Spice86)

[Ghidra code generator](https://github.com/OpenRakis/Spice86/tree/master/ghidraCodeGenerator)

[Cryogenic: Dune ASM code overriden with C# code, bit by bit](https://github.com/OpenRakis/Cryogenic)

# Other interesting tools

[dosdebug](https://github.com/johndoe31415/dosdebug)

[CiCParser2021](https://github.com/gabonator/Projects/tree/master/XenonResurrection/Parser/CicParser2021)

[semblance](https://github.com/zfigura/semblance)

[MASM2C](https://github.com/xor2003/masm2c)

[MASM/TASM](https://github.com/xsro/masm-tasm) ([VSCode marketplace](https://marketplace.visualstudio.com/items?itemName=xsro.masm-tasm))

# Existing efforts

### Engines

[madmoose/dune](https://github.com/madmoose/dune) (Uses SCUMMVM, and can decode videos)

[madmoose/dune-disassembly](https://github.com/madmoose/dune-disassembly): Disassembly work on the PC CD DOS version.

[scummvm/cryo](https://github.com/scummvm/scummvm/tree/master/engines/cryo) (for Lost Eden, a very similar game from Cryo)

[scummvm-cryo](https://github.com/elyosh/scummvm-cryo) ([Scummvm Wiki page](https://wiki.scummvm.org/index.php?title=Dune))

[DUNE revival project](https://sourceforge.net/p/dunerevival/code/HEAD/tree/) ([GitHub copy](https://github.com/sonicpp/dunerevival-code)) (uses [SCUMMVM](https://www.scummvm.org/) too)

### Resources

[Save editors and other resources](https://sites.google.com/site/duneeditor/home)

[Dune "HERAD" Ad Lib Music Hacking thread](https://www.vogons.org/viewtopic.php?t=49813)

[Port of the French Mega CD dub over to the PC CD DOS version](https://www.abandonware-forums.org/forum/autres/les-aventuriers-de-la-traduction-perdue/764167-dune-cd/page15#post804135)

[HERAD music format description](http://www.vgmpf.com/Wiki/index.php/HERAD)

[Partial data files description (CD version)](https://bigs.fr/dune_old/)

[HNM video format description](https://wiki.multimedia.cx/index.php?title=HNM_%281%29)

[HERAD implementation in adplug](https://github.com/adplug/adplug/blob/master/src/herad.cpp)

[DUNE revival thread](https://forum.dune2k.com/topic/17217-rewriting-cryos-dune-1-it-seems-possible/page/13/)

[DUNE game translations](https://github.com/sonicpp/Dune-game-translations)

[HERAD implementation in MIDIPLEX](https://github.com/stascorp/MIDIPLEX)

# IDA Settings and How to

8086 16-bit Real Mode.
Entry point (CS initial value) = 1ED.

DOSBox Debugger AUTOEXEC example:


```
mount C C:\DUNE_CD\C
imgmount D "C:\DUNE_CD\CD\Dune.iso" -t iso
D:
Z:\DEBUG.COM DNCDPRG.EXE ENG ADP220 SBP2227 EMS 386 SAF WRIC:\DUNECD\
exit
```


[ScummVM Wiki : HOWTO-Reverse Engineering](https://wiki.scummvm.org/index.php?title=HOWTO-Reverse_Engineering)

# LICENSE

GNU GPLv2 License
