using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace library
{
    public partial class Form2 : Form
    {
        
        string[] kontrol = new string[8];

        public void veriCek()
        {
            adapter = new MySqlDataAdapter("SELECT * FROM librarypanel2 ", mysqlbaglan);
            MySqlCommand command = new MySqlCommand(
          "SELECT * FROM librarypanel2" ,
          mysqlbaglan);


            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                if (reader.Read() != false) 
                {
                    for (int i = 0; i <= reader.Depth; i++)
                    {


                        kontrol[i] = Convert.ToString(reader[i]);

                    }
                }else
                {
                    
                }
                
 
            }
            else
            {
                
            }
            reader.Close();

        }

        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=library;Uid=root;Pwd='';");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataAdapter adana;
        DataTable dt;
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                mysqlbaglan.Open();
                if (mysqlbaglan.State != ConnectionState.Closed)
                {
                    MessageBox.Show("Bağlantı Başarılı Bir Şekilde Gerçekleşti");
                }
                else
                {
                    MessageBox.Show("Maalesef Bağlantı Yapılamadı...!");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Hata! " + err.Message, "Hata Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            VeriGetirM();
            VeriGetirK();
            veriCek();



        }
        public void veriEkleM()
        {
            string sorgu = "Insert into librarypanel (musteriNO,musteriAD,kitapNO,kiralamaSuresi) values (@mNo,@mName,@kNO,@deadline)";


            cmd = new MySqlCommand(sorgu, mysqlbaglan);
            cmd.Parameters.AddWithValue("@mNO", mNO.Text);
            cmd.Parameters.AddWithValue("@mName", mName.Text);
            cmd.Parameters.AddWithValue("@kNO", kNO1.Text);
            cmd.Parameters.AddWithValue("@deadline", deadline.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Eklendi!");
            

        }
        public void VeriGetirM()
        {
            dt = new DataTable();
            
            adapter = new MySqlDataAdapter("SELECT *FROM librarypanel", mysqlbaglan);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            

        }

        private void ekleBtnM_Click(object sender, EventArgs e)
        {
            veriEkleM();
            VeriGetirM();
            guncelle();
            veriCek();

            mNO.Text = "";
            mName.Text = "";
            kNO1.Text = "";
            deadline.Text = "";

        }

        public void veriEkleK()
        {
            string sorgu = "Insert into librarypanel2 (kitapisim,stok,yazar,kitapNO) values (@kName,@stok,@azar,@kNO2)";


            cmd = new MySqlCommand(sorgu, mysqlbaglan);
            cmd.Parameters.AddWithValue("@kName", kName.Text);
            cmd.Parameters.AddWithValue("@stok", stok.Text);
            cmd.Parameters.AddWithValue("@azar", azar.Text);
            cmd.Parameters.AddWithValue("@kNO2", kNO2.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Eklendi!");


        }
        public void VeriGetirK()
        {
            dt = new DataTable();

            adapter = new MySqlDataAdapter("SELECT *FROM librarypanel2", mysqlbaglan);
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;

        }
        

        public void VeriSil()
        {
            //Farklı bir sütunu seçtiğimizde hata alıyoruz, düzeltilecek.

           // if (dataGridView2.SelectedRows[0].Cells.Count > 0)
            {
                string sql = "Delete From librarypanel2 Where kitapNO=@kNO1";
                cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.Parameters.AddWithValue("@kNO1", Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[3].Value));

                cmd.ExecuteNonQuery();

                VeriGetirK();

                MessageBox.Show("Kayıt silindi.");
            }
            //else
            {
                //MessageBox.Show("Silinecek sütunu seçmediniz!");
            }
              
            
            



        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

                int index = e.RowIndex;
                
                DataGridViewRow row = dataGridView2.Rows[index];
                kNO1.Text = row.Cells[0].Value.ToString();
                
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            veriEkleK();    
            VeriGetirK();
            veriCek();

            kName.Text = "";
            stok.Text = "";
            azar.Text = "";
            kNO2.Text = "";

        }

        public void guncelle()
        {
            

            adapter = new MySqlDataAdapter("SELECT * FROM librarypanel2 ", mysqlbaglan);
            MySqlCommand command = new MySqlCommand(
          "SELECT * FROM librarypanel2 WHERE kitapNO="+kNO1.Text,
          mysqlbaglan);


            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                    reader.Read();
                
                    int tut = Convert.ToInt32(reader["stok"]) - 1;
                    mysqlbaglan.Close();
                    mysqlbaglan.Open();
                    string sql = "UPDATE librarypanel2 SET stok=@stok WHERE kitapNO="+kNO1.Text;
                    cmd = new MySqlCommand(sql, mysqlbaglan);
                    cmd.Parameters.AddWithValue("@stok", tut);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Müşteri eklendi!");
                    VeriGetirK();
                
            }
            else
            {
                MessageBox.Show("Veritabanı hatası!");
            }
            reader.Close();





        }

        private void button2_Click(object sender, EventArgs e)
        {
            veriCek();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(kontrol.Contains(kNO2.Text) == true)
            {
                string sql = "UPDATE librarypanel2 SET kitapisim=@kName,stok=@stok,yazar=@azar WHERE kitapNO=@kNO2";
                cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.Parameters.AddWithValue("@kName", kName.Text);
                cmd.Parameters.AddWithValue("@stok", stok.Text);
                cmd.Parameters.AddWithValue("@azar", azar.Text);
                cmd.Parameters.AddWithValue("kNO2", kNO2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kitap için girdiğiniz değerler güncellendi!");
                VeriGetirK();
                veriCek();
            }
            else
            {
                MessageBox.Show("Kitap güncellenemedi. Hata!");
            }

            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            veriCek();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            VeriSil();
        }
    }
}
