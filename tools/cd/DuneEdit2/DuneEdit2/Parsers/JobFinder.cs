namespace DuneEdit2.Parsers
{
    public class JobFinder
    {
        public const string UnknownValue = "Unknown Job / Not used";

        public JobFinder()
        {
        }

        public static string GetJobDesc(byte id)
        {
            return id switch
            {
                0 => "Spice Mining",
                1 => "Spice prospecting",
                2 => "Waiting orders",
                3 => "Spice miners going to search equipment",
                4 => "Military Training",
                5 => "Espionage",
                6 => "Attacking",
                7 => "Military searching equipment",
                8 => "Irrigation & Tree care",
                9 => "Wind-trap assembly",
                10 => "Bulb growing",
                11 => "Ecologists going to search equipment",
                12 => "Harkonnen spice mining",
                13 => "Harkonnen prospecting",
                14 => "Harkonnen waiting orders",
                15 => "Harkonnen spice miners going to search equipment",
                16 => "Spice Mining Finished",
                17 => "Spice prospecting Finished",
                18 => "Waiting Orders Finished",
                19 => "Spice miners going to search equipment Finished",
                20 => "Military Training Finished",
                21 => "Espionage Finished",
                22 => "Attacking Finished",
                23 => "Military searching equipment Finished",
                24 => "Irrigation & Tree care Finished",
                25 => "Wind-trap assembly Finished",
                26 => "Bulb growing Finished",
                27 => "Ecologists going to search equipment Finished",
                28 => "Harkonnen spice mining Finished",
                29 => "Harkonnen prospecting Finished",
                30 => "Harkonnen waiting orders Finished",
                31 => "Harkonnen spice miners finished searching for equipment",
                32 => "Spice Mining - No More Orders to give",
                33 => "Troop prospecting captured - No More Orders to give",
                34 => "Troop apologizes for being captured",
                35 => "Spice miners going to search for a spice harvester - No more orders to give",
                64 or 65 or 66 or 67 or 68 or 69 or 70 or 71 or 72 or 73 or 74 or 75 or 76 or 77 or 78 or 79 or 80 or 81 or 82 or 83 or 84 or 85 or 86 or 87 or 88 or 89 or 90 or 91 or 92 or 93 or 94 or 95 or 96 or 97 or 98 or 99 or 100 or 101 or 102 or 103 or 104 or 105 or 106 or 107 or 108 or 109 or 110 or 111 or 112 or 113 or 114 or 115 or 116 or 117 or 118 or 119 or 120 or 121 or 122 or 123 or 124 or 125 or 126 or 127 => "Moving to another place",
                128 or 129 or 130 or 131 or 132 or 133 or 134 or 135 or 136 or 137 or 138 or 139 or 140 or 141 or 142 or 143 or 144 or 145 or 146 or 147 or 148 or 149 or 150 or 151 or 152 or 153 or 154 or 155 or 156 or 157 or 158 or 159 => "Not yet hired",
                160 or 161 or 162 or 163 or 164 or 165 or 166 or 167 or 168 or 169 or 170 or 171 or 172 or 173 or 174 or 175 or 176 or 177 or 178 or 179 or 180 or 181 or 182 or 183 or 184 or 185 or 186 or 187 or 188 or 189 or 190 or 191 or 192 or 193 or 194 or 195 or 196 or 197 or 198 or 199 or 200 or 201 or 202 or 203 or 204 or 205 or 206 or 207 or 208 or 209 or 210 or 211 or 212 or 213 or 214 or 215 or 216 or 217 or 218 or 219 or 220 or 221 or 222 or 223 or 224 or 225 or 226 or 227 or 228 or 229 or 230 or 231 or 232 or 233 or 234 or 235 or 236 or 237 or 238 or 239 or 240 or 241 or 242 or 243 or 244 or 245 or 246 or 247 or 248 or 249 or 250 or 251 or 252 or 253 or 254 or byte.MaxValue => "Complain about slaving by harkonnen",
                _ => UnknownValue,
            };
        }
    }
}