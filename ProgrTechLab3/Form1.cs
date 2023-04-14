using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Reflection;

namespace ProgrTechLab3
{
    public partial class MainForm : Form
    {
        private BindingList<Tree> _trees = new();

        public MainForm()
        {
            InitializeComponent();
            
            //_trees.Add(new Tree("new", 0, 0, 0, 0));


            dataGridView1.DataSource = _trees;

        }


       

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    using (FileStream fs = new FileStream("trees.json", FileMode.Open))
        //    {
        //        try
        //        {
        //            var inputData = JsonSerializer.Deserialize<BindingList<Tree>>(fs);
        //            if (inputData != null)
        //                foreach (var tree in inputData)
        //                {
        //                    _trees.Add(tree);
        //                }
        //        }
        //        catch (JsonException exception)
        //        {
        //            MessageBox.Show("Файл пуст!", "Ой-ой", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
                
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    using (FileStream fs = new FileStream("trees.json", FileMode.OpenOrCreate))
        //    {
        //        JsonSerializer.Serialize(fs, _trees);
        //    }
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    using (FileStream fs = new FileStream("trees.json", FileMode.Open))
        //    {
        //        try
        //        {
        //            var refreshData = JsonSerializer.Deserialize<BindingList<Tree>>(fs);
        //            if (refreshData != null)
        //            {
        //                _trees.Clear();
        //                foreach (var tree in refreshData)
        //                {
        //                    _trees.Add(tree);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
        //}
        private void показатьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Process.Start($"https://yandex.ru/images/search?text={_trees[dataGridView1.SelectedRows[0].Index].Name}&from=tabbar");
            }
            else
            {
                MessageBox.Show("Выберите строку", "Ой-ой", MessageBoxButtons.OK);
            }

            
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var cloneTree = _trees[dataGridView1.SelectedRows[0].Index].CloneTree();
                AddOrEdit editForm = new AddOrEdit(cloneTree); 
                editForm.ShowDialog(this);
                if (editForm.DialogResult == DialogResult.OK)
                    _trees[dataGridView1.SelectedRows[0].Index] = cloneTree;
            }
            else
            {
                MessageBox.Show("Выберите строку", "Ой-ой", MessageBoxButtons.OK);
            }

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                _trees.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Выберите строку", "Ой-ой", MessageBoxButtons.OK);
            }
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    Tree newTree = new Tree("new", 0, 0, 0, 0);
        //    AddOrEdit addForm = new AddOrEdit(newTree);
        //    if (addForm.ShowDialog(this) == DialogResult.OK)
        //        _trees.Add(newTree);
        //}

        //private void toolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(
        //        "Программа для хранения данных о деревьях.\nОчень интересная!\n© 2023 Ilnur Mukhamatgaraev. All rights reserved.",
        //        "О программе",
        //        MessageBoxButtons.OK,
        //        MessageBoxIcon.Information);
        //}

        //private void toolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(
        //            "\"Добавить из файла\" - добавляет в таблицу данные из JSON файла.\n" +
        //            "\"Сохранить\" - сохраняет данные из таблицы в JSON файл.\n" + 
        //            "\"Обновить\" - удаляет несохраненные данные и обновляет таблицу в соответствии с JSON файлом.\n" + 
        //            "\"Добавить\" - добавляет информацию о дереве, которую указывает пользователь.",
        //            "Справка",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
                
        //}
        
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FileChoose fileChoose = new FileChoose();
            //if (fileChoose.ShowDialog(this) == DialogResult.OK)
            //{
            //    int index;
            //    if (fileChoose.radioButton1.Checked)
            //        index = 1;
            //    else if (fileChoose.radioButton2.Checked)
            //        index = 2;
            //    else if (fileChoose.radioButton3.Checked)
            //        index = 3;
            //    else index = -1;
            //    if (index != -1)
            //    {
            //        using (FileStream fs = new FileStream($"trees{index}.json", FileMode.OpenOrCreate))
            //        { JsonSerializer.Serialize(fs, _trees); }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Выберите файл!");
            //    }
            //}
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fileName = saveFileDialog1.FileName;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            { JsonSerializer.Serialize(fs, _trees); }
        }

        private void добавитьИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fileName = openFileDialog1.FileName;
            try 
            { 
                using (FileStream fs = new FileStream(fileName, FileMode.Open)) 
                { 
                    var inputData = JsonSerializer.Deserialize<BindingList<Tree>>(fs); 
                    if (inputData != null) 
                        foreach (var tree in inputData) 
                        { 
                            _trees.Add(tree);
                        }
                }
            }
            catch (Exception exception) 
            { 
                MessageBox.Show("Не существует!", "Ой-ой", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void добавитьВручнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tree newTree = new Tree("new", 0, 0, 0, 0);
            AddOrEdit addForm = new AddOrEdit(newTree);
            if (addForm.ShowDialog(this) == DialogResult.OK)
                _trees.Add(newTree);
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fileName = openFileDialog1.FileName;
            try
            { 
                using (FileStream fs = new FileStream(fileName, FileMode.Open)) 
                { 
                    var refreshData = JsonSerializer.Deserialize<BindingList<Tree>>(fs); 
                    if (refreshData != null) 
                    { 
                        _trees.Clear(); 
                        foreach (var tree in refreshData) 
                        { 
                            _trees.Add(tree);
                        }
                    }

                }
            }
            catch (Exception exception) 
            { 
                _trees.Clear();
            }
            
        }
            
    }

}


