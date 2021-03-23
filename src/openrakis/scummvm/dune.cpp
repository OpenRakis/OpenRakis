/* ScummVM - Graphic Adventure Engine
 *
 * ScummVM is the legal property of its developers, whose names
 * are too numerous to list here. Please refer to the COPYRIGHT
 * file distributed with this source distribution.
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 *
 */

#include "dune/dune.h"

#include "dune/hsq.h"
#include "dune/video.h"
#include "dune/statics.h"

#include "common/config-manager.h"
#include "common/debug-channels.h"
#include "common/debug.h"
#include "common/file.h"
#include "common/fs.h"
#include "common/scummsys.h"
#include "engines/util.h"
#include "graphics/palette.h"
#include "gui/EventRecorder.h"

namespace Dune {

DuneEngine::DuneEngine(OSystem *syst, const ADGameDescription *gameDesc)
	: Engine(syst), _gameDescription(gameDesc) {

	_timerTicks = 0;
	_rnd = new Common::RandomSource("dune_randomseed");
}

DuneEngine::~DuneEngine() {
	delete _rnd;
	DebugMan.clearAllDebugChannels();
}

Common::Error DuneEngine::run() {
	initGraphics(320, 200);

	if (!_archive.openArchive("DUNE.DAT")) {
		debug("Failed to open DUNE.DAT");
	}
	_video = new HnmPlayer(this);

	byte pal[3*256] = {0};
	for (int i = 0; i != 256; ++i) {
		pal[3*i+0] = pal[3*i+1] = pal[3*i+2] = i;
	}
	_system->getPaletteManager()->setPalette(pal, 0, 255);

	playVideo(HNM_VIRGIN);
	playVideo(HNM_CRYO);
	playVideo(HNM_CRYO2);
	playVideo(HNM_PRESENT);
	playVideo(HNM_FORT);
	playVideo(HNM_IRULAN);

	return Common::kNoError;
}

bool DuneEngine::isCD() {
	return _gameDescription->flags & ADGF_CD;
}

void DuneEngine::dumpResource(const char *filename) {
	Common::SeekableReadStream *r = _archive.openMember(filename);
	Common::DumpFile f;
	f.open(filename);

	int size = r->size();
	byte *buf = new byte[size];
	r->read(buf, size);

	f.write(buf, size);
	r->seek(0);
	delete[] buf;
}

void DuneEngine::playVideo(HNMVideos videoId) {
	const char *hnmFilenames[] = {
		"DFL2.HNM",    //  1
		"MNT1.HNM",    //  2
		"MNT2.HNM",    //  3
		"MNT3.HNM",    //  4
		"MNT4.HNM",    //  5
		"SIET.HNM",    //  6
		"PALACE.HNM",  //  7
		"PALACE.HNM",  //  8
		"FORT.HNM",    //  9
		"FORT.HNM",    // 10
		"DEAD3.HNM",   // 11
		"DEAD.HNM",    // 12
		"DEAD2.HNM",   // 13
		"VER.HNM",     // 14
		"TITLE.HNM",   // 15
		"MTG1.HNM",    // 16
		"MTG2.HNM",    // 17
		"MTG3.HNM",    // 18
		"PLANT.HNM",   // 19
		"CREDITS.HNM", // 20
		"VIRGIN.HNM",  // 21
		"CRYO.HNM",    // 22
		"CRYO2.HNM",   // 23
		"PRESENT.HNM", // 24
		"IRULAN.HNM",  // 25
		"SEQA.HNM",    // 26
		"SEQL.HNM",    // 27
		"SEQM.HNM",    // 28
		"SEQP.HNM",    // 29
		"SEQG.HNM",    // 30
		"SEQJ.HNM",    // 31
		"SEQK.HNM",    // 32
		"SEQI.HNM",    // 33
		"SEQD.HNM",    // 34
		"SEQN.HNM",    // 35
		"SEQR.HNM"     // 36
	};

	const char *filename = hnmFilenames[videoId];

	Common::SeekableReadStream *r = _archive.openMember(filename);

	_video->setReader(r);

	if (videoId == HNM_IRULAN) {
		_video->setSubtitles(Subs::irulan, _archive.openMember("IRUL1.HSQ"));
	}

	_video->setInterlace(videoId >= HNM_IRULAN);
	_video->start();

	Common::Event event;
	Common::EventManager *eventMan = _system->getEventManager();

	float nextFrameTime = _system->getMillis() + (1000.0/12.0);
	while (!_video->done() && !shouldQuit()) {
		_video->decodeAVFrame();

		_system->copyRectToScreen(_video->_frameBuffer, 320, 0, 0, 320, 200);
		_system->updateScreen();

		while (eventMan->pollEvent(event)) {
		}

		float now = _system->getMillis();
		if (now < nextFrameTime) {
			_system->delayMillis((int)floor(nextFrameTime - now));
		}
		nextFrameTime += (1000.0/12.0);
	}

}

} // End of namespace Dune
