using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdafruitGFXFont
{
    public partial class CharDisplayTest : Form
    {
        public CharDisplayTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(textBox1.Text);
            int height = Convert.ToInt32(textBox2.Text);

            string bytes = richTextBox1.Text;
            string[] byteArray = bytes.Split(',');

            List<int> bytearr = new List<int>();
            foreach (string b in byteArray)
            {
                if (b.Trim().StartsWith("0x"))
                    bytearr.Add(Convert.ToInt32(b.Trim(), 16));
            }

            if (width * height > bytearr.Count * 8)
            {
                MessageBox.Show("Length of width and height exceeds the given values");
                return;
            }

            int widthcounter = 0;
            int heightcounter = 0;

            string tolog = "";

            //int byteoffset = Convert.ToInt32(textBox3.Text, 16);
            int tocount = width * height / 8;

            //for (int a = byteoffset; a < byteoffset+tocount; a++)
            for (int a = 0; a < tocount; a++)
            {
                int b = bytearr[a];
                string bin = Convert.ToString(b, 2);

                int bl = bin.Length;
                for (int i = 0; i < 8 - bl; i++)
                {
                    bin = "0" + bin;
                }

                foreach (char c in bin)
                {
                    if (c == '0')
                    {
                        tolog += "  ";
                    }
                    else if (c == '1')
                    {
                        tolog += "X";
                    }
                    widthcounter++;
                    if (widthcounter == width)
                    {
                        widthcounter = 0;
                        tolog += "\n";
                        heightcounter++;
                        if (heightcounter == height)
                        {
                            //a = byteoffset + tocount;
                            a = tocount;
                            break;
                        }
                    }
                }
            }
            MessageBox.Show(tolog);
        }
    }
}
