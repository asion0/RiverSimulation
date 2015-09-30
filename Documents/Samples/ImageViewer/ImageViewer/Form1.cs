using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class Form1 : Form
    {
        //Declare list to store picture box and checkbox controls
        List<PictureBox> listPB;
        List<CheckBox> chkBoxList;
        string str = string.Empty;
        public Form1()
        {
            InitializeComponent();
            listPB = new List<PictureBox>();
            chkBoxList = new List<CheckBox>();
            //Declare the connection string
            str= "Data Source=MYPC;Initial Catalog=myDB;Integrated Security=True";
            //Initialize the list
            listPB.Add(pictureBox1);
            listPB.Add(pictureBox2);
            listPB.Add(pictureBox3);
            listPB.Add(pictureBox4);
            chkBoxList.Add(chkpicture1);
            chkBoxList.Add(chkpicture2);
            chkBoxList.Add(chkpicture3);
            chkBoxList.Add(chkpicture4);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            SqlConnection sqlCon = new SqlConnection(str);
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("select name from test", sqlCon);
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlCon.Close();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";
            
        }

        private void btnAddFromFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Loop through the picture boxes
                foreach (PictureBox pb in listPB)
                {
                    //Find an empty picture box
                    if (pb.Image == null)
                    {
                        //Load the image
                        pb.Load(openFileDialog1.FileName);
                        Image img = pb.Image;
                        //Adjust the image size after loading it to Picture box
                        if (pb.Width < img.Width && pb.Height < img.Height)
                        {
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {

                            pb.SizeMode = PictureBoxSizeMode.Normal;
                        }
                        break;
                    }
                }

            }
        }

        private void btnAddToFile_Click(object sender, EventArgs e)
        {
            ShowCheckBoxes();
             int count = 0;
            foreach (CheckBox chk in chkBoxList)
            {
                if (chk.Checked)
                {
                    int index = count;

                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "Bitmap files (*.bmp)|*.bmp|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif";
                    save.FilterIndex = 4;
                    save.RestoreDirectory = true;

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        listPB[count].Image.Save(save.FileName);
                    }
                }
               
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (PictureBox img in listPB)
            {
                if (listPB[count].Image != null)
                {
                    chkBoxList[count].Visible = true;
                }
                ++count;
            }
             count = 0;
             foreach (CheckBox chk in chkBoxList)
             {
                 if (chk.Checked)
                 {
                     int index = count;
                     listPB[count].Image = null;
                     chk.Visible = false;
                 }
                 ++count;
             }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            ShowCheckBoxes();
            int count = 0;
            //Loop through all the picture boxes and add pictures selected by users.
            foreach (CheckBox chk in chkBoxList)
            {
                if (chk.Checked)
                {
                    string imageLocation = pictureBox1.ImageLocation;
                    byte[] image = null;
                    //Convert the image into byte array for saving it to DB
                    image = System.IO.File.ReadAllBytes(imageLocation);
                    SqlConnection sqlCon = new SqlConnection(str);
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("insert  into test(photo,name) values (@photo,@name)", sqlCon);
                    command.Parameters.AddWithValue("@photo", image);
                    command.Parameters.AddWithValue("@name", imageLocation);
                    sqlCon.Open();
                    command.ExecuteNonQuery();
                    sqlCon.Close();
                }
                ++count;
            }
        }
        public void ShowCheckBoxes()
        {
            int count = 0;
            //Loop through the picture box collection to find out the picture boxes that have images, only the checkboxes for those picture boxes will be visible
            foreach (PictureBox img in listPB)
            {
                if (listPB[count].Image != null)
                {
                    chkBoxList[count].Visible = true;
                }
                ++count;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(str);
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("select photo from test where name=@name", sqlCon);
            command.Parameters.AddWithValue("@name", comboBox1.SelectedValue.ToString());
            
            sqlCon.Open();
            byte[] image = (byte[])command.ExecuteScalar();
            if (image != null)
            {
                sqlCon.Close();
                //Save the picture from Db in the memory stream
                MemoryStream ms = new MemoryStream(image);
                //Load the picture from memory stream to picture box
                //pictureBox1.Image = Image.FromStream(ms);
                foreach (PictureBox pb in listPB)
                {
                    //Find an empty picture box
                    if (pb.Image == null)
                    {
                        pb.Image = Image.FromStream(ms);
                        break;
                    }
                }

            }
        }



    }
}
