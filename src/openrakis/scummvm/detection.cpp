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

#include "engines/advancedDetector.h"

#include "common/savefile.h"
#include "common/system.h"
#include "common/util.h"

#include "base/plugins.h"

#include "dune/dune.h"

static const PlainGameDescriptor cryoGames[] = {
	{ "dune", "Dune" },
	{ 0, 0 }
};


namespace Dune {

static const ADGameDescription gameDescriptions[] = {
	// CD version
	{
		"dune",
		"CD",
		{
			{"dune.dat", 0, "f096565944ab48cf4cb6cbf389384e6f", 397794384},
			{0,0,0,0}
		},
		Common::EN_ANY,
		Common::kPlatformDOS,
		ADGF_CD,
		GUIO0()
	},

	AD_TABLE_END_MARKER
};

} // End of namespace Dune

class DuneMetaEngine : public AdvancedMetaEngine {
public:
	//OLD STYLE DuneMetaEngine() : AdvancedMetaEngine(detectionParams) {}
	//NEW STYLE
	DuneMetaEngine() : AdvancedMetaEngine(Dune::gameDescriptions, sizeof(ADGameDescription), cryoGames) {}

	virtual const char *getName() const {
		return "Dune Engine";
	}

	virtual const char *getOriginalCopyright() const {
		return "Dune Engine (C) Cryo Interactive Entertainment";
	}

	virtual bool hasFeature(MetaEngineFeature f) const;
	virtual bool createInstance(OSystem *syst, Engine **engine, const ADGameDescription *desc) const;
};

bool DuneMetaEngine::hasFeature(MetaEngineFeature f) const {
	return
		false;
}

bool Dune::DuneEngine::hasFeature(EngineFeature f) const {
	return
		(f == kSupportsRTL);
}

bool DuneMetaEngine::createInstance(OSystem *syst, Engine **engine, const ADGameDescription *desc) const {
	if (desc) {
		*engine = new Dune::DuneEngine(syst, desc);
	}
	return desc != 0;
}

#if PLUGIN_ENABLED_DYNAMIC(DUNE)
	REGISTER_PLUGIN_DYNAMIC(DUNE, PLUGIN_TYPE_ENGINE, DuneMetaEngine);
#else
	REGISTER_PLUGIN_STATIC(DUNE, PLUGIN_TYPE_ENGINE, DuneMetaEngine);
#endif
