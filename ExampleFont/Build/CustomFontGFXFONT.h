// GFX font created with Niklas Jensen's GFX font creation tool
const uint8_t CustomFontBitmaps[] PROGMEM = {
0x93,0x24,
0x84,0x1,
0xEA,0x80,
};

const GFXglyph CustomFontGlyphs[] PROGMEM = {
{0,3,5,4,0,0}, // Character 0x41 = A 
{2,4,4,4,0,0}, // Character 0x42 = B 
{4,3,5,4,0,0}, // Character 0x43 = C 
};

const GFXfont CustomFont PROGMEM = { (uint8_t*)CustomFontBitmaps,(GFXglyph*)CustomFontGlyphs, 0x41, 0x44, 5};
