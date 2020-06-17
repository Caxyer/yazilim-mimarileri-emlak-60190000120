using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace WindowsFormsApplication3
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
   OleDbConnection con = new OleDbConnection("provider=microsoft.ACE.Oledb.12.0;Data Source=emlak.accdb");
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;


        void listele()
        {
            con.Close();
            da = new OleDbDataAdapter("select * from arsa", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "arsa");
            dataGridView1.DataSource = ds.Tables["arsa"];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
 con.Open();
            da = new OleDbDataAdapter("SELECT num FROM arsa WHERE num=" + textBox1.Text, con);
            ds = new DataSet();
            da.Fill(ds, "arsa");
            if (ds.Tables.Count > 1)
            {
                MessageBox.Show(textBox1.Text + "numaralı öğrenci kayıtlı", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            else
            {
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO toner1(num,fiyat,konum)VALUES(@num,@fiyat,@konum)";
                cmd.Parameters.AddWithValue("@num", textBox1.Text);
                cmd.Parameters.AddWithValue("@fiyat", textBox2.Text);
               
                cmd.Parameters.AddWithValue("@konum", textBox3.Text);

                if (cmd.ExecuteNonQuery() > 0)
                    MessageBox.Show("kayıt başarı ile eklendi", "yeni kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("bir hata oluştu", "hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
                cmd.Parameters.Clear();
                listele();
            }
        
     
        }
    private void button2_Click(object sender, EventArgs e)
    {
        int secilenOgrenci = (int)dataGridView1.CurrentRow.Cells["num"].Value;

        DialogResult cevap = MessageBox.Show("Seçilen kayidi silmek istediğinize eminmisinz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (cevap == DialogResult.Yes)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM arsa WHERE num=" + secilenOgrenci;

            if (cmd.ExecuteNonQuery() > 0)
                MessageBox.Show("Kayit başarı ile silindi", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Bir hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            con.Close();
            listele();
        }
    }
        private void button3_Click(object sender, EventArgs e)
        {
             cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE ev SET num=@num,fiyat=@fiyat,konum=@konum WHERE num=" + textBox1.Text;
            cmd.Parameters.AddWithValue("@num", textBox1.Text);
            cmd.Parameters.AddWithValue("@fiyat", textBox2.Text);
            
            cmd.Parameters.AddWithValue("@konum", textBox3.Text);



            if (cmd.ExecuteNonQuery() > 0)
                MessageBox.Show("kayıt başarı ile güncelledi", "güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("hata oluştu", "hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            con.Close();
            cmd.Parameters.Clear();
            
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            da = new OleDbDataAdapter("SELECT * from arsa where num like '%" + textBox4.Text + "'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "arsa");
            dataGridView1.DataSource = ds.Tables["arsa"];
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenOgrenci = (int)dataGridView1.CurrentRow.Cells["num"].Value;
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM arsa WHERE num=@num";
            cmd.Parameters.AddWithValue("@num", secilenOgrenci);
            da = new OleDbDataAdapter(cmd);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["fiyat"].ToString();
                
                textBox1.Text = dr["num"].ToString();
                textBox3.Text = dr["konum"].ToString();

            }
            con.Close();
        }

        private void Form5_Activated(object sender, EventArgs e)
        {
            listele();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();
        }
    }
}
