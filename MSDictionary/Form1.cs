using MetroFramework.Forms;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace MSDictionary
{
    public partial class Form1 : MetroForm
    {
        SQLiteConnection m_dbConnection = null;
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        bool key_press = true;
        public Form1()
        {
            InitializeComponent();
            dbConnector();
            loadAllData();
        }


        public void dbConnector()
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;
            m_dbConnection = new SQLiteConnection(@"Data Source=" + path + "\\database\\dbDic.dll;Version=3;");
            m_dbConnection.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSearchWord.Focus();

        }
        public void loadAllData()
        {

            string sql = "select * from ovidhan";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            //clear list
            // m_list.Clear();
            lboxWord.Items.Clear();

            //load data to dictionnary
            while (reader.Read())
            {

                //lboxWord.Items.Add(reader["_id"].ToString());
                dic.Add(reader["_id"].ToString(), new List<string>
                                {
                                reader["_id"].ToString(),
                                reader["pron"].ToString(),
                                reader["pos"].ToString(),
                                reader["meaning"].ToString(),
                                reader["synonyms"].ToString(),
                                reader["new"].ToString(),
                                reader["modify"].ToString()

                                 });
            }

            
            List<string> list = new List<string>(dic.Keys);
            // Loop through list.
            list.Sort();
            foreach (string k in list)
            {
                //Console.WriteLine("{0}, {1}", k, dic[k]);
                lboxWord.Items.Add(k);
            }


        }
        private void lboxWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keyWord = lboxWord.SelectedItem.ToString();
            if (dic.ContainsKey(keyWord))
            {
                
                textBox1.Text = dic[keyWord][0];
                textBox2.Text = dic[keyWord][1];
                textBox3.Text = dic[keyWord][2];
                textBox4.Text = dic[keyWord][3];
                textBox5.Text = dic[keyWord][4];
                textBox6.Text = dic[keyWord][5];
       
            }
            else
            {

                textBox1.Text = "Sorry!!! not found";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        private void txtSearchWord_TextChanged(object sender, EventArgs e)
        {
            int x = -1;
            if (txtSearchWord.Text.Length != 0)
            {
                do
                {
                    x = lboxWord.FindString(txtSearchWord.Text, x);

                    if (x != -1)
                    {
                        if (key_press)
                        {
                            lboxWord.TopIndex = x;
                        }
                      //  lboxWord.TopIndex = x;
                        lboxWord.SetSelected(x, true);
                        return;
                    }
                    else {
                        
                        textBox1.Text = "not found";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                    }

                } while (x != -1);
            }
            else
            {
                lboxWord.SetSelected(0, true);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";

            }
        }

        private void lboxWord_MouseClick(object sender, MouseEventArgs e)
        {
            key_press = false;
            txtSearchWord.Text = lboxWord.SelectedItem.ToString();
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MetroMessageBox.Show(this, "\n\nAre You Want To Exit?", "MSDictionaryt | Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
            else
            {
               
            }
           
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this,"\n\nDevelpoed by: Marufsharia \n Database provided by: Mahmudul Hasan Shohag ", "MSDictionaryt | About", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
        }

        private void lboxWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                key_press = false;
                txtSearchWord.Text = textBox1.Text;
            }
            if (e.KeyCode == Keys.Down)
            {
                key_press = false;
                txtSearchWord.Text = lboxWord.SelectedItem.ToString();
            }
        }
        private void lboxWord_KeyPress(object sender, KeyPressEventArgs e)
        {
           
                txtSearchWord.Text = lboxWord.SelectedItem.ToString();
            
        }

        private void txtSearchWord_Click(object sender, EventArgs e)
        {
            key_press = true;
        }

        private void txtSearchWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            key_press = true;
        }
    }
}
