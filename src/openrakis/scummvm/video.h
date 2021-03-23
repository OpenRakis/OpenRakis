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

#ifndef DUNE_VIDEO_H
#define DUNE_VIDEO_H

#include "common/array.h"
#include "common/stream.h"

namespace Audio {
	class QueuingAudioStream;
}

namespace Dune {

class DuneEngine;

class HnmPlayer {
public:
	Common::SeekableReadStream *_reader;
	bool   _done;
	uint16 _headerSize;
	int    _currentFrame;
	int    _frameCount;
	Common::Array<uint32> _frameOffsets;

	Audio::QueuingAudioStream *_audioQueue;
	bool                       _audioIsStarted;

	struct {
		int  width;
		int  height;
		byte flags;
		byte mode;
	} _frameHeader;

	byte *_frameBuffer;

	int   _decodeBufferSize;
	byte *_decodeBuffer;

	bool  _interlace;

	const int16                *_subtitleFrames;
	int                         _subtitleCurrentPart;
	Common::SeekableReadStream *_subtitleResource;

private:
	DuneEngine *_vm;

public:
	HnmPlayer(DuneEngine *vm);
	~HnmPlayer();

	void setReader(Common::SeekableReadStream *reader);

	void setSubtitles(const int16 *subtitleFrames, Common::SeekableReadStream *subtitleResource) {
		_subtitleFrames = subtitleFrames;
		_subtitleCurrentPart = 0;
		_subtitleResource = subtitleResource;
	}

	void setInterlace(bool interlace = true) {
		_interlace = interlace;
	}

	bool done() { return _done; };

	void start();

	void applyPaletteBlock(Common::ReadStream *reader);
	void decodeAVFrame();
	void decodeAVFrameChunks();
	void decodeVideoFrame();
};

} // End of namespace Dune

#endif
