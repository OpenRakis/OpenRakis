# OpenRakis

This is a WIP project aiming at reversing Cryo's DUNE game.

## Project structure

* *bin/floppy* :  PC DOS floppy version files (LOGO.HNM, BAGDAD.HSQ, DUNE.EXE, LOGO.EXE, ...)
* *bin/cd* : PC DOS CD version files (DUNE.DAT, DNCDPRG.EXE, DUNE.EXE)
* tools : a set of tools from various sources
* src : the resulting source code
* asm : the original x86 DOS assembly

the bin folder is not part of this repo, as it is copyrighted material.

# Tools

[IDA Freeware](https://www.scummvm.org/news/20180331/)

[MASM/TASM](https://github.com/xsro/masm-tasm) ([VSCode marketplace](https://marketplace.visualstudio.com/items?itemName=xsro.masm-tasm))

[MASM2C](https://github.com/xor2003/masm2c)

[semblance](https://github.com/zfigura/semblance)

[LZEXE 0.91](https://bellard.org/lzexe.html) (to uncompress LOGO.EXE)

[CiCParser2021](https://github.com/gabonator/Projects/tree/master/XenonResurrection/Parser/CicParser2021)

[DOSBox Debugger with AUTOEXEC support](https://www.vogons.org/viewtopic.php?p=860536#p860536)

# To be looked into

[8080JIT](https://github.com/DaveTCode/8080JIT)

[reko decompiler](https://github.com/uxmal/reko)

# works by other people

[madmoose/dune](https://github.com/madmoose/dune) (Uses SCUMMVM, and can decode videos)

[Save editors and other resources](https://sites.google.com/site/duneeditor/home)

[scummvm-cryo](https://github.com/elyosh/scummvm-cryo)

[Dune "HERAD" Ad Lib Music Hacking thread](https://www.vogons.org/viewtopic.php?t=49813)

[Port of the French Mega CD dub over to the PC CD DOS version](https://www.abandonware-forums.org/forum/autres/les-aventuriers-de-la-traduction-perdue/764167-dune-cd/page15#post804135)

[HERAD music format description](http://www.vgmpf.com/Wiki/index.php/HERAD)

[Partial data files description (CD version)](https://bigs.fr/dune_old/)

[HNM video format description](https://wiki.multimedia.cx/index.php?title=HNM_%281%29)

[HERAD implementation in adplug](https://github.com/adplug/adplug/blob/master/src/herad.cpp)

[DUNE revival thread](https://forum.dune2k.com/topic/17217-rewriting-cryos-dune-1-it-seems-possible/page/13/)

[DUNE revival project](https://sourceforge.net/p/dunerevival/code/HEAD/tree/) ([GitHub copy](https://github.com/sonicpp/dunerevival-code)) (uses [SCUMMVM](https://www.scummvm.org/))

[DUNE game translations](https://github.com/sonicpp/Dune-game-translations)

[HERAD implementation in MIDIPLEX](https://github.com/stascorp/MIDIPLEX)

# IDA Settings

8086 16-bit Real Mode.
Entry point (CS initial value) = 1ED.

# LICENSE

GNU GPLv3 License
