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

#ifndef DUNE_H
#define DUNE_H

#include "dune/archive.h"
#include "dune/graphics.h"

#include "common/random.h"
#include "engines/advancedDetector.h"
#include "engines/engine.h"
#include "gui/debugger.h"

namespace Dune {

class HnmPlayer;

enum HNMVideos {
	HNM_DFL2,
	HNM_MNT1,
	HNM_MNT2,
	HNM_MNT3,
	HNM_MNT4,
	HNM_SIET,
	HNM_PALACE,
	HNM_PALACE2,
	HNM_FORT,
	HNM_FORT2,
	HNM_DEAD3,
	HNM_DEAD,
	HNM_DEAD2,
	HNM_VER,
	HNM_TITLE,
	HNM_MTG1,
	HNM_MTG2,
	HNM_MTG3,
	HNM_PLANT,
	HNM_CREDITS,
	HNM_VIRGIN,
	HNM_CRYO,
	HNM_CRYO2,
	HNM_PRESENT,
	HNM_IRULAN,
	HNM_SEQA,
	HNM_SEQL,
	HNM_SEQM,
	HNM_SEQP,
	HNM_SEQG,
	HNM_SEQJ,
	HNM_SEQK,
	HNM_SEQI,
	HNM_SEQD,
	HNM_SEQN,
	HNM_SEQR
};

class DuneEngine : public Engine {
public:
	DuneEngine(OSystem *syst, const ADGameDescription *gameDesc);
	~DuneEngine();

	virtual Common::Error run();
	virtual bool hasFeature(EngineFeature f) const;

	bool isCD();

	int _timerTicks;
	Graphics _graphics;

private:
	Common::RandomSource *_rnd;
	const ADGameDescription *_gameDescription;

	Archive _archive;
	void dumpResource(const char *filename);

	HnmPlayer *_video;
	void playVideo(HNMVideos videoId);
};


} // End of namespace Dune

#endif
