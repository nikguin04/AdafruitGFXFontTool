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
    public partial class CharRangecs : Form
    {
        public CharRangecs()
        {
            InitializeComponent();
            textBox1_TextChanged(null, null);
            textBox2_TextChanged(null, null);
            textBox3_TextChanged(null, null);
            textBox4_TextChanged(null, null);
        }

        public int minRange = 0;
        public int maxRange = 0;
        public int fontHeight = 0;
        public string fontName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //minRange = Convert.ToInt32(textBox1.Text);
            //maxRange = Convert.ToInt32(textBox2.Text);
            //fontHeight = Convert.ToInt32(textBox3.Text);
            if (minRange == 0 || maxRange == 0 || fontHeight == 0 || fontName == "")
            {
                MessageBox.Show("Inputs are not valid");
                return;
                
            }
            DialogResult = DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            try {
                int num;
                if (text.StartsWith("0x"))
                {
                    num = Convert.ToInt32(text, 16);
                } else
                {
                    num = Convert.ToInt32(text);
                }
                char _char = Form1.uft8CharFromInt(num);
                label4.Text = num.ToString() + " / 0x" + Convert.ToString(num, 16) + " = " + _char;
                minRange = num;

            } catch (Exception exc)
            {
                label4.Text = "Not A Number";
                minRange = 0;
            }
            
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            try
            {
                int num;
                if (text.StartsWith("0x"))
                {
                    num = Convert.ToInt32(text, 16);
                }
                else
                {
                    num = Convert.ToInt32(text);
                }
                char _char = Form1.uft8CharFromInt(num);
                label5.Text = num.ToString() + " / 0x" + Convert.ToString(num, 16) + " = " + _char;
                maxRange = num;

            }
            catch (Exception exc)
            {
                label5.Text = "Not A Number";
                maxRange = 0;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fontHeight = Convert.ToInt32(textBox3.Text);
            } catch (Exception exc)
            {
                fontHeight = 0;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            fontName = textBox4.Text;
        }
    }
}
