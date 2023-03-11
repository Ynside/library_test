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
                    MessageBox.Show("Database bağlantı hatası!");
                }
                
 
            }
            else
            {
                MessageBox.Show("Hata! Sütun yok.");
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
                Console.WriteLine("No rows found.");
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
                MessageBox.Show("Kitap Güncellendi!");
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
    }
}
