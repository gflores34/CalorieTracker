using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CalorieTracker
{
    //using System.Runtime.InteropServices;
    //using System;
    //using System.Windows.Forms;

    public partial class CalorieTrackerForm : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();


        public CalorieTrackerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setVersionLabel();
            SetDateLabel();
            side_home_button_click(sender, e);
            menu_strip1.RenderMode = ToolStripRenderMode.Professional;
            menu_strip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
        }

        //Grabs current date and sets the time labels
        private void SetDateLabel()
        {
            DateTime currentDate = DateTime.Today;
            home_date_label.Text = currentDate.ToString("d");
        }

        /*
         ************************* Sets version label to ToolStrip ***************************
         */
        private void setVersionLabel()
        {
            toolStrip_version_label.Text = Application.ProductVersion.ToString();
        }


        /*
         ************************* Set panels to false visibility / Remove borders from buttons ***************************
         */
        private void setPanelsInvisible()
        {
            //Panels
            home_panel.Visible = false;
            add_meal_panel.Visible = false;
            user_panel.Visible = false;
            data_panel.Visible = false;

            //Borders
            side_home_button.FlatAppearance.BorderSize = 0;
            side_addMeal_button.FlatAppearance.BorderSize = 0;
            side_searchData_button.FlatAppearance.BorderSize = 0;
        }

        /*
         ************************* hide or shows panel ***************************
         */
        private void toolStrip_hideSidePanel_label_Click(object sender, EventArgs e)
        {
            if (side_panel.Visible == true)
            {
                side_panel.Visible = false;
            }
            else
            {
                side_panel.Visible = true;
            }
        }


        /*
         ************************* Moveable window from menu strip ***************************
         */

        private void menu_strip1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void menu_strip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Focused)
            {
                Focus();
            }
        }


        /*
         ************************* Closes the application ***************************
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menu_exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
         ************************* Home Panel ***************************
         */
        private void side_home_button_click(object sender, EventArgs e)
        {
            setPanelsInvisible();
            home_panel.Visible = true;
            side_home_button.FlatAppearance.BorderSize = 1;
        }


        /*
         ************************* Add meal Panel ***************************
         */

        //Hides all panels and makes the Add Meal panel visible
        private void side_addMeal_button_Click(object sender, EventArgs e)
        {
            setPanelsInvisible();
            add_meal_panel.Visible = true;
            side_addMeal_button.FlatAppearance.BorderSize = 1;
            addMeal_restaurantName_label.Visible = false;
            addMeal_restaurant_textBox.Visible = false;
            addMeal_asterik3.Visible = false;
        }

        //Check for restaurant check
        private void addMeal_isRestaurant_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(addMeal_isRestaurant_checkBox.Checked)
            {
                addMeal_restaurantName_label.Visible = true;
                addMeal_restaurant_textBox.Visible = true;
                addMeal_asterik3.Visible = true;
            } 
            else
            {
                addMeal_restaurantName_label.Visible = false;
                addMeal_restaurant_textBox.Visible=false;
                addMeal_asterik3.Visible = false;
            }
        }


        /*
         ************************* User Panel ***************************
         */
        private void side_userProfile_pictureBox_Click(object sender, EventArgs e)
        {
            user_panel.Visible = true;
            loadUsers();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void user_addUser_button_Click(object sender, EventArgs e)
        {
            //Converting feet/inches to cm. Will help later on when calculating DailyCalorieIntake
            int feet = int.Parse(user_heightFeet_textBox.Text);
            int inches = int.Parse(user_heightInches_textBox.Text);
            double heightDouble = ((feet*12) + inches);
            int height = (int)heightDouble;

            //Daily Calorie intake
            int age = int.Parse(user_age_textBox.Text);
            int weight = int.Parse(user_goalWeight_textBox.Text);
            double dailyCaloriesMale = ((10 * (weight * 0.45)) + ((6.25 * (height * 2.54))) - (5 * 29) + 5);
            double dailyCaloriesFemale = ((10 * (weight * 0.45)) + ((6.25 * (height * 2.54))) - (5 * 29) - 161);


            if (user_sex_comboBox.Text == "male")
            {
                //Debug.WriteLine("male hit");
                var user = new User
                {
                    FirstName = user_firstName_textBox.Text,
                    LastName = user_lastName_textBox.Text,
                    Age = age,
                    Weight = weight,
                    Height = height,
                    GoalWeight = int.Parse(user_goalWeight_textBox.Text),
                    IsActive = true,
                    DailyCalorieGoal = (int)dailyCaloriesMale
                };
                Database db = new Database();
                db.InsertUser(user);
            }
            else if(user_sex_comboBox.Text == "female")
            {
                //Debug.WriteLine("female hit");
                var user = new User
                {
                    FirstName = user_firstName_textBox.Text,
                    LastName = user_lastName_textBox.Text,
                    Age = age,
                    Weight = weight,
                    Height = height,
                    GoalWeight = int.Parse(user_goalWeight_textBox.Text),
                    IsActive = true,
                    DailyCalorieGoal = (int)dailyCaloriesFemale
                };
                Database db = new Database();
                db.InsertUser(user);
            }
        }

        //Reset user text boxes
        private void user_resetUser_button_Click(object sender, EventArgs e)
        {
            user_firstName_textBox.Text = string.Empty;
            user_lastName_textBox.Text = string.Empty;
            user_heightFeet_textBox.Text = string.Empty;
            user_heightInches_textBox.Text = string.Empty;
            user_goalWeight_textBox.Text = string.Empty;
            user_weight_textBox.Text = string.Empty;
            user_age_textBox.Text = string.Empty;
            user_sex_comboBox.SelectedIndex = -1; 
        }

        private void loadUsers()
        {
            Database db = new Database();
            List<User> userList = db.FindAllUsers();
            List<string> userNames = new List<string>();

            for(int i = 0; i < userList.Count; i++)
            {
                userNames.Add(userList.ElementAt(i).Id + " " + userList.ElementAt(i).FirstName + " " + userList.ElementAt(i).LastName);
            }
            
            user_currentUsers_listBox.DataSource = userNames;
        }

        //Selects a User to be the active user
        private void user_selectUser_button_Click(object sender, EventArgs e)
        {

        }

        /*
         ************************* Data Panel ***************************
         */

        private void side_searchData_button_Click(object sender, EventArgs e)
        {
            setPanelsInvisible();
            data_panel.Visible = true;
            side_searchData_button.FlatAppearance.BorderSize = 1;
            setDataGrid();
        }

        private void setDataGrid()
        {
            Database db = new Database();
            var col = db.FindAllUsers();

            data_testData_dataGrid.DataSource = col;
        }
        private void data_selectUser_button_Click(object sender, EventArgs e)
        {

            //Checks if the grid has at least one user
            if(data_testData_dataGrid.SelectedCells.Count > 0)
            {
                //Get selected row
                int selectedRowIndex = data_testData_dataGrid.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = data_testData_dataGrid.Rows[selectedRowIndex];

                //Get Cell
                bool cellValueSelected = Convert.ToBoolean(selectedRow.Cells["IsSelected"].Value);
                int cellValueUserId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                //Check if a cell is selected
                if(cellValueSelected.ToString() == "True") 
                {
                    //Updates user to collection
                    Database db = new Database();
                    List<User> userList = db.FindUser(cellValueUserId);
                    User user = userList.ElementAt<User>(0);
                    user.IsSelected = true;
                    db.UpdateUser(user);
                    db.DeleteUser(user);      //TODO Might go back and look at this solution. Its the only way I could get LiteDB to update the user
                    db.InsertUser(user);
                    Debug.WriteLine("updated "+ user.FirstName +": IsSelected = " + user.IsSelected.ToString());
                }
            }
            setDataGrid();
            
        }

        private void data_removeUser_button_Click(object sender, EventArgs e)
        {

            if (data_testData_dataGrid.SelectedCells.Count > 0)
            {
                //Get selected row
                int selectedRowIndex = data_testData_dataGrid.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = data_testData_dataGrid.Rows[selectedRowIndex];

                //Get user.Id Cell
                int cellValueUserId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                
                    //Updates user to collection
                    Database db = new Database();
                    List<User> userList = db.FindUser(cellValueUserId);
                    User user = userList.ElementAt<User>(0);
                    db.DeleteUser(user);   
                    Debug.WriteLine("deleted " + user.FirstName);
                
            }
            setDataGrid();
        }

    }
}
