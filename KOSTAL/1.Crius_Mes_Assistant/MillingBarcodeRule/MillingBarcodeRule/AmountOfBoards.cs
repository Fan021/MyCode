using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesStationCommon
{
    public partial class AmountOfBoards : Form
    {
        private string _boards = "";
        public AmountOfBoards()
        {
            InitializeComponent();
        }
        public string boards
        {
            get
            {
                return _boards;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            int num = 0;
            try
            {
                if(int.TryParse(this.textBox1.Text, out num))
                {
                    if(!Regex.IsMatch(this.textBox1.Text, @"^[0-9]*[1-9][0-9]*$"))
                    {
                        this.textBox1.BackColor = Color.Red;
                        button1.Enabled = false;
                        return;
                    }
                    else
                    {
                        this.textBox1.BackColor = Color.White;
                        if(e.KeyValue == 13)
                        {
                            button1.Enabled = true;
                            button1.Focus();
                        }
                    }
                }else
                {
                   this.textBox1.BackColor = Color.Red;
                    button1.Enabled = false;
                    return;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void AmountOfBoards_Load(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            this.ControlBox = false;
            this.button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _boards = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
