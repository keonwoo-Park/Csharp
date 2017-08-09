using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memo
{
    public partial class memoPadForm : Form
    {
        public bool modified = false;
        public bool haveFileName = false;
        public string filePath = string.Empty;


        public memoPadForm()
        {
            InitializeComponent();
        }

        public void reset_memo()
        {
            content.Text = string.Empty;
            filePath = string.Empty;
            modified = false;
            haveFileName = false;
            memoPadForm.ActiveForm.Text = "제목 없음 - 메모장";
        }

        public string getFileName()
        {
            int startFileNamePosition = filePath.LastIndexOf("\\");
            int endFileNamePosition = filePath.LastIndexOf(".");
            int nameLenth = (endFileNamePosition - 1) - startFileNamePosition;
            string name = filePath.Substring(startFileNamePosition + 1, nameLenth);

            return name;
        }

        public void saveFile()
        {
            string fileName = string.Empty;

            if (haveFileName == true)
            {
                System.IO.StreamWriter fs = System.IO.File.CreateText(filePath);
                fs.WriteLine(content.Text);
                fs.Close();
                modified = false;
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    System.IO.StreamWriter fs = System.IO.File.CreateText(filePath);
                    fs.WriteLine(content.Text);
                    fs.Close();
                    modified = false;
                    haveFileName = true;
                }
                else
                {
                    memoPadForm.ActiveForm.Text = "제목 없음 - 메모장";
                    return;
                }
            }

            fileName = getFileName();
            memoPadForm.ActiveForm.Text = fileName + " - 메모장";
        }

        private void content_TextChanged(object sender, EventArgs e)
        {
            modified = true;
        }

        private void newPage_Click(object sender, EventArgs e)
        {
            try
            {
                if(modified == true)
                {
                    DialogResult result = MessageBox.Show("변경내역을 저장하시겠습니까?", "저장", MessageBoxButtons.YesNoCancel );
                    if(result == DialogResult.Yes)
                    {
                        saveFile();
                        reset_memo();
                    }
                    else if(result == DialogResult.No)
                    {
                        reset_memo();
                    }
                    else if(result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    if(haveFileName == true)
                    {
                        reset_memo();
                    }
                }
            }
            catch
            {
                MessageBox.Show("새 파일을 준비하는 도중 이상이 발생했습니다.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            try
            {
                if(modified == true)
                {
                    DialogResult result = MessageBox.Show("변경 내역을 저장 하시겠습니까? ","저장",MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        saveFile();
                        MessageBox.Show("저장완료.");
                    }
                    reset_memo();
                }
                if(openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;
                    fileName = getFileName();
                    memoPadForm.ActiveForm.Text = fileName + " - 메모장 ";                    

                    System.IO.StreamReader fs = System.IO.File.OpenText(filePath);
                    content.Text = fs.ReadToEnd();
                    fs.Close();
                    modified = false;
                    haveFileName = true;
                }
            }
            catch
            {
                MessageBox.Show("열기를 하는 도중 이상이 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                saveFile();
            }
            catch
            {
                MessageBox.Show("저장을 하는 도중 이상이 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            try
            {
                if(modified == true)
                {
                    DialogResult result = MessageBox.Show("변경 사항을 저장하시겠습니까?","저장", MessageBoxButtons.YesNoCancel);
                    if(result == DialogResult.Yes)
                    {
                        saveFile();
                    }
                    else if(result == DialogResult.Cancel)
                    {
                        return;
                    }
                    reset_memo();
                }
                Application.Exit();
            }
            catch
            {
                MessageBox.Show("종료 하는 도중 이상이 발생했습니다.", "에러", MessageBoxButtons. OK,MessageBoxIcon.Warning);
            }
        }

        private void anotherName_Click(object sender, EventArgs e)
        {
            try
            {
                haveFileName = false;
                saveFile();
            }
            catch
            {
                MessageBox.Show("저장을 하는 도중 이상이 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void memoPadInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is c# study.\n It's beginner level~");
        }
    }
}
