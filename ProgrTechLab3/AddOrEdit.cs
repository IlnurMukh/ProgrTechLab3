using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrTechLab3
{
    public partial class AddOrEdit : Form
    {
        private Tree tree;
        private Exception? _averageEx = null;
        private Exception? _nameEx = null;
        private bool _nameIsEmpty;
        private bool _aLTIsIncorrect;

        public AddOrEdit(Tree t)
        {
            
            
            InitializeComponent();
            tree = t;
            //if (!IsNew(tree))
            //{
            //    textBox1.Text = tree.Name;
            //    comboBox1.SelectedValue = tree.LeafType;
            //    numericUpDown1.Value = tree.NumberOfTypes;
            //    textBox2.Text = tree.AverageLifetime.ToString();
            //    trackBar1.Value = (int)(tree.MaxHeight * 10);
            //    label6.Text = tree.MaxHeight.ToString();
            //}

            foreach (var leafType in Enum.GetValues(typeof(Tree.LeafTypes)))
            {
                comboBox1.Items.Add(leafType.ToString());
            }

            
            var bndName = textBox1.DataBindings.Add(nameof(TextBox.Text), tree, nameof(Tree.Name));
            bndName.FormattingEnabled = true;
            bndName.BindingComplete += BndNameOnBindingComplete;
            comboBox1.DataBindings.Add(nameof(ComboBox.SelectedIndex), tree, nameof(Tree.IntLeafType));
            numericUpDown1.DataBindings.Add(nameof(NumericUpDown.Value), tree, nameof(Tree.NumberOfTypes));
            var bndALT = textBox2.DataBindings.Add(nameof(TextBox.Text), tree, nameof(Tree.AverageLifetime));
            bndALT.FormattingEnabled = true;
            bndALT.BindingComplete += BndALTOnBindingComplete;
            trackBar1.DataBindings.Add(nameof(TrackBar.Value), tree, nameof(Tree.MaxHeight));
        }

        private void BndALTOnBindingComplete(object sender, BindingCompleteEventArgs e)
        {
            e.Cancel = false;
            _aLTIsIncorrect = false;
            if (e.BindingCompleteContext == BindingCompleteContext.DataSourceUpdate && e.Exception != null)
            {
                for (int i = 0; i < 180; i++)
                {
                    textBox2.BackColor = (Color.FromArgb(255, i, i));
                }

                
            }
            if (textBox2.Text == "0")
                _aLTIsIncorrect = true;
            _averageEx = e.Exception;
        }

        private void BndNameOnBindingComplete(object sender, BindingCompleteEventArgs e)
        {
            e.Cancel = false;
            _nameIsEmpty = false;
            if (e.BindingCompleteContext == BindingCompleteContext.DataSourceUpdate && e.Exception != null || textBox1.Text.Trim().Length == 0)
            {
                for (int i = 0; i < 180; i++)
                {
                    textBox1.BackColor = (Color.FromArgb(255,i,i));
                }
                _nameIsEmpty = true;
            }
            _nameEx = e.Exception;

        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label6.Text = ((float)(trackBar1.Value) / 10).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_nameEx == null && _averageEx == null && !_nameIsEmpty && !_aLTIsIncorrect)
            {
                DialogResult = DialogResult.OK;
                tree.MaxHeight = trackBar1.Value / 10f;
            }
            else if (_nameEx != null || _nameIsEmpty)
            {
                textBox1.BackColor = Color.LightCoral;
                textBox1.Focus();
                MessageBox.Show("Проверьте правильность заполнения поля\"Название\"");
            }
            else
            {
                textBox2.BackColor = Color.LightCoral;
                textBox2.Focus();
                MessageBox.Show("Проверьте правильность заполнения поля\"Среднее время жизни\"");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text !="0")
                textBox2.BackColor = Color.White;
        }
    }
}
