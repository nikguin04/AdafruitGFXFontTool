using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdafruitGFXFont
{
    internal class ImportTool
    {
        public void startImport(Form1 form1Control)
        {
            var fd = new OpenFileDialog();
            DialogResult result = fd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
            {
                /*try
                {*/
                    // Read input file
                    StreamReader sr = new StreamReader(fd.FileName);
                    string fullImportRead = sr.ReadToEnd();
                    sr.Close();

                    // Get general font names
                    string gfxFontName = new Regex(@"GFXfont (\S*)", RegexOptions.Multiline).Matches(fullImportRead)[0].Groups[1].Value;
                    Regex gfxInfoRegex = new Regex(@"GFXfont " + gfxFontName + @" [^\{]*{(?:[^\)]*\)|)([^,]*),(?:[^\)]*\)|)([^,]*),([^,]*),([^,]*),([^}]*)", RegexOptions.Multiline);
                    MatchCollection gfxInfoMatch = gfxInfoRegex.Matches(fullImportRead);
                    string gfxFontBitmapName = gfxInfoMatch[0].Groups[1].Value.Trim();
                    string gfxFontGlyphName = gfxInfoMatch[0].Groups[2].Value.Trim();
                    int gfxFontStartIndex = int.Parse(gfxInfoMatch[0].Groups[3].Value.Trim().Substring(2), System.Globalization.NumberStyles.HexNumber);
                    int gfxFontEndIndex = int.Parse(gfxInfoMatch[0].Groups[4].Value.Trim().Substring(2), System.Globalization.NumberStyles.HexNumber);
                    int gfxFontHeight = int.Parse(gfxInfoMatch[0].Groups[5].Value.Trim());

                    // Get bitmap data
                    string rawBitmapdata = new Regex(@"uint8_t " + gfxFontBitmapName + @"\[\][^\{]*{(.+?)};", RegexOptions.Singleline).Matches(fullImportRead)[0].Groups[1].Value;
                    string[] Bitmapdata = rawBitmapdata.Split(',');
                    // fix last null pointer
                    if (Bitmapdata[Bitmapdata.Length-1].Trim() == "")
                        Bitmapdata[Bitmapdata.Length-1] = "0x0";

                    // Get glyphs data
                    string rawGlyphs = new Regex(@"GFXglyph " + gfxFontGlyphName + @"\[\][^\{]*{(.+?)};", RegexOptions.Singleline).Matches(fullImportRead)[0].Groups[1].Value;
                    MatchCollection glyphMatches = new Regex(@"{([^}]*)").Matches(rawGlyphs);
                    form1Control.glyphDictionary.Clear();

                    int gmindex = 0;
                    foreach (Match glyphMatch in glyphMatches)
                    {
                        string[] glyphValues = glyphMatch.Groups[1].Value.Split(',');
                        int dictionaryIndex = gfxFontStartIndex + gmindex++;
                        int glyphBitmapStartIndex = int.Parse(glyphValues[0].Trim());
                        form1Control.glyphDictionary.Add(dictionaryIndex, new Form1.Glyph(
                            _width: int.Parse(glyphValues[1].Trim()),
                            _height: int.Parse(glyphValues[2].Trim()),
                            _xAdvance: int.Parse(glyphValues[3].Trim()),
                            _xOffset: int.Parse(glyphValues[4].Trim()),
                            _yOffset: int.Parse(glyphValues[5].Trim()),
                            _pixelDict: new Dictionary<int, bool>()
                            )
                        );

                        int pixelDictLength = form1Control.glyphDictionary[dictionaryIndex].width * form1Control.glyphDictionary[dictionaryIndex].height;
                        List<BitArray> bitmapParsedList = new List<BitArray>();
                        // Parse all used bitmaps into array
                        for (int a = 0; a < pixelDictLength / 4 + 1; a++)
                        {
                            bitmapParsedList.Add(new BitArray(new int[] { int.Parse(Bitmapdata[glyphBitmapStartIndex + a].Trim().Substring(2), System.Globalization.NumberStyles.HexNumber) }));
                        }
                        for (int i = 0; i < pixelDictLength; i++)
                        {
                            form1Control.glyphDictionary[dictionaryIndex].pixelDict.Add(i, bitmapParsedList[i/4][i%4]);                        
                        }
                        Console.Write("");

                        /*int maxPixelDictLength = form1Control.glyphDictionary[dictionaryIndex].width * form1Control.glyphDictionary[dictionaryIndex].height;
                        for (int i = 0; i < glyphBitmapLength; i++)
                        {
                            int gbIndex = totalGBIndex++;
                            if (Bitmapdata.Length <= gbIndex || Bitmapdata[gbIndex].Trim() == "")
                                break;
                            
                            int hexBit = int.Parse(Bitmapdata[gbIndex].Trim().Substring(2),System.Globalization.NumberStyles.HexNumber);

                            BitArray bitarr = new BitArray(new int[] { hexBit }); // will always be 4
                            for (int a = 0; a < 4; a++)
                            {
                                if (form1Control.glyphDictionary[dictionaryIndex].pixelDict.Count <= maxPixelDictLength)
                                    form1Control.glyphDictionary[dictionaryIndex].pixelDict.Add(form1Control.glyphDictionary[dictionaryIndex].pixelDict.Count, bitarr[a]);
                            }
                        }*/

                        
                    }

                form1Control.initializeGlyphsBlank(gfxFontStartIndex, gfxFontEndIndex);

                form1Control.minRange = gfxFontStartIndex;
                form1Control.maxRange = gfxFontEndIndex;
                form1Control.fontHeight = gfxFontHeight;
                form1Control.fontName = gfxFontName;
                MessageBox.Show("done?");

                /*} catch (Exception e)
                {
                    MessageBox.Show("ERROR PARSING IMPORTED FONT!\nThis tool is only tested with fonts generated by this program, your font code might differ from the import format");
                    MessageBox.Show(e.Message);
                    MessageBox.Show(e.StackTrace);
                }*/

            }
        }
    }
}
