using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AdafruitGFXFont
{
    public partial class Form1 : Form
    {
        public Dictionary<int, Glyph> glyphDictionary = new Dictionary<int, Glyph>();

        public class Glyph
        {
            
            public int width;
            public int height;
            public int xAdvance;
            public int xOffset;
            public int yOffset;
            //public List<byte> bitmapBits;
            public Dictionary<int, bool> pixelDict;

            public Glyph(int _width, int _height, int _xAdvance, int _xOffset, int _yOffset, Dictionary<int, bool> _pixelDict)
            {
                width = _width;
                height = _height;
                xAdvance = _xAdvance;
                xOffset = _xOffset;
                yOffset = _yOffset;
                pixelDict = _pixelDict;
            }
            public Glyph() { }
        }

        //public class 

        public Form1()
        {
            InitializeComponent();

            //flowLayoutPanel1.Controls.Clear();
        }

        

        public int minRange = 0;
        public int maxRange = 0;
        public int fontHeight = 0;
        public string fontName = "";

        private void button2_Click(object sender, EventArgs e)
        {
            using (CharRangecs charrange = new CharRangecs())
            {
                if (charrange.ShowDialog() == DialogResult.OK)
                {
                    minRange = charrange.minRange;
                    maxRange = charrange.maxRange+1;
                    fontHeight = charrange.fontHeight;
                    fontName = charrange.fontName;
                    this.Text = "Adafruit GFX Font Designer (" + charrange.fontName + ")";
                    label5.Text = "Choose Char (Height: " + Convert.ToString(charrange.fontHeight) + ")";

                    initializeGlyphs();
                    button2.Enabled = false;
                    button5.Enabled = false;
                }
            }
        }

        private void initializeGlyphs()
        {
            for (int i = minRange; i < maxRange; i++)
            {
                glyphDictionary.Add(i, new Glyph());
                //glyphDictionary[i].bitmapBits = new List<byte>();
                glyphDictionary[i].pixelDict = new Dictionary<int, bool>();

                glyphDictionary[i].width = 3;
                glyphDictionary[i].height = fontHeight;
                glyphDictionary[i].xAdvance = 4;
                glyphDictionary[i].xOffset = 0;
                glyphDictionary[i].yOffset = 0;

                comboBox1.Items.Add(i.ToString() + " / 0x" + Convert.ToString(i, 16) + " = " + uft8CharFromInt(i));
                tryInitGlyphDict(i, glyphDictionary[i].width, glyphDictionary[i].height);
            }
            comboBox1.SelectedIndex = 0;
            button4.Enabled = true;
        }

        public void initializeGlyphsBlank(int minRange, int maxRange)
        {
            for (int i = minRange; i < maxRange; i++)
            {
                comboBox1.Items.Add(i.ToString() + " / 0x" + Convert.ToString(i, 16) + " = " + uft8CharFromInt(i));
                
            }
            comboBox1.SelectedIndex = 0;
            button4.Enabled = true;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int width = Convert.ToInt32(textBox4.Text), height = Convert.ToInt32(textBox5.Text);

            int index = comboBox1.SelectedIndex + minRange;
            textBox4.Text = glyphDictionary[index].width.ToString();
            textBox5.Text = glyphDictionary[index].height.ToString();
            textBox6.Text = glyphDictionary[index].xAdvance.ToString();
            textBox7.Text = glyphDictionary[index].xOffset.ToString();
            textBox8.Text = glyphDictionary[index].yOffset.ToString();

            initLayoutPanel(glyphDictionary[index].width, glyphDictionary[index].height, index);
        }

        public void tryInitGlyphDict(int gIndex, int width, int height)
        {
            if (glyphDictionary[gIndex].pixelDict.Keys.Count == 0)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int index = x + y * width;
                        glyphDictionary[gIndex].pixelDict.Add(index, false);
                    }
                }
            }
        }

        private void initLayoutPanel(int width, int height, int gIndex)
        {

            foreach (PictureBox pb in flowLayoutPanel1.Controls)
            {
                pb.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            tryInitGlyphDict(gIndex, width, height);
            
            int size = 40;
            flowLayoutPanel1.Width = (size+6) * width + 4;
            flowLayoutPanel1.Height = (size + 6) * height + 4;
            if (glyphDictionary[gIndex].pixelDict.Keys.Count == 0 || true)
            {
                //int index = 0;
                for (int y = 0; y < height; y++)
                {

                    for (int x = 0; x < width; x++)
                    {
                        int index = x + y * width;
                        
                        

                        //glyphDictionary[gIndex].pixelDict[index] = false;
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Size = new Size(size, size);

                        flowLayoutPanel1.Controls.Add(pictureBox);
                        //if (y + x != 0 && (y + x * height + 1) % width == 0) // use if y not x
                        if (y + x != 0 && (index+1)%width == 0 || width == 1)
                            flowLayoutPanel1.SetFlowBreak(pictureBox, true);

                        Color initCol;
                        if (!glyphDictionary[gIndex].pixelDict[index])
                        {
                            initCol = SystemColors.ControlLightLight;
                        }
                        else
                        {
                            initCol = SystemColors.ControlText;
                        }
                        pictureBox.BackColor = initCol;
                        pictureBox.Name = flowLayoutPanel1.Controls.Count.ToString(); // EDIT THIS LATER
                        pictureBox.Tag = index.ToString();

                        pictureBox.MouseClick += new MouseEventHandler((_sender, _eventargs) =>
                        {
                            Color col;
                            
                            if (!glyphDictionary[gIndex].pixelDict[index])
                            {
                                col = SystemColors.ControlText;
                                glyphDictionary[gIndex].pixelDict[index] = true;
                            }
                            else
                            {
                                col = SystemColors.ControlLightLight;
                                glyphDictionary[gIndex].pixelDict[index] = false;
                            }
                            pictureBox.BackColor = col;
                        });
                        //index++;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(textBox4.Text),
                height = Convert.ToInt32(textBox5.Text),
                xAdvance = Convert.ToInt32(textBox6.Text),
                xOffset = Convert.ToInt32(textBox7.Text),
                yOffset = Convert.ToInt32(textBox8.Text);

            int index = comboBox1.SelectedIndex + minRange;

            glyphDictionary[index].width = width;
            glyphDictionary[index].height = height;
            glyphDictionary[index].xAdvance = xAdvance;
            glyphDictionary[index].xOffset = xOffset;
            glyphDictionary[index].yOffset = yOffset;

            glyphDictionary[index].pixelDict.Clear();

            initLayoutPanel(width, height, index);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Bitmap, Glyphs, GFXFont

            string bitmapOutput = "";
            bitmapOutput += "const uint8_t " + fontName + "Bitmaps[] PROGMEM = {\n";

            string glyphsOutput = "";
            glyphsOutput += "const GFXglyph " + fontName + "Glyphs[] PROGMEM = {\n";
            int bitmapIndex = 0;
            int tempBitmapIndex = 0;

            string GFXfont = $"const GFXfont {fontName} PROGMEM = {{ (uint8_t*){fontName}Bitmaps,(GFXglyph*){fontName}Glyphs, 0x{Convert.ToString(minRange, 16)}, 0x{Convert.ToString(maxRange, 16)}, {fontHeight.ToString()}}};";

            for (int a = minRange; a < +maxRange; a++)
            {
                Glyph glyph = glyphDictionary[a];
                int pixelByte = 0;
                int dictIndex = 0;

                for (int i = 0; i < glyph.pixelDict.Keys.Count; i++)
                {
                    bool pix = glyph.pixelDict[i];
                    if (pix)
                    {
                        pixelByte += Convert.ToInt32(Math.Pow(2, 8-1-dictIndex));
                    }
                    dictIndex++;

                    if (dictIndex == 8 || i == glyph.pixelDict.Keys.Count - 1)
                    {
                        /*for (int p = 0; p < 8-dictIndex; p++)
                        {

                        }*/
                        bitmapOutput += "0x" + Convert.ToString(pixelByte, 16).ToUpper() + ",";
                        dictIndex = 0;
                        pixelByte = 0;
                        bitmapIndex++;
                    }
                }
                bitmapOutput += "\n";


                glyphsOutput += $"{{{tempBitmapIndex},{glyph.width},{glyph.height},{glyph.xAdvance},{glyph.xOffset},{glyph.yOffset}}}, // Character 0x{Convert.ToString(a, 16)} = {uft8CharFromInt(a)} \n";
                tempBitmapIndex = bitmapIndex;
            }

            //bitmapOutput = bitmapOutput.Substring(0, bitmapOutput.Length - 1); // remove last comma
            bitmapOutput += "};\n";
            glyphsOutput += "};\n";

            //MessageBox.Show(bitmapOutput);
            //MessageBox.Show(glyphsOutput);
            //MessageBox.Show(GFXfont);

            CommonOpenFileDialog fbd = new CommonOpenFileDialog();
            fbd.IsFolderPicker = true;
            CommonFileDialogResult result = fbd.ShowDialog();
            
            if (result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(fbd.FileName))
            {
                //string[] files = Directory.GetFiles(fbd.SelectedPath);
                //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                StreamWriter sw = new StreamWriter(fbd.FileName + "\\" + fontName + "GFXFONT.h");
                sw.WriteLine("// GFX font created with Niklas Jensen's GFX font creation tool");
                sw.WriteLine(bitmapOutput);
                sw.WriteLine(glyphsOutput);
                sw.WriteLine(GFXfont);

                sw.Close();
                MessageBox.Show("Success");
            }

        }

        public static char uft8CharFromInt(int num)
        {
            byte[] bytearr = new byte[1] { (byte)num };
            return Encoding.UTF8.GetChars(bytearr)[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ImportTool import_tool = new ImportTool();
            import_tool.startImport(this);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (CharDisplayTest cdt = new CharDisplayTest())
            {
                if (cdt.ShowDialog() == DialogResult.OK)
                {
                    // logic?
                }
            }
        }
    }
}
