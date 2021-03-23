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

#ifndef DUNE_GRAPHICS_H
#define DUNE_GRAPHICS_H

#include "common/endian.h"

namespace Common {
	class ReadStream;
}

namespace Dune {

class Graphics {
	int _global_y_offset;

public:
	Graphics();
	void setGlobalYOffset(int y_offset);
	void blitGraphics(Common::ReadStream *r, byte *frameBuffer, int x_offset, int y_offset, int w, int h, byte flags, byte mode);
	void blitGraphics(byte *buf, int buf_len, byte *frameBuffer, int x_offset, int y_offset, int w, int h, byte flags, byte mode);
};

} // End of namespace Dune

#endif
